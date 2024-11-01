using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRollEachDice : MonoBehaviour
{
    void Start()
    {
        //Debug.Log(GlobalEventsManager.instance.gameObject.name);
        for (int i = 0; i < GlobalEventsManager.instance.dice.Length - 1; i++)
        {
            GlobalEventsManager.instance.dice[i].rollDiceDown(false);
        }
        GlobalEventsManager.instance.dice[GlobalEventsManager.instance.dice.Length - 1].rollDiceDown(true);
        Destroy(gameObject);
    }
}
