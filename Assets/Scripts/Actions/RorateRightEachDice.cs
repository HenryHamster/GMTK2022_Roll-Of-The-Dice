using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RorateRightEachDice : MonoBehaviour
{
    void Start()
    {
        //Debug.Log(GlobalEventsManager.instance.gameObject.name);
        for (int i = 0; i < GlobalEventsManager.instance.dice.Length - 1; i++)
        {
            GlobalEventsManager.instance.dice[i].rotateDiceRight(false);
        }
        GlobalEventsManager.instance.dice[GlobalEventsManager.instance.dice.Length - 1].rotateDiceRight(true);
        Destroy(gameObject);
    }
}
