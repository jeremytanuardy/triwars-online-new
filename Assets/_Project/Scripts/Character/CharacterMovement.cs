using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Handles WASD character movement with NavMeshAgent
/// Albion-style: No jump, walk/run blending via Animator Speed parameter
/// Works with ClickToMove for point-and-click + WASD hybrid control
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float rotationSpeed = 10f;

    private NavMeshAgent agent;
    private Animator animator;
    private AttackSystem attackSystem;  // CACHED for performance
    private Vector3 currentMoveVelocity;
    private bool isWASDActive = false;

    // DEBUG: Track stoppingDistance to detect corruption
    private float expectedStoppingDistance = 0f;
    private float lastStoppingDistanceCheck = 0f;
    private const float STOPPING_DISTANCE_CHECK_INTERVAL = 0.2f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        attackSystem = GetComponent<AttackSystem>();  // Cache once in Awake()

        // Configure NavMeshAgent
        if (agent != null)
        {
            agent.speed = moveSpeed;
            agent.acceleration = acceleration;
            agent.angularSpeed = 360f; // Fast but not crazy (was 1000!)

            // CRITICAL FIX: Stopping distance based on attack range
            // Get attack range from AttackSystem for natural combat spacing
            if (attackSystem != null)
            {
                // Stop at 75% of attack range (prevents body penetration)
                agent.stoppingDistance = attackSystem.attackRange * 0.75f;
                expectedStoppingDistance = agent.stoppingDistance; // Store for validation
                Debug.Log($"<color=lime>[CharacterMovement] stoppingDistance = {agent.stoppingDistance:F2}m (attackRange={attackSystem.attackRange}m * 0.75)</color>");
            }
            else
            {
                agent.stoppingDistance = 1.5f; // Fallback if no AttackSystem
                expectedStoppingDistance = agent.stoppingDistance;
                Debug.Log("<color=yellow>[CharacterMovement] No AttackSystem found, using default stoppingDistance=1.5m</color>");
            }

            agent.autoBraking = true;

            // DISABLE auto-rotation - we handle rotation manually
            // This prevents NavMesh from interfering and causing "muter-muter"
            agent.updateRotation = false;

            // MASSIVE WARFARE: ZERO avoidance (Albion Online style)
            // Walk-through OK, no pushing, pure NavMesh pathfinding only
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

            Debug.Log($"<color=green>[CharacterMovement]</color> NavMeshAgent configured: updateRotation={agent.updateRotation}, angularSpeed={agent.angularSpeed}, avoidance=NONE (massive warfare)");
        }

        // Configure Animator - CRITICAL FIX!
        if (animator != null)
        {
            // DISABLE root motion - animation should NOT override transform position/rotation
            // Root motion = animation controls movement (for cutscenes, NOT gameplay)
            animator.applyRootMotion = false;

            Debug.Log($"<color=green>[CharacterMovement]</color> Animator configured: applyRootMotion={animator.applyRootMotion}");
        }
        else
        {
            Debug.LogError("CharacterMovement: Animator component not found! Animations will not play.");
        }
    }

    void Update()
    {
        HandleWASDMovement();
        UpdateAnimator();

        // DEBUG: Validate stoppingDistance hasn't been corrupted
        ValidateStoppingDistance();
    }

    void HandleWASDMovement()
    {
        // CRITICAL: Check if currently attacking - BLOCK all movement
        bool isAttacking = attackSystem != null && attackSystem.IsAttacking();

        if (isAttacking)
        {
            // LOCKED during attack - ignore all movement input
            isWASDActive = false;
            currentMoveVelocity = Vector3.zero;
            if (agent != null && agent.isOnNavMesh)
            {
                agent.velocity = Vector3.zero;  // Force stop
            }
            return; // Skip all movement logic below
        }

        // Get input (WASD)
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D
        float vertical = Input.GetAxisRaw("Vertical");     // W/S

        // Calculate movement direction relative to world space
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (inputDirection.magnitude > 0.1f)
        {
            // CANCELLABLE ATTACK: WASD input cancels attack (Dota/Albion standard)
            if (attackSystem != null && attackSystem.IsAttacking())
            {
                attackSystem.CancelAttack();
            }

            // WASD is active - cancel any click-to-move pathfinding
            isWASDActive = true;

            if (agent != null && agent.isOnNavMesh)
            {
                // Stop pathfinding, manual control
                agent.ResetPath();

                // Smooth acceleration
                Vector3 targetVelocity = inputDirection * moveSpeed;
                currentMoveVelocity = Vector3.Lerp(currentMoveVelocity, targetVelocity, acceleration * Time.deltaTime);

                // Move via NavMeshAgent
                agent.velocity = currentMoveVelocity;
            }
        }
        else
        {
            // No WASD input
            isWASDActive = false;

            // Decelerate
            currentMoveVelocity = Vector3.Lerp(currentMoveVelocity, Vector3.zero, acceleration * Time.deltaTime);

            // If not pathfinding (click-to-move), apply deceleration
            if (agent != null && agent.isOnNavMesh && !agent.hasPath)
            {
                agent.velocity = currentMoveVelocity;
            }
        }

        // ROTATION LOGIC - Handle BOTH WASD and click-to-move
        // SKIP rotation if currently attacking (AttackSystem handles rotation)
        // NOTE: attackSystem already declared at top of function (line 88)

        if (!isAttacking && agent != null)
        {
            // Use agent.velocity (includes pathfinding velocity)
            // IMPORTANT: 0.5f threshold prevents rotation jitter when stopped
            float velocity = agent.velocity.magnitude;

            if (velocity > 0.5f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(agent.velocity);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        // When attacking OR velocity low: LOCK rotation - prevents "muter-muter" and camera jerk
    }

    void UpdateAnimator()
    {
        if (animator == null || agent == null) return;

        // Get actual velocity from NavMeshAgent (works for both WASD and click-to-move)
        float speed = agent.velocity.magnitude;

        // Update animator Speed parameter for Idle/Walk/Run blending
        // Speed values: <0.1=Idle, 0.1-3.0=Walk, >3.0=Run
        animator.SetFloat("Speed", speed);
    }

    // For external systems to check if player is moving
    public float GetCurrentSpeed()
    {
        return agent != null ? agent.velocity.magnitude : 0f;
    }

    // Check if WASD is currently controlling movement
    public bool IsWASDActive()
    {
        return isWASDActive;
    }

    // DEBUG: Validate that stoppingDistance hasn't been corrupted by other systems
    void ValidateStoppingDistance()
    {
        if (agent == null || !agent.isOnNavMesh) return;

        // Check at intervals
        if (Time.time - lastStoppingDistanceCheck < STOPPING_DISTANCE_CHECK_INTERVAL) return;
        lastStoppingDistanceCheck = Time.time;

        // Validate stoppingDistance
        float currentStoppingDistance = agent.stoppingDistance;
        if (Mathf.Abs(currentStoppingDistance - expectedStoppingDistance) > 0.01f)
        {
            Debug.LogError($"<color=red>🚨🚨🚨 [STOPPING DISTANCE CORRUPTION DETECTED!]</color>\n" +
                          $"  Expected: {expectedStoppingDistance:F3}m\n" +
                          $"  Current:  {currentStoppingDistance:F3}m\n" +
                          $"  SOMETHING OVERWROTE stoppingDistance!\n" +
                          $"  Stack trace will show culprit:", this);

            // Auto-fix corruption
            agent.stoppingDistance = expectedStoppingDistance;
            Debug.Log($"<color=yellow>  Auto-fixed to {expectedStoppingDistance:F3}m</color>");
        }
    }
}
