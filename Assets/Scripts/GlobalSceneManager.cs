using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSceneManager : MonoBehaviour
{
    public static GlobalSceneManager instance;
    public static bool[] unlockedLevels=new bool[21];
    public static int currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        unlockedLevels[0] = true;   
        /*if (instance != null)
        {
            Destroy(gameObject);
        }*/
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    public void openMenuScene()
    {
        MusicManager.instance.openMenu();
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
    public void exitGame()
    {
        SceneManager.LoadScene("ExitGameBlankScene", LoadSceneMode.Single);
        Application.Quit();
    }
    public void openLevel(int lvl)
    {
        if (unlockedLevels[lvl - 1])
        {

            currentLevel = lvl;
            SceneManager.LoadScene("Level" + lvl.ToString());
            MusicManager.instance.openLevel();

        }
        else
        {
            //Unable to open level
        }
    }
    public void nextLevel()
    {
        unlockedLevels[currentLevel] = true;
        currentLevel += 1;
        SceneManager.LoadScene("Level" + (currentLevel).ToString());
    } 
    public void openLevelSelectMenu()
    {
        SceneManager.LoadScene("LevelSelectMenu", LoadSceneMode.Single);
    }
}
