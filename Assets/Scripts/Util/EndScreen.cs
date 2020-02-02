using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Win() {
        AudioManager.Instance.FadeToNewClip(AudioManager.Instance.audioClips["Win"]);

    }
    public void Lose()
    {
        GetComponent<RawImage>().color = new Color(0, 0, 0, 1);
        AudioManager.Instance.FadeToNewClip(AudioManager.Instance.audioClips["Lose"]);
    }
}
