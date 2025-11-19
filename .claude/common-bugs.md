# Common Bugs & Fixes - Triwars Online

## Movement & Rotation Issues

### Bug: Player penetrates monster body (menyatu)
**Cause:** NavMeshAgent doesn't respect regular Colliders, only NavMeshObstacle
**Fix:** Add `NavMeshObstacle` component to spawned monsters with `carving = true`
```csharp
NavMeshObstacle obstacle = monster.AddComponent<NavMeshObstacle>();
obstacle.carving = true;
obstacle.shape = NavMeshObstacleShape.Capsule;
obstacle.radius = 1.8f;
```

### Bug: Player auto-rotates (muter-muter) when reaching destination
**Cause:** `Animator.applyRootMotion = true` - animation overrides transform rotation
**Fix:** Disable root motion in CharacterMovement.Awake()
```csharp
animator.applyRootMotion = false;
```
**Also disable:** `agent.updateRotation = false` (prevent NavMesh rotation interference)

### Bug: Player doesn't face target when in attack range
**Cause:** No rotation logic when stopped at destination
**Fix:** Add HandleFaceTarget() to continuously face target when in range
```csharp
if (reachedTarget) {
    Vector3 dir = (target.position - transform.position).normalized;
    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);
}
```

## Visual Issues

### Bug: Hover/selection circle too large or wrong color
**Cause:** LineRenderer radius/color not proportional to monster size
**Fix:** Use radius 0.8-1.0 for monster feet, bright colors with high opacity (0.8-0.9)
- Hover = RED (temporary)
- Selection = YELLOW (persistent)

## Unity Setup Issues

### Bug: GLB files not loading (AssetDatabase returns null)
**Cause:** Unity doesn't have native GLB support
**Fix:** Install UnityGLTF package via Git URL
```bash
https://github.com/KhronosGroup/UnityGLTF.git
```

### Bug: NavMeshAgent angularSpeed too high (1000) causing overshoot
**Cause:** `agent.angularSpeed = rotationSpeed * 100` (10 * 100 = 1000)
**Fix:** Set fixed reasonable value: `agent.angularSpeed = 360f`

## Weapon Attachment Issues

### Bug: Sword attaches from center/middle instead of handle
**Cause:** GLB model pivot point is at center of sword, not at handle
**Fix:** Adjust `positionOffset` and `rotationOffset` in WeaponAttachment component
**Correct defaults for this project's sword:**
```csharp
positionOffset = new Vector3(0.05f, 0f, 0.45f);
rotationOffset = new Vector3(90f, 0f, 0f);
scale = new Vector3(0.6f, 0.6f, 0.6f);
```
**Tip:** Enable `liveUpdate` and tweak in Inspector while in Play mode untuk real-time adjustment

---

## Architecture Mistakes (CRITICAL)

### FATAL: Recommending Built-in NavMesh Baking (2025-11-19)
**Mistake:** Recommended legacy built-in NavMesh approach (Window → AI → Navigation → Bake)
**Why Wrong:**
- Legacy system untuk single-scene games
- NOT scalable untuk MMORPG dengan multiple zones
- Can't do runtime updates (dynamic obstacles, doors)
- Can't have multiple agent types (flying vs ground)
- Can't instance dungeons with separate NavMesh

**Correct Solution:** NavMeshSurface component-based approach
- ✅ Modern Unity standard (2018+)
- ✅ Component-based = modular & scalable
- ✅ Per-zone NavMesh = tambah zone ga perlu rebake all
- ✅ Runtime baking support (dynamic content)
- ✅ Multiple surfaces = multiple agent types
- ✅ Perfect for MMORPG architecture

**Lesson:** "Simple" ≠ "Right". Architecture must be scalable from day 1.
**Impact:** Wrong architecture = months of rework later = technical debt

**Rule:** ALWAYS think: "Ini scale untuk MMORPG 50+ zones?" before recommending solution.

---

**Note:** Always check Animator.applyRootMotion and NavMeshAgent.updateRotation when debugging rotation issues!
