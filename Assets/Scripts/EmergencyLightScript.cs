using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;
public class EmergencyLightScript : MonoBehaviour
{
    public enum LightType
    {
        flashing,
        fading
    }
    private Coroutine offLightRoutine;
    private SpriteRenderer sr;
    public bool turnedOn;
    public Light2D light;
    public LightType lType;

    public float lightChangeSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (lType == LightType.flashing)
        {
            turnedOn = true;
            changeLightLevel(turnedOn);
        }
        else if (lType == LightType.fading)
        {
            turnedOn = false;
            changeLightLevel(turnedOn);
        }
    }

    void Update()
    {
        if (lType == LightType.fading)
        {
            if ((turnedOn && light.intensity != 0.87f) || (!turnedOn && light.intensity != 0))
            {
                changeLightLevel(turnedOn);
            }
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (lType == LightType.flashing)
        {
            
        }
        else if (lType == LightType.fading)
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
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (lType == LightType.flashing)
        {
            
        }
        else if (lType == LightType.fading)
        {
            if (other.CompareTag("Player") && turnedOn)
            {
                offLightRoutine = StartCoroutine(lightOff());
            }
        }
    }

    private void changeLightLevel(bool on)
    {
        if (lType == LightType.flashing)
        {
            offLightRoutine = StartCoroutine(flashing());
        }
        else if (lType == LightType.fading)
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
    }

    IEnumerator lightOff()
    {
        yield return new WaitForSeconds(3);
        turnedOn = false;
    }

    IEnumerator flashing()
    {
        while (turnedOn)
        {
            light.intensity = 0.87f;
            sr.color = new Color32(255, 60, 47, 255);
            yield return new WaitForSeconds(0.5f);
            light.intensity = 0;
            sr.color = new Color32(121, 8, 0, 255);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
