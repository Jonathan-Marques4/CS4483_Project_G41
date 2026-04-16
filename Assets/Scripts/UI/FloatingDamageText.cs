using TMPro;
using UnityEngine;

public class FloatingDamageText : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 1.5f;
    [SerializeField] private float fadeDuration = 0.8f;
    [SerializeField] private float horizontalDrift = 0.3f;

    private TMP_Text label;
    private float elapsed;
    private Color startColor;
    private float driftDir;

    // Called directly from Enemy.TakeDamage — no event subscription needed
    public static void Spawn(Vector3 worldPos, float damage)
    {
        var prefab = Resources.Load<GameObject>("FloatingDamageText");
        if (prefab == null)
        {
            Debug.LogError("FloatingDamageText: prefab not found in Resources folder!");
            return;
        }
        var offset = new Vector3(Random.Range(-0.3f, 0.3f), 0.5f, 0f);
        var go = Instantiate(prefab, worldPos + offset, Quaternion.identity);
        go.GetComponent<FloatingDamageText>()?.SetDamage(damage);
    }

    void Awake()
    {
        label = GetComponent<TMP_Text>();
        startColor = label.color;
        driftDir = Random.value > 0.5f ? 1f : -1f;

        // Force render on top of all sprites
        var mr = GetComponent<MeshRenderer>();
        if (mr != null)
        {
            mr.sortingLayerName = "Crops";
            mr.sortingOrder = 100;
        }
    }

    public void SetDamage(float amount)
    {
        label.text = $"-{Mathf.RoundToInt(amount)} hp";
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        transform.position += new Vector3(
            driftDir * horizontalDrift * Time.deltaTime * 0.5f,
            floatSpeed * Time.deltaTime,
            0f
        );

        float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
        label.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

        if (elapsed >= fadeDuration)
            Destroy(gameObject);
    }
}
