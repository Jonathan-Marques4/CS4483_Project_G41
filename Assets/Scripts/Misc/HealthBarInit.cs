using UnityEngine;

// Auto-wires the HealthBar to the parent HealthComponent at runtime
public class HealthBarInit : MonoBehaviour
{
    void Start()
    {
        var hc = transform.parent.GetComponent<HealthComponent>();
        var hb = GetComponent<HealthBar>();
        if (hc != null && hb != null)
            hb.Init(hc);
    }
}