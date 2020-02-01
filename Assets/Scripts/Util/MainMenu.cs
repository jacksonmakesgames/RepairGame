using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioSource buttonAudioSource;
    
    [SerializeField]
    AudioClip beginClip;

    public void LoadGameLevel(int levelIndex) {
        //buttonAudioSource.PlayOneShot(beginClip);
        GameManager.Instance.LoadScene(levelIndex);
    }

    public void Quit() {
        Application.Quit();
    }
}
