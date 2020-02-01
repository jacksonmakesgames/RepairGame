using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Range(.00001f, .05f)]
    [SerializeField]
    private float fadeLength; // seconds/step

    [Serializable]
    public struct NamedTrack
    {
        public string name;
        public AudioClip track;
    }
    public NamedTrack[] namedTracks;

    public  Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    AudioSource source;

    AudioClip currentTrack;

    [SerializeField]
    float maxVolume;

    bool fading = false;

    private void Awake()
    {
        if (AudioManager.Instance != null) { Destroy(this.gameObject); return; } else { AudioManager.Instance = this; }
        DontDestroyOnLoad(this);

        foreach (NamedTrack t in namedTracks) {
            audioClips.Add(t.name, t.track);
        }
        
        source = GetComponent<AudioSource>();
        source.clip = audioClips["Main Menu"];


    }

    public void ChangeTrack(AudioClip track) {
        if (currentTrack == track) return;
       FadeToNewClip(track);
    }

    IEnumerator FadeIn() {
        if (fading) yield break;
        fading = true;
        source.Play();
        source.volume = 0;
        while (source.volume < maxVolume)
        {
            yield return new WaitForSeconds(fadeLength);
            source.volume += .01f;
        }
        fading = false;
    }
    IEnumerator FadeOut() {
        if (fading) yield break;
        fading = true;
        while (source.volume > 0)
        {
            yield return new WaitForSeconds(fadeLength);
            source.volume -= .01f;

        }
        fading = false;
    }

    public void FadeToNewClip(AudioClip clip) {
        StartCoroutine(FadeToNewClipEnum(clip));
    }
    IEnumerator FadeToNewClipEnum(AudioClip clip) {
        yield return StartCoroutine(FadeOut());
        source.clip = clip;
        yield return StartCoroutine(FadeIn());
    }

    void Start()
    {
        StartCoroutine(FadeIn());
        
    }

    void Update()
    {
        
    }
}
