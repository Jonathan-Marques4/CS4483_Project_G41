using UnityEngine;
using System.Collections;

public class WeaponVisualController : MonoBehaviour
{
    // Offsets from player center in world space — tweak in Inspector
    [SerializeField] private Vector3 offsetDown  = new Vector3( 0.26f, -0.11f, 0f);
    [SerializeField] private Vector3 offsetUp    = new Vector3( 0.26f,  0.08f, 0f);
    [SerializeField] private Vector3 offsetSide  = new Vector3( 0.38f, -0.08f, 0f);

    [SerializeField] private float weaponScale   = 1.05f;
    [SerializeField] private float swingDuration = 0.25f;
    [SerializeField] private float swingAngle    = 120f;

    [SerializeField] private SimpleTopDownAnimator topDownAnimator;

    private SpriteRenderer weaponRenderer;
    private ItemData lastEquipped;
    private float swingRotationOffset = 0f;

    void Start()
    {
        // Find WeaponHolder anywhere in children
        foreach (var t in transform.GetComponentsInChildren<Transform>(true))
        {
            if (t.name == "WeaponHolder")
            {
                weaponRenderer = t.GetComponent<SpriteRenderer>();
                if (weaponRenderer == null)
                    weaponRenderer = t.gameObject.AddComponent<SpriteRenderer>();
                weaponRenderer.sortingLayerName = "Player";
                weaponRenderer.sortingOrder = 10;
                weaponRenderer.enabled = false;
                t.localScale = Vector3.one * weaponScale;
                break;
            }
        }

        if (weaponRenderer == null)
            Debug.LogWarning("WeaponVisualController: WeaponHolder not found under " + gameObject.name);

        if (topDownAnimator == null)
            topDownAnimator = GetComponent<SimpleTopDownAnimator>();
    }

    public void PlaySwing(Vector2 facing)
    {
        if (weaponRenderer == null || !weaponRenderer.enabled) return;
        StopAllCoroutines();
        StartCoroutine(SwingCoroutine(facing));
    }

    private IEnumerator SwingCoroutine(Vector2 facing)
    {
        float direction = (Mathf.Abs(facing.x) > Mathf.Abs(facing.y))
            ? (facing.x >= 0 ? 1f : -1f)
            : (facing.y > 0 ? -1f : 1f);

        float elapsed = 0f;
        while (elapsed < swingDuration)
        {
            float t = elapsed / swingDuration;
            swingRotationOffset = direction * swingAngle * Mathf.Sin(t * Mathf.PI);
            elapsed += Time.deltaTime;
            yield return null;
        }
        swingRotationOffset = 0f;
    }

    void Update()
    {
        if (weaponRenderer == null || InventoryManager.Instance == null) return;

        var slot = InventoryManager.Instance.GetSelectedHotbarSlot();
        bool hasSword = slot != null && !slot.IsEmpty()
                        && slot.item != null
                        && slot.item.itemType == ItemType.Weapon;

        if (!hasSword)
        {
            weaponRenderer.enabled = false;
            lastEquipped = null;
            return;
        }

        weaponRenderer.enabled = true;

        if (slot.item != lastEquipped)
        {
            lastEquipped = slot.item;
            weaponRenderer.sprite = slot.item.icon;
        }

        Vector2 dir = topDownAnimator != null ? topDownAnimator.LastDirection : Vector2.down;

        // Determine world-space offset and tip direction
        Vector3 worldOffset;
        Vector2 tipDir;
        bool flipX = false;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            bool right = dir.x > 0;
            worldOffset = new Vector3((right ? 1f : -1f) * offsetSide.x, offsetSide.y, 0f);
            tipDir  = right ? Vector2.right : Vector2.left;
            flipX   = !right;
        }
        else if (dir.y > 0)
        {
            worldOffset = offsetUp;
            tipDir = Vector2.up;
        }
        else
        {
            worldOffset = offsetDown;
            tipDir = Vector2.down;
        }

        // World-space position — bypasses all parent transform issues
        weaponRenderer.transform.position = transform.position + worldOffset;

        // World-space rotation: rotate the sprite so its local +Y (tip) points toward tipDir
        // atan2 gives angle of tipDir from +X axis; subtract 90° because sprite tip is at +Y (not +X)
        float angle = Mathf.Atan2(tipDir.y, tipDir.x) * Mathf.Rad2Deg + 90f + swingRotationOffset;
        weaponRenderer.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        weaponRenderer.flipX = flipX;
    }
}
