using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playlist : MonoBehaviour {
    [SerializeField]
    private Object[] soundTrack;


    private AudioSource audioSourceComponent;

    void Start() {
        audioSourceComponent = GetComponent<AudioSource>();

        if (soundTrack.Length == 0) {
            Debug.Log("The soundTrack is empty!");
        } else {
            PlayRandomSound();
        }
    }

    void Update() {
        if (!audioSourceComponent.isPlaying) {
            PlayRandomSound();
        }
    }

    private void PlayRandomSound() {
        audioSourceComponent.clip = soundTrack[Random.Range(0, soundTrack.Length)] as AudioClip;
        audioSourceComponent.Play();
    }
}
