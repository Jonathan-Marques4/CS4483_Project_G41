using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDamageFlash : MonoBehaviour
{
    public static ScreenDamageFlash Instance;

    public Image flashImage;
    public float flashAlpha = 0.35f;
    public float flashDuration = 0.4f;

    private Coroutine flashRoutine;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetAlpha(0f);
    }

    public void Flash()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        SetAlpha(flashAlpha);

        float timer = 0f;

        while (timer < flashDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(flashAlpha, 0f, timer / flashDuration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(0f);
    }

    private void SetAlpha(float alpha)
    {
        if (flashImage == null) return;

        Color color = flashImage.color;
        color.a = alpha;
        flashImage.color = color;
    }
}