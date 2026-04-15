using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSequenceController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject screen1Ship;
    public GameObject screen2Blackout;
    public GameObject screen3Landing;

    [Header("Timing")]
    public float screen1Duration = 4f;
    public float screen2Duration = 5f;
    public float screen3Duration = 5f;

    [Header("Next Scene")]
    public string nextSceneName = "SpawnArea";

    private bool isSkipping = false;

    private void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    // Spacebar to skip the intro
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkipIntro();
        }
    }

    private IEnumerator PlayIntroSequence()
    {
        ShowOnly(screen1Ship);
        yield return new WaitForSeconds(screen1Duration);

        if (isSkipping) yield break;

        ShowOnly(screen2Blackout);
        yield return new WaitForSeconds(screen2Duration);

        if (isSkipping) yield break;

        ShowOnly(screen3Landing);
        yield return new WaitForSeconds(screen3Duration);

        if (isSkipping) yield break;

        LoadNextScene();
    }

    private void ShowOnly(GameObject activeScreen)
    {
        if (screen1Ship != null) screen1Ship.SetActive(false);
        if (screen2Blackout != null) screen2Blackout.SetActive(false);
        if (screen3Landing != null) screen3Landing.SetActive(false);

        if (activeScreen != null) activeScreen.SetActive(true);
    }

    public void SkipIntro()
    {
        if (isSkipping) return;

        isSkipping = true;
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}