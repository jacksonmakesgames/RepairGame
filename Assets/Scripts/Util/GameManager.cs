using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Range(.00001f, .05f)]
    [SerializeField]
    private float fadeLength; // seconds/step

    [SerializeField]
    RawImage screenFadeImage;
    bool fading = false;

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            AudioManager.Instance.FadeToNewClip(AudioManager.Instance.audioClips["Main Menu"]);
        }
        if (level == 1) {
            AudioManager.Instance.FadeToNewClip(AudioManager.Instance.audioClips["Game"]);
        }
    }

    private void Awake()
    {
        if (GameManager.Instance != null) { Destroy(this.gameObject); return; } else { GameManager.Instance = this; }
        DontDestroyOnLoad(this);

    }

    Color GetColorWithNewAlpha(Color color, float a) {
        return new Color(color.r, color.g, color.b, a);
    }

    IEnumerator FadeIn()
    {
        if (fading) yield break;
        fading = true;
        screenFadeImage.color = GetColorWithNewAlpha(screenFadeImage.color, 0);

        while (screenFadeImage.color.a <1.0f)
        {
            yield return new WaitForSeconds(fadeLength);
            screenFadeImage.color = GetColorWithNewAlpha(screenFadeImage.color, screenFadeImage.color.a + .01f);

        }
        fading = false;
    }
    IEnumerator FadeOut()
    {
        if (fading) yield break;
        fading = true;

        while (screenFadeImage.color.a>0.0f)
        {
            yield return new WaitForSeconds(fadeLength);
            screenFadeImage.color = GetColorWithNewAlpha(screenFadeImage.color, screenFadeImage.color.a - .01f);

        }
        fading = false;
    }

    public void LoadScene(int sceneIndex) {
        StartCoroutine(LoadSceneEnum(sceneIndex));
    }

    IEnumerator LoadSceneEnum(int sceneIndex)
    {
        yield return StartCoroutine(FadeIn());
        SceneManager.LoadScene(sceneIndex);
        yield return StartCoroutine(FadeOut());

    }

}
