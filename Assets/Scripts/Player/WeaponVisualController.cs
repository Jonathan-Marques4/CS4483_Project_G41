using UnityEngine;
using System.Collections;

public class WeaponVisualController : MonoBehaviour
{
    [Header("Offsets - TUNE THESE IN INSPECTOR")]
    [Tooltip("X = distance from visual body center to pommel. Make this the exact distance so pommel sits just outside hitbox.")]
    [SerializeField] private Vector3 offsetDown = new Vector3( 0.65f, -0.25f, 0f);
    [SerializeField] private Vector3 offsetUp   = new Vector3( 0.65f,  0.35f, 0f);
    [SerializeField] private Vector3 offsetSide = new Vector3( 0.80f, -0.10f, 0f);   // ← Start here. Increase X = farther from body

    [Header("Weapon Settings")]
    [SerializeField] private float weaponScale   = 1.05f;
    [SerializeField] private float swingDuration = 0.25f;
    [SerializeField] private float swingAngle    = 120f;

    [SerializeField] private SimpleTopDownAnimator topDownAnimator;

    private SpriteRenderer weaponRenderer;
    private Transform weaponHolder;
    private SpriteRenderer charRenderer;   // ONLY used to read current visual center (bounds)
    private ItemData lastEquipped;
    private float swingRotationOffset = 0f;

    void Start()
    {
        foreach (var t in transform.GetComponentsInChildren<Transform>(true))
        {
            if (t.name == "WeaponHolder")
            {
                weaponHolder = t;
                weaponRenderer = t.GetComponent<SpriteRenderer>();
                if (weaponRenderer == null)
                    weaponRenderer = t.gameObject.AddComponent<SpriteRenderer>();

                weaponRenderer.sortingLayerName = "Player";
                weaponRenderer.sortingOrder = 10;
                weaponRenderer.enabled = false;
                weaponHolder.localScale = Vector3.one * weaponScale;
            }
            else if (t.name == "Sprite" && charRenderer == null)
            {
                charRenderer = t.GetComponent<SpriteRenderer>();
            }
        }

        if (weaponHolder == null)
            Debug.LogError("WeaponHolder child not found! (must exist under player)");

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
        if (slot == null || slot.IsEmpty() || slot.item?.itemType != ItemType.Weapon)
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

        Vector3 worldOffset = Vector3.zero;
        Vector2 tipDir = Vector2.down;
        bool weaponFlipX = false;

        // ==================== THIS IS THE FIX ====================
        bool facingRight = dir.x > 0;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))   // LEFT / RIGHT
        {
            // Get the CURRENT visual center of the character sprite (already accounts for flipX)
            float visualCenterX = 0f;
            if (charRenderer != null)
            {
                visualCenterX = charRenderer.bounds.center.x - transform.position.x;
            }

            // Place sword pommel at: visualCenter + facing offset
            // This makes the distance from body to pommel IDENTICAL on left and right
            float sideX = facingRight ? offsetSide.x : -offsetSide.x;

            worldOffset = new Vector3(visualCenterX + sideX, offsetSide.y, 0f);

            tipDir = facingRight ? Vector2.right : Vector2.left;
            weaponFlipX = !facingRight;
        }
        else if (dir.y > 0)   // UP
        {
            worldOffset = offsetUp;
            tipDir = Vector2.up;
        }
        else                  // DOWN
        {
            worldOffset = offsetDown;
            tipDir = Vector2.down;
        }

        // Position in world space (most reliable method)
        weaponRenderer.transform.position = transform.position + worldOffset;

        // Rotation so tip points in facing direction
        float angle = Mathf.Atan2(tipDir.y, tipDir.x) * Mathf.Rad2Deg + 90f + swingRotationOffset;
        weaponRenderer.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        weaponRenderer.flipX = weaponFlipX;
    }
}