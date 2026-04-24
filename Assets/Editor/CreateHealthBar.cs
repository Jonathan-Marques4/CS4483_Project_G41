using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;

public class CreateHealthBar
{
    [MenuItem("Tools/Setup Health Bar")]
    public static void Run()
    {
        var healthUI = Object.FindFirstObjectByType<HealthUI>();
        if (healthUI == null)
        {
            Debug.LogError("HealthUI not found in scene.");
            return;
        }

        Transform hud = healthUI.transform.parent;

        // Remove existing HealthBar if present
        var existing = hud.Find("HealthBar");
        if (existing != null)
            Object.DestroyImmediate(existing.gameObject);

        // ---- HealthBar root ----
        var barGO = new GameObject("HealthBar");
        barGO.layer = 5;
        var barRT = barGO.AddComponent<RectTransform>();
        barRT.SetParent(hud, false);
        barRT.anchorMin = new Vector2(0f, 1f);
        barRT.anchorMax = new Vector2(0f, 1f);
        barRT.pivot = new Vector2(0.5f, 0.5f);
        barRT.anchoredPosition = new Vector2(120f, -15f);
        barRT.sizeDelta = new Vector2(200f, 20f);

        // ---- Background ----
        var bgGO = new GameObject("Background");
        bgGO.layer = 5;
        var bgRT = bgGO.AddComponent<RectTransform>();
        bgRT.SetParent(barGO.transform, false);
        bgRT.anchorMin = Vector2.zero;
        bgRT.anchorMax = Vector2.one;
        bgRT.offsetMin = Vector2.zero;
        bgRT.offsetMax = Vector2.zero;
        var bgImg = bgGO.AddComponent<Image>();
        bgImg.color = new Color(0.2f, 0.2f, 0.2f, 1f);

        // ---- Fill Area ----
        var fillAreaGO = new GameObject("Fill Area");
        fillAreaGO.layer = 5;
        var fillAreaRT = fillAreaGO.AddComponent<RectTransform>();
        fillAreaRT.SetParent(barGO.transform, false);
        fillAreaRT.anchorMin = new Vector2(0f, 0.25f);
        fillAreaRT.anchorMax = new Vector2(1f, 0.75f);
        fillAreaRT.offsetMin = Vector2.zero;
        fillAreaRT.offsetMax = Vector2.zero;

        // ---- Fill ----
        var fillGO = new GameObject("Fill");
        fillGO.layer = 5;
        var fillRT = fillGO.AddComponent<RectTransform>();
        fillRT.SetParent(fillAreaGO.transform, false);
        fillRT.anchorMin = Vector2.zero;
        fillRT.anchorMax = new Vector2(1f, 1f);
        fillRT.offsetMin = Vector2.zero;
        fillRT.offsetMax = Vector2.zero;
        var fillImg = fillGO.AddComponent<Image>();
        fillImg.color = Color.red;

        // ---- Slider ----
        var slider = barGO.AddComponent<Slider>();
        slider.fillRect = fillRT;
        slider.handleRect = null;
        slider.direction = Slider.Direction.LeftToRight;
        slider.minValue = 0f;
        slider.maxValue = 100f;
        slider.wholeNumbers = true;
        slider.value = 100f;
        slider.interactable = false;

        // ---- Wire to HealthUI ----
        healthUI.healthSlider = slider;
        healthUI.healthText = null;   // clear old text ref — no longer needed

        // Hide the old HP_Text GameObject
        healthUI.gameObject.SetActive(false);

        EditorUtility.SetDirty(healthUI);
        EditorSceneManager.MarkSceneDirty(healthUI.gameObject.scene);

        Debug.Log("Health bar created at " + barGO.name + " under " + hud.name);
    }
}
