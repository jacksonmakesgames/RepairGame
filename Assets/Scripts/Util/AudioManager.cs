using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
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

    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    AudioSource source;

    AudioClip currentTrack;

    [SerializeField]
    float maxVolume;

    bool fading = false;

    private void Awake()
    {
        foreach (NamedTrack t in namedTracks) {
            audioClips.Add(t.name, t.track);
        }
        
        source = GetComponent<AudioSource>();
        source.clip = audioClips["Main Menu"];


    }

    public void ChangeTrack(AudioClip track) {
        if (currentTrack == track) return;
        StartCoroutine(FadeToNewClip(track));
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

    IEnumerator FadeToNewClip(AudioClip clip) {
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
        //TESTING:
        if (Input.GetButtonDown("Fire1")) { StartCoroutine(FadeToNewClip(audioClips["Game"])); }
        
    }
}
