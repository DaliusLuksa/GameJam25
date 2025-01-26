using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sound_Effect : MonoBehaviour
{
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip placeSound;

    public void playSounds(string soundName)
    {
        AudioSource audio = GetComponent<AudioSource>();

        if(!audio) {
             Debug.LogWarning("AudioSource component not found on the GameObject.");
        }

        switch (soundName)
        {
            case "death":
                audio.clip = deathSound;

                break;
            case "pickup":
                audio.clip = pickupSound;

                break;
            case "place":
                audio.clip = placeSound;

                break;
            default:
                Debug.LogError("Unknown sound effect");
                break;
        }

        audio.Play();
    }
}
