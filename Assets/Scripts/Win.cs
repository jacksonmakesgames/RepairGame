using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update
    public void Winner()
    {
        AudioManager.Instance.FadeToNewClip(AudioManager.Instance.audioClips["Win"]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
