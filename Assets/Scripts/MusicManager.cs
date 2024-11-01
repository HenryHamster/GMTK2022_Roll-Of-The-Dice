using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource MenuMusicSource;
    public AudioSource LevelMusicSource;
    public void setVolume(float value)//value from 0 to 1 slider
    {
        if (MenuMusicSource != null)MenuMusicSource.volume = value * 0.2f;

        if (LevelMusicSource != null)LevelMusicSource.volume = value * 0.2f;
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        openMenu();
    }
    public void openLevel()
    {
        //Debug.Log("CLIP IS PLAYING: " + MenuMusicSource.isPlaying);

        if (LevelMusicSource!=null&&!LevelMusicSource.isPlaying) LevelMusicSource.Play();
        if (MenuMusicSource != null && MenuMusicSource.isPlaying) MenuMusicSource.Pause();
        if (LevelMusicSource != null && !LevelMusicSource.isPlaying) LevelMusicSource.UnPause();
    }
        
    public void openMenu()
    {
        //Debug.Log("CLIP IS PLAYING: " + MenuMusicSource.isPlaying);

        if (LevelMusicSource != null && !LevelMusicSource.isPlaying) MenuMusicSource.Play();
        if (MenuMusicSource != null && MenuMusicSource.isPlaying)MenuMusicSource.UnPause();
        if (LevelMusicSource != null && !LevelMusicSource.isPlaying) LevelMusicSource.Pause();
    }
    private void Update()
    {
        if (LevelMusicSource != null && LevelMusicSource.isPlaying && MenuMusicSource != null && MenuMusicSource.isPlaying)
        {
            if (SceneManager.GetActiveScene().name.Length <= 7)
            {
                MenuMusicSource.Stop();
            }
            else
            {
                LevelMusicSource.Stop();
            }
        }
    }
}
