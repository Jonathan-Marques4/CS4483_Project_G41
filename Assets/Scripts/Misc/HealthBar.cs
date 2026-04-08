using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform fillRect;
    private Image fillImage;
    private HealthComponent health;

    public void Init(HealthComponent hc)
    {
        var fillTransform = transform.Find("Fill");
        if (fillTransform == null)
        {
            Debug.LogError("HealthBar: missing Fill child!", gameObject);
            return;
        }

        fillRect = fillTransform.GetComponent<RectTransform>();
        fillImage = fillTransform.GetComponent<Image>();

        health = hc;
        hc.OnDamaged += _ => UpdateBar();
        hc.OnHealed  += _ => UpdateBar();
        hc.OnDeath   += () => Destroy(gameObject);
        UpdateBar();
    }

    void UpdateBar()
    {
        if (health == null || fillRect == null) return;
        float pct = health.CurrentHealth / health.MaxHealth;

        // Resize the fill rect from left anchor instead of using fillAmount
        fillRect.anchorMin = new Vector2(0f, 0f);
        fillRect.anchorMax = new Vector2(pct, 1f);
        fillRect.offsetMin = Vector2.zero;
        fillRect.offsetMax = Vector2.zero;

        if (fillImage != null)
            fillImage.color = Color.Lerp(Color.red, Color.green, pct);
    }
}