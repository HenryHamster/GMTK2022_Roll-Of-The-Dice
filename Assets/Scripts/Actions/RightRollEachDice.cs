using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightRollEachDice : MonoBehaviour
{
    void Start()
    {
        //Debug.Log(GlobalEventsManager.instance.gameObject.name);
        for (int i = 0; i < GlobalEventsManager.instance.dice.Length - 1; i++)
        {
            GlobalEventsManager.instance.dice[i].rollDiceRight(false);
        }
        GlobalEventsManager.instance.dice[GlobalEventsManager.instance.dice.Length - 1].rollDiceRight(true);
        Destroy(gameObject);
    }
}
