using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionsMenuScript : MonoBehaviour
{
    public static float savedGlobalVolume=0.5f;
    public static float savedMusicVolume=0.5f;
    public static float savedSFXVolume=0.5f;
    public static bool fullscreen=true;
    public static int resolution=0;
    public Slider globalVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;
    //Global Player variables&options handler
    public float origBrightness;
    public float savedBrightness;
    public AudioSource musicSource;    public static OptionsMenuScript ddolPrevention;//Prevent duplication on don't destroy on load
    public CanvasGroup optionsMenuCanvasGroup;
    public CanvasGroup pauseMenuCanvasGroup;
    public CanvasGroup winCanvasGroup;
    public CanvasGroup comfirmationScreen;
    public GameObject nextLevelButton;
    public bool quitOrMenu;//true=quit false=menu
    public bool pauseMenuLastFrame;
    public void openLevelSelect()
    {
        GlobalSceneManager.instance.openLevelSelectMenu();
    }
    public void openWinMenu()
    {
        if (GlobalSceneManager.currentLevel >= 21)
        {
            nextLevelButton.SetActive(false);
        }
        else
        {
            nextLevelButton.SetActive(true);
        }
        winCanvasGroup.alpha = 1;
        winCanvasGroup.interactable = true;
        winCanvasGroup.blocksRaycasts = true;
    }
    public void closeWinMenu() {
        winCanvasGroup.alpha = 0;
        winCanvasGroup.interactable = false;
        winCanvasGroup.blocksRaycasts = false;
    }
    public void openOptionsMenu()
    {
        optionsMenuCanvasGroup.alpha = 1;
        optionsMenuCanvasGroup.interactable = true;
        optionsMenuCanvasGroup.blocksRaycasts = true;
        pauseMenuCanvasGroup.alpha = 0;
        pauseMenuCanvasGroup.interactable = false;
        pauseMenuCanvasGroup.blocksRaycasts = false;
    }
    public void openPauseMenu()
    {
        pauseMenuCanvasGroup.alpha = 1;
        pauseMenuCanvasGroup.interactable = true;
        pauseMenuCanvasGroup.blocksRaycasts = true;
    }
    public void closePauseMenu()
    {

        pauseMenuCanvasGroup.alpha = 0;
        pauseMenuCanvasGroup.interactable = false;
        pauseMenuCanvasGroup.blocksRaycasts = false;
    }
    public void togglePauseMenu()
    {
        if (!pauseMenuLastFrame)
        {
            openPauseMenu();
            //closePauseMenu();
        }
    }
    public void closeOptionMenu()
    {
        optionsMenuCanvasGroup.alpha = 0;
        optionsMenuCanvasGroup.interactable = false;
        optionsMenuCanvasGroup.blocksRaycasts = false;
        pauseMenuCanvasGroup.alpha = 1;
        pauseMenuCanvasGroup.interactable = true;
        pauseMenuCanvasGroup.blocksRaycasts = true;
    }

    void Start()
    {

        /*if (ddolPrevention != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);*/
        if (SceneManager.GetActiveScene().name.Length <= 7)
        {
            musicSource = MusicManager.instance.LevelMusicSource;
        }
        else
        {
            musicSource = MusicManager.instance.MenuMusicSource;
        }
        ddolPrevention = this;
        Debug.Log("Global Volume: " + savedGlobalVolume);
        Debug.Log("Music Volume: " + savedMusicVolume);

        Debug.Log("SFX Volume: " + savedSFXVolume);
        Debug.Log("Fullscreen: " + fullscreen);
        Debug.Log("Resolution: " + resolution);
        AudioListener.volume = savedGlobalVolume;
        globalVolumeSlider.value = savedGlobalVolume;
        musicVolumeSlider.value = savedMusicVolume;
        sfxVolumeSlider.value = savedSFXVolume;     
        fullscreenToggle.isOn = fullscreen;
        resolutionDropdown.value = resolution;
        if (SceneManager.GetActiveScene().name.Length <= 7)
        {
            Debug.Log("In game scene");
            GlobalAudioManager.instance.sfxVolume = savedSFXVolume;
            if (musicSource != null)
            {
                musicSource.volume = savedMusicVolume;
            }
            UpdateMusicVolume(savedMusicVolume);
            UpdateAspectRatio(resolution);
        }
        //Loading previous save
    }
    public void UpdateFullscreen(bool fullscreenValue)
    {
        fullscreen = fullscreenValue;
        Screen.fullScreen = fullscreenValue;
    }
    public void UpdateAspectRatio(int aspectRatio)
    {
        Debug.Log("Setting aspect ratio to "+aspectRatio.ToString());
        resolution = aspectRatio;
        switch (aspectRatio)
        {
            case 0:
                Camera.main.ResetAspect();
                break;
            case 1:
                Camera.main.aspect = 16f / 10f; //1.6
                break;
            case 2:
                Camera.main.aspect = 16f / 9f;//1.7777
                break;
            case 3:
                Camera.main.aspect = 13f / 6f;//2.17777
                break;
            case 4:
                Camera.main.aspect = 4f / 3f; //1.33333
                break;
            case 5:
                Camera.main.aspect = 3f / 2f;///1.5
                break;
            case 6:
                Camera.main.aspect = 1f / 1f;//11
                break;
            default:
                Camera.main.ResetAspect();
                break;

        }
        Debug.Log("Aspect ratio is " + Camera.main.aspect   );

    }
    /*public void UpdateBrightness(float brightness)
    {
     //   savedBrightness = brightness;
        //Screen.brightness = brightness;

    }*/
    public void UpdateGlobalVolume(float volume)
    {
        savedGlobalVolume = volume;
        AudioListener.volume = volume;

    }

    public void UpdateSFXVolume(float volume)
    {
        Debug.Log("Updating volume to " + volume);
        savedSFXVolume = volume;
        if (SceneManager.GetActiveScene().name.Length<=7)
        {
            GlobalAudioManager.instance.sfxVolume = volume;
        }

    }
    public void UpdateMusicVolume(float volume)
    {
        MusicManager.instance.setVolume(volume);
    }
    private void Awake()
    {
       // savedBrightness = Screen.brightness;
        //origBrightness = Screen.brightness;
        //GlobalSaveManager.SavePlayer(new SavedData());
    }
    private void LateUpdate()
    {

        if (!Input.GetMouseButton(0))
        {
            pauseMenuLastFrame = pauseMenuCanvasGroup.interactable;
        }
    }
    private void Update()
    {
        
    }
    public void nextLevel()
    {

        if (SceneManager.GetActiveScene().name != "MenuScene" || SceneManager.GetActiveScene().name != "LevelSelectMenu")
        {
            GlobalSceneManager.instance.nextLevel();
        }
    }
    public void restartLevel()
    {
        if (SceneManager.GetActiveScene().name != "MenuScene" || SceneManager.GetActiveScene().name != "LevelSelectMenu")
        {
            GlobalSceneManager.instance.openLevel(GlobalSceneManager.currentLevel);
        }
    }
    public void quitGame()
    {
        quitOrMenu = false;
        if (SceneManager.GetActiveScene().name != "MenuScene" || SceneManager.GetActiveScene().name != "LevelSelectMenu")
        {
            comfirmationScreen.alpha = 1;
            comfirmationScreen.interactable = true;
            comfirmationScreen.blocksRaycasts = true;
        }
        else
        {
            comfirmedQuit();
        }
    }
    public void quitMenu()
    {
        quitOrMenu = true;
        if (SceneManager.GetActiveScene().name != "MenuScene"||SceneManager.GetActiveScene().name!="LevelSelectMenu")
        {
            comfirmationScreen.alpha = 1;
            comfirmationScreen.interactable = true;
            comfirmationScreen.blocksRaycasts = true;
        }
        else
        {
            comfirmedQuit();
        }
    }
    public void comfirmedQuit()
    {
        comfirmationScreen.alpha = 0;
        comfirmationScreen.interactable = false;
        comfirmationScreen.blocksRaycasts =false;
        if (!quitOrMenu)
        {
            GlobalSceneManager.instance.exitGame();
        }
        else
        {
            GlobalSceneManager.instance.openMenuScene();
        }
    }
    public void cancelledQuit()
    {
        comfirmationScreen.alpha = 0;
        comfirmationScreen.interactable = false;
        comfirmationScreen.blocksRaycasts = false;
    }
    private void OnApplicationFocus(bool focus)
    {
        if (SceneManager.GetActiveScene().name.Length<=7)
        {
            if (focus)
            {
               // UpdateBrightness(savedBrightness);
            }
            else
            {
                if (!winCanvasGroup.blocksRaycasts && !pauseMenuCanvasGroup.interactable)
                {
                    openPauseMenu();
                }
                //Screen.brightness = origBrightness;
            }
        }
    }

}
