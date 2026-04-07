using UnityEngine;
using UnityEngine.SceneManagement; 

public class OxygenManager : MonoBehaviour
{
    [Header("Oxygen Settings")]
    public float maxOxygen = 100f;
    public float currentOxygen;
    public float oxygenDrainRate = 5f;   // How fast it drains in space
    public float oxygenRefillRate = 20f; // How fast it refills in the base

    [Header("Environment Settings")]
    public bool isHelmetOn = false;
    public string safeSceneName = "SampleScene"; 

    void Start()
    {
        // Start with a full tank
        currentOxygen = maxOxygen;
        CheckEnvironment(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    void OnEnable()
    {
        // Run  check every time a new scene finishes loading
        SceneManager.sceneLoaded += CheckEnvironment;
    }

    void OnDisable()
    {
        // Clean up the listener if this object is destroyed
        SceneManager.sceneLoaded -= CheckEnvironment;
    }

    void CheckEnvironment(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == safeSceneName)
        {
            isHelmetOn = false;
            Debug.Log("Entered Base: Helmet Off. Oxygen refilling.");
        }
        else
        {
            isHelmetOn = true;
            Debug.Log("Entered Space: Helmet On! Oxygen draining.");
        }
    }

    void Update()
    {
        if (isHelmetOn)
        {
            // Drain oxygen over time
            currentOxygen -= oxygenDrainRate * Time.deltaTime;
            
            // Keep it from dropping below zero
            currentOxygen = Mathf.Max(currentOxygen, 0f);

            if (currentOxygen <= 0)
            {
                PassOut();
            }
        }
        else
        {
            // Rapidly refill oxygen when back in the base
            currentOxygen += oxygenRefillRate * Time.deltaTime;
            
            // Keep it from exceeding the maximum limit
            currentOxygen = Mathf.Min(currentOxygen, maxOxygen);
        }
    }

    void PassOut()
    {
        Debug.Log("Out of oxygen! Player passed out.");
       
    }
}