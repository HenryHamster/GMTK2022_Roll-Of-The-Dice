using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudioManager : MonoBehaviour
{

    public float sfxVolume;
    public static GlobalAudioManager instance;
    public int cIndex;
    public AudioSource buttonAudioSource;
    public float buttonAudioVolume;
    public AudioClip buttonClickAudio;
    public AudioClip hoverButton;
    public AudioClip reverseButton;//ctrl+z button
    public AudioClip[] resetAudio;//repurposed from shuffle
    public int resetAudioIndex;
    public AudioSource eventsAudioSource;
    public float eventsAudioVolume;
    public AudioClip winAudio;
    public AudioClip correctAudio;
    public AudioClip wrongAudio;
    public AudioClip earlyAudio;
    public AudioClip swapAudio;
    public AudioClip diceSelectAudio;//repurposed from buttonSelectAudio    
    public AudioClip diceHoverAudio;
    public AudioSource diceRollSource;
    public float diceRollVolume;
    public AudioClip[] diceRollSound;
    private void Awake()
    {
        instance = this;
        sfxVolume = OptionsMenuScript.savedSFXVolume;
    }
    public void playDiceSound()
    {
        if (diceRollSource == null)
        {
            return;
        }
        diceRollSource.PlayOneShot(diceRollSound[cIndex], diceRollVolume * sfxVolume);
        cIndex++;
        if (cIndex >= diceRollSound.Length)
        {
            cIndex = 0;
        }
    }
    public void buttonClickedSound()
    {
        buttonAudioSource.PlayOneShot(buttonClickAudio, buttonAudioVolume * sfxVolume);
    }
    public void buttonHoverSound()
    {
        buttonAudioSource.PlayOneShot(hoverButton, buttonAudioVolume * sfxVolume);
    }
    public void reverseButtonSound()
    {
        buttonAudioSource.PlayOneShot(reverseButton, buttonAudioVolume * sfxVolume*1.5f);
    }
    public void resetButtonSound()
    {

        eventsAudioSource.PlayOneShot(resetAudio[resetAudioIndex], diceRollVolume * sfxVolume);
        resetAudioIndex++;
        if (resetAudioIndex >= resetAudio.Length)
        {
            resetAudioIndex = 0;
        }
    }
    public void winSound()
    {
        eventsAudioSource.PlayOneShot(winAudio, eventsAudioVolume * sfxVolume);
    }
    public void correctSound()
    {
        eventsAudioSource.PlayOneShot(correctAudio, eventsAudioVolume * sfxVolume);
    }
    public void diceSelectSound()
    {
        eventsAudioSource.PlayOneShot(diceSelectAudio, eventsAudioVolume * sfxVolume);
    }
    private void OnDestroy()
    {
        diceRollSource.Stop();
        eventsAudioSource.Stop();
        buttonAudioSource.Stop();
    }
    public void swapDiceSound()
    {
        eventsAudioSource.PlayOneShot(swapAudio, eventsAudioVolume * sfxVolume);
    }
    public void earlySound()
    {
        if (earlyAudio == null) return;
        eventsAudioSource.PlayOneShot(earlyAudio, eventsAudioVolume * sfxVolume);
    }
    public void wrongSound()
    {
        if (wrongAudio == null) return;

        eventsAudioSource.PlayOneShot(wrongAudio, eventsAudioVolume * sfxVolume);
    }
}
