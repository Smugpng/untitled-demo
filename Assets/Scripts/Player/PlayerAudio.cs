using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip hitSound, lowHpSound, dashSound;
    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Dmg()
    {
        audioSource.clip = hitSound;
        audioSource.Play();
    }
    public void low()
    {
        audioSource.clip = lowHpSound;
        audioSource.Play();
    }
    public void dash()
    {
        audioSource.clip = dashSound;
        audioSource.Play();
    }

}
