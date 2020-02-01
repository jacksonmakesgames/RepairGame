using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioSource buttonAudioSource;
    
    [SerializeField]
    AudioClip beginClip;

    public void LoadGameLevel(int levelIndex) {
        buttonAudioSource.PlayOneShot(beginClip);
        SceneManager.LoadScene(levelIndex);
    }

    public void Quit() {
        Application.Quit();
    }
}
