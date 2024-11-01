using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvas : MonoBehaviour
{
    private void Start()
    {
        if (GlobalSceneManager.unlockedLevels[GlobalSceneManager.currentLevel])
        {
            Destroy(gameObject);
        }
    }
}
