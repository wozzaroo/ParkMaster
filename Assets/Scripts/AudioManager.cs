using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class AudioManager : MonoBehaviour
    {
        AudioSource pickUpAudioSource;
        public AudioClip[] pickUpClips;
        void Start() 
        {
            pickUpAudioSource = GetComponent<AudioSource>();  
        }
        public void PlayPickUpSound(int pickUpSound)
        {
        
            pickUpAudioSource.clip = pickUpClips[pickUpSound];
            pickUpAudioSource.Play();
        }
}
