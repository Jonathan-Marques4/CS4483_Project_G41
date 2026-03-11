using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;
public class EmergencyLightScript : MonoBehaviour
{
    private Coroutine offLightRoutine;
    private SpriteRenderer sr;
    public bool turnedOn;
    public Light2D light;

    public float lightChangeSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        turnedOn = false;
        changeLightLevel(turnedOn);
    }

    void Update()
    {
        if ((turnedOn && light.intensity != 0.87f) || (!turnedOn && light.intensity != 0))
        {
            changeLightLevel(turnedOn);
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !turnedOn)
        {
            if (offLightRoutine != null)
            {
                StopCoroutine(offLightRoutine);
            }
            turnedOn = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && turnedOn)
        {
            offLightRoutine = StartCoroutine(lightOff());
        }
    }

    private void changeLightLevel(bool on)
    {
        if (on)
        {
            light.intensity = Mathf.Lerp(light.intensity, 0.87f, Time.deltaTime * lightChangeSpeed);
            sr.color = Color.Lerp(sr.color, new Color32(255, 60, 47, 255), Time.deltaTime * lightChangeSpeed);
        }
        else
        {
            light.intensity = Mathf.Lerp(light.intensity, 0, Time.deltaTime * lightChangeSpeed);
            sr.color = Color.Lerp(sr.color, new Color32(121, 8, 0, 255), Time.deltaTime * lightChangeSpeed);
        }
    }

    IEnumerator lightOff()
    {
        yield return new WaitForSeconds(3);
        turnedOn = false;
    }
}
