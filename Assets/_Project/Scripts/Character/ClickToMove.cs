using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Albion-style point-and-click movement with NavMesh
/// - Right-click ground to move
/// - Green hover indicator shows target position
/// - Integrates with existing WASD movement
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class ClickToMove : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Layer mask for ground/terrain (what can be clicked)")]
    public LayerMask groundLayer = -1; // Everything by default
    
    [Tooltip("Maximum click distance from camera")]
    public float maxClickDistance = 100f;

    [Header("Ground Hover Indicator")]
    [Tooltip("Green circle that shows where you'll move when clicking")]
    public GameObject hoverIndicator;

    [Tooltip("How high above ground to position indicator")]
    public float indicatorHeight = 0.1f;

    [Header("Monster Targeting")]
    [Tooltip("Layer mask for monsters/enemies")]
    public LayerMask monsterLayer = -1;

    [Tooltip("Monster prefab to spawn for testing (TEMP)")]
    public GameObject monsterPrefab;

    private GameObject currentClickTarget;

    private NavMeshAgent agent;
    private Camera mainCamera;
    private AttackSystem attackSystem;  // CACHED for performance
    private bool isHoveringGround = false;
    private Vector3 hoverPosition;

    // Hover tracking
    private GameObject currentHoveredMonster = null;
    private GameObject hoverRing = null; // Temporary hover visual (red)
    private bool isHoveringMonster = false; // Flag untuk block ground hover

    // Selection tracking (persistent)
    private GameObject currentSelectedMonster = null;
    private GameObject selectionRing = null; // Persistent selection visual (yellow)

    // DEBUG: Track movement state for body penetration investigation
    private float lastDistanceLog = 0f;
    private const float DISTANCE_LOG_INTERVAL = 0.1f; // Log every 0.1 seconds

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        attackSystem = GetComponent<AttackSystem>();  // Cache once in Awake()

        // Create hover indicator if not assigned
        if (hoverIndicator == null)
        {
            CreateHoverIndicator();
        }

        // Hide indicator initially
        if (hoverIndicator != null)
        {
            hoverIndicator.SetActive(false);
        }
    }

    void Start()
    {
        // AUTO-SPAWN DISABLED - User only wants monsters from TestMonsterSpawner (SPACE key)
        // The unwanted Hell Golem Warrior was spawning here
    }

    void Update()
    {
        // CRITICAL: Block all input during attack animation
        bool isAttacking = attackSystem != null && attackSystem.IsAttacking();

        if (isAttacking)
        {
            // Hide indicators during attack
            if (hoverIndicator != null)
            {
                hoverIndicator.SetActive(false);
            }
            return; // Ignore all input during attack
        }

        HandleMonsterHover(); // Check monster hover first (priority)
        HandleGroundHover();
        HandleLeftClickSelect(); // Left-click = select target only
        HandleClickToMove(); // Right-click = move/attack
        // HandleFaceTarget() REMOVED - CharacterMovement already handles rotation correctly!

        // DEBUG: Frame-by-frame tracking of movement to catch body penetration
        TrackMovementState();
    }

    void HandleLeftClickSelect()
    {
        // Left-click to select target (Albion style - just highlight, no movement)
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, maxClickDistance);

            // Check for monster
            foreach (var hit in hits)
            {
                GameObject hitObject = hit.collider.gameObject;
                bool hasAnimator = hitObject.GetComponent<Animator>() != null;
                bool hasMonsterName = hitObject.name.Contains("HellGolem") ||
                                      hitObject.name.Contains("Monster") ||
                                      hitObject.name.Contains("TestMonster");

                if (hasAnimator && hasMonsterName)
                {
                    // Selected monster - create PERSISTENT selection circle
                    Debug.Log($"<color=cyan>✓ SELECTED:</color> {hitObject.name}");

                    // Clear old selection
                    ClearSelectionCircle();

                    // Set new selection
                    currentSelectedMonster = hitObject;
                    currentClickTarget = hitObject;
                    CreateSelectionCircle(hitObject);

                    // Show monster name - DISABLED (MonsterNameDisplay removed)
                    // MonsterNameDisplay nameDisplay = hitObject.GetComponent<MonsterNameDisplay>();
                    // if (nameDisplay != null)
                    // {
                    //     nameDisplay.ShowNameUI();
                    // }

                    return;
                }
            }

            // Clicked ground/nothing - deselect
            if (currentClickTarget != null)
            {
                Debug.Log("<color=yellow>✗ Deselected target</color>");

                // Hide monster name - DISABLED (MonsterNameDisplay removed)
                // if (currentSelectedMonster != null)
                // {
                //     MonsterNameDisplay nameDisplay = currentSelectedMonster.GetComponent<MonsterNameDisplay>();
                //     if (nameDisplay != null)
                //     {
                //         nameDisplay.HideNameUI();
                //     }
                // }

                ClearSelectionCircle();
                currentClickTarget = null;
                currentSelectedMonster = null;
            }
        }
    }

    void CreateSelectionCircle(GameObject target)
    {
        // Create PERSISTENT yellow circle (berbeda dari hover red)
        selectionRing = new GameObject("SelectionRing");
        selectionRing.transform.position = target.transform.position + Vector3.up * 0.06f; // Slightly higher
        selectionRing.transform.SetParent(target.transform); // Follow monster

        LineRenderer line = selectionRing.AddComponent<LineRenderer>();

        // Circle settings - same size as hover
        int segments = 32;
        float radius = 0.8f;
        line.positionCount = segments + 1;
        line.useWorldSpace = false;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * Mathf.PI * 2f / segments;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            line.SetPosition(i, new Vector3(x, 0, z));
        }

        // YELLOW color untuk selection (berbeda dari red hover)
        line.startWidth = 0.06f;
        line.endWidth = 0.06f;
        line.startColor = new Color(1f, 0.9f, 0.2f, 0.9f); // YELLOW/GOLD
        line.endColor = new Color(1f, 0.9f, 0.2f, 0.9f);

        Material mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        mat.color = new Color(1f, 0.9f, 0.2f);
        line.material = mat;
    }

    void ClearSelectionCircle()
    {
        if (selectionRing != null)
        {
            Destroy(selectionRing);
            selectionRing = null;
        }
    }

    void HandleMonsterHover()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, maxClickDistance); // Get ALL hits

        // Debug: log semua hits
        if (hits.Length > 0 && Input.GetKeyDown(KeyCode.Space)) // Press Space untuk debug
        {
            Debug.Log($"<color=cyan>RAYCASTPressing HITS: {hits.Length} objects</color>");
            foreach (var h in hits)
            {
                Debug.Log($"  - {h.collider.gameObject.name} (layer: {LayerMask.LayerToName(h.collider.gameObject.layer)})");
            }
        }

        // Check all hits untuk monster (prioritas tinggi)
        foreach (var hit in hits)
        {
            GameObject hitObject = hit.collider.gameObject;

            // Check if it's a monster
            bool hasAnimator = hitObject.GetComponent<Animator>() != null;
            bool hasMonsterName = hitObject.name.Contains("HellGolem") ||
                                  hitObject.name.Contains("Monster") ||
                                  hitObject.name.Contains("TestMonster");

            if (hasAnimator && hasMonsterName)
            {
                // Hovering over monster - highlight it
                isHoveringMonster = true; // SET FLAG - block ground hover

                if (currentHoveredMonster != hitObject)
                {
                    ClearMonsterHighlight();
                    currentHoveredMonster = hitObject;
                    HighlightMonster(hitObject);
                }
                return; // STOP processing - monster found
            }
        }

        // Not hovering monster - clear highlight
        isHoveringMonster = false; // CLEAR FLAG - allow ground hover
        if (currentHoveredMonster != null)
        {
            ClearMonsterHighlight();
        }
    }

    void HighlightMonster(GameObject monster)
    {
        // Create OUTLINE CIRCLE using LineRenderer (subtle border aja)
        hoverRing = new GameObject("HoverRing_Outline");
        hoverRing.transform.position = monster.transform.position + Vector3.up * 0.05f;

        // PARENT to monster - so it follows automatically (no lag!)
        hoverRing.transform.SetParent(monster.transform);

        LineRenderer line = hoverRing.AddComponent<LineRenderer>();

        // Circle settings - MUCH SMALLER, tight around monster feet
        int segments = 32;
        float radius = 0.8f; // SMALLER! (was 1.2, masih kegedean)
        line.positionCount = segments + 1;
        line.useWorldSpace = false;

        // Create circle points
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * Mathf.PI * 2f / segments;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            line.SetPosition(i, new Vector3(x, 0, z));
        }

        // Line appearance - BRIGHT RED, thin outline (tetep merah terang!)
        line.startWidth = 0.05f; // Thin border
        line.endWidth = 0.05f;
        line.startColor = new Color(1f, 0.15f, 0.15f, 0.85f); // BRIGHT RED, high opacity
        line.endColor = new Color(1f, 0.15f, 0.15f, 0.85f);

        // Material - bright red
        Material mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        mat.color = new Color(1f, 0.15f, 0.15f); // BRIGHT RED
        line.material = mat;

        Debug.Log($"<color=yellow>✓ HOVERING:</color> {monster.name}");
    }

    void ClearMonsterHighlight()
    {
        if (hoverRing != null)
        {
            Destroy(hoverRing);
            hoverRing = null;
        }

        currentHoveredMonster = null;
    }

    void HandleGroundHover()
    {
        // SKIP ground hover if hovering monster
        if (isHoveringMonster)
        {
            isHoveringGround = false;
            if (hoverIndicator != null)
            {
                hoverIndicator.SetActive(false);
            }
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxClickDistance, groundLayer))
        {
            // Mouse is hovering over ground
            isHoveringGround = true;
            hoverPosition = hit.point;

            // Show and position indicator
            if (hoverIndicator != null)
            {
                hoverIndicator.SetActive(true);
                hoverIndicator.transform.position = hoverPosition + Vector3.up * indicatorHeight;
            }
        }
        else
        {
            // Not hovering ground
            isHoveringGround = false;

            if (hoverIndicator != null)
            {
                hoverIndicator.SetActive(false);
            }
        }
    }

    void HandleClickToMove()
    {
        // Right-click to move or target (Albion/MMORPG standard)
        if (Input.GetMouseButtonDown(1)) // Right-click
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, maxClickDistance); // Get ALL hits

            // PRIORITY: Check for monster first
            foreach (var hit in hits)
            {
                GameObject hitObject = hit.collider.gameObject;

                bool hasAnimator = hitObject.GetComponent<Animator>() != null;
                bool hasMonsterName = hitObject.name.Contains("HellGolem") ||
                                      hitObject.name.Contains("Monster") ||
                                      hitObject.name.Contains("TestMonster");

                if (hasAnimator && hasMonsterName)
                {
                    // Clicked on monster - target it
                    TargetMonster(hitObject, hit.point);
                    return; // DONE - don't process ground click
                }
            }

            // No monster clicked - check ground
            if (hits.Length > 0)
            {
                // Use first hit (usually ground)
                var groundHit = hits[0];

                // Clicked ground - just move
                MoveToPosition(groundHit.point);
                CreateClickEffect(groundHit.point);

                // Clear target if moving away
                if (currentClickTarget != null)
                {
                    currentClickTarget = null;
                    agent.stoppingDistance = 0f; // Reset stopping distance
                }
            }
        }
    }

    void MoveToPosition(Vector3 targetPosition)
    {
        // CANCELLABLE ATTACK: Right-click move cancels attack (Dota/Albion standard)
        if (attackSystem != null && attackSystem.IsAttacking())
        {
            attackSystem.CancelAttack();
        }

        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(targetPosition);

            // Debug: log distance and path info
            float distance = Vector3.Distance(transform.position, targetPosition);
            Debug.Log($"<color=cyan>Click-to-Move:</color> Target={targetPosition}, Distance={distance:F2} units");

            // Wait a frame then check path
            StartCoroutine(CheckPathAfterFrame(targetPosition, distance));
        }
        else
        {
            Debug.LogWarning("ClickToMove: NavMeshAgent not on NavMesh! Did you bake NavMesh?");
        }
    }

    System.Collections.IEnumerator CheckPathAfterFrame(Vector3 targetPosition, float clickDistance)
    {
        yield return null; // Wait one frame for path calculation

        if (agent != null && agent.hasPath)
        {
            float pathLength = agent.remainingDistance;
            Debug.Log($"<color=yellow>Path Info:</color> ClickDist={clickDistance:F2}, PathLength={pathLength:F2}, HasPath={agent.hasPath}");

            if (pathLength < clickDistance * 0.5f)
            {
                Debug.LogWarning($"<color=red>PATH ISSUE:</color> Path only {pathLength:F2} units but clicked {clickDistance:F2} units away!");
                Debug.LogWarning("Possible causes: NavMesh holes, obstacles, or NavMesh not covering full ground");
            }
        }
        else
        {
            Debug.LogWarning("<color=red>No valid path found!</color>");
        }
    }

    void CreateClickEffect(Vector3 position)
    {
        // Simple ring pulse effect (MMORPG-style)
        GameObject ring = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        ring.name = "ClickEffect";
        ring.transform.position = position + Vector3.up * 0.05f;
        ring.transform.localScale = new Vector3(0.5f, 0.01f, 0.5f);

        // Remove collider
        Collider col = ring.GetComponent<Collider>();
        if (col != null) Destroy(col);

        // White semi-transparent material
        Renderer renderer = ring.GetComponent<Renderer>();
        if (renderer != null)
        {
            Shader shader = Shader.Find("Universal Render Pipeline/Unlit");
            if (shader == null) shader = Shader.Find("Unlit/Color");

            Material mat = new Material(shader);
            mat.color = new Color(1f, 1f, 1f, 0.6f); // White, 60% opacity

            mat.SetOverrideTag("RenderType", "Transparent");
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.renderQueue = 3000;

            renderer.material = mat;
        }

        // Destroy after 0.5 seconds
        Destroy(ring, 0.5f);
    }

    void TargetMonster(GameObject monster, Vector3 clickPosition)
    {
        // Target existing monster (don't destroy/respawn)
        currentClickTarget = monster;

        // Set destination with stopping distance for melee attack range
        if (agent != null && agent.isOnNavMesh)
        {
            float currentDist = Vector3.Distance(transform.position, monster.transform.position);
            float attackRange = 1.8f; // Same as AttackSystem.attackRange

            // CRITICAL FIX: Only move if OUT OF RANGE
            // DON'T touch stoppingDistance - CharacterMovement already set it correctly (1.35m)
            if (currentDist > attackRange)
            {
                // Out of range - move closer
                agent.SetDestination(monster.transform.position);
                Debug.Log($"<color=cyan>✓ TARGETED:</color> {monster.name} - MOVING CLOSER (currentDist={currentDist:F2}, attackRange={attackRange})");
            }
            else
            {
                // Already in range - STOP, no new destination
                agent.ResetPath(); // Clear any existing path
                Debug.Log($"<color=lime>✓ TARGETED:</color> {monster.name} - ALREADY IN RANGE (currentDist={currentDist:F2}, attackRange={attackRange}) - NO MOVE</color>");
            }
        }

        // Visual feedback at click position
        CreateClickEffect(clickPosition);
    }

    // TEMP DEBUG: Spawn test monster (ganti jadi proper monster spawning system nanti)
    [ContextMenu("Debug - Spawn Test Monster")]
    void SpawnTestMonster()
    {
        if (monsterPrefab == null)
        {
            Debug.LogWarning("No monster prefab assigned!");
            return;
        }

        // Spawn monster 5 units in front of player
        Vector3 spawnPos = transform.position + transform.forward * 5f;

        GameObject monster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
        monster.name = "TestMonster_HellGolem";

        Debug.Log($"<color=green>Test monster spawned at {spawnPos}</color>");
    }

    void CreateHoverIndicator()
    {
        // Create small subtle green cube (MMORPG standard)
        GameObject indicator = GameObject.CreatePrimitive(PrimitiveType.Cube);
        indicator.name = "GroundHoverIndicator";
        indicator.transform.SetParent(null); // Keep in scene root

        // Tiny size: cursor-sized, very thin/flat
        indicator.transform.localScale = new Vector3(0.25f, 0.03f, 0.25f);

        // Remove collider (don't want it blocking raycasts)
        Collider col = indicator.GetComponent<Collider>();
        if (col != null) Destroy(col);

        // Subtle green material (light glow effect)
        Renderer renderer = indicator.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Try URP Unlit shader first, fallback to Unlit/Color
            Shader shader = Shader.Find("Universal Render Pipeline/Unlit");
            if (shader == null) shader = Shader.Find("Unlit/Color");

            Material mat = new Material(shader);

            // Super subtle pale green - barely visible light effect (MMORPG standard)
            mat.color = new Color(0.5f, 1f, 0.5f, 0.12f); // Very pale green, 12% opacity

            // Properly enable transparency for URP
            mat.SetOverrideTag("RenderType", "Transparent");
            mat.SetFloat("_Surface", 1); // 0=Opaque, 1=Transparent (URP specific)
            mat.SetFloat("_Blend", 0); // 0=Alpha, 1=Premultiply, 2=Additive, 3=Multiply
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.SetInt("_AlphaClip", 0);
            mat.renderQueue = 3000;
            mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");

            renderer.material = mat;
        }

        hoverIndicator = indicator;
        hoverIndicator.SetActive(false);

        Debug.Log("ClickToMove: Created subtle green hover indicator (0.6 diameter)");
    }

    // Public method to check if agent is moving (for animation sync)
    public bool IsMoving()
    {
        return agent != null && agent.velocity.magnitude > 0.1f;
    }

    // Get current movement speed for animations
    public float GetCurrentSpeed()
    {
        return agent != null ? agent.velocity.magnitude : 0f;
    }

    // Get current click target (for AttackSystem)
    public GameObject GetCurrentTarget()
    {
        return currentClickTarget;
    }

    // DEBUG: Track movement state every frame to catch body penetration
    void TrackMovementState()
    {
        // Only log if we have a target and agent is pathfinding
        if (currentClickTarget == null || agent == null || !agent.isOnNavMesh) return;

        // Log at intervals to avoid spam
        if (Time.time - lastDistanceLog < DISTANCE_LOG_INTERVAL) return;
        lastDistanceLog = Time.time;

        // Calculate distances
        float distanceToTarget = Vector3.Distance(transform.position, currentClickTarget.transform.position);
        float remainingDistance = agent.remainingDistance;
        float stoppingDistance = agent.stoppingDistance;
        float velocity = agent.velocity.magnitude;
        bool hasPath = agent.hasPath;

        // CRITICAL: Log if we're getting TOO CLOSE (body penetration warning)
        if (distanceToTarget < 1.0f)
        {
            Debug.Log($"<color=red>⚠️⚠️⚠️ [BODY PENETRATION WARNING]</color>\n" +
                     $"  Distance to target: {distanceToTarget:F3}m (< 1.0m!)\n" +
                     $"  stoppingDistance: {stoppingDistance:F3}m\n" +
                     $"  remainingDistance: {remainingDistance:F3}m\n" +
                     $"  velocity: {velocity:F2}\n" +
                     $"  hasPath: {hasPath}\n" +
                     $"  Target: {currentClickTarget.name}");
        }
        // Normal tracking when moving toward target
        else if (hasPath && velocity > 0.1f)
        {
            Debug.Log($"<color=cyan>[Movement Tracking]</color>\n" +
                     $"  Distance to target: {distanceToTarget:F3}m\n" +
                     $"  stoppingDistance: {stoppingDistance:F3}m\n" +
                     $"  remainingDistance: {remainingDistance:F3}m\n" +
                     $"  velocity: {velocity:F2}");
        }
    }
}
