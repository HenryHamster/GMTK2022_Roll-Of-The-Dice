using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevertForwardRollOneDice : MonoBehaviour
{
    void Start()
    {

        //Debug.Log("CurrentActionIndex: " + (GlobalEventsManager.instance.currentActionIndex + 1));
        //Debug.Log("Dice 1: " + GlobalEventsManager.instance.selectedDice[GlobalEventsManager.instance.currentActionIndex + 1, 0]);
        //Debug.Log(GlobalEventsManager.instance.gameObject.name);
        GlobalEventsManager.instance.dice[GlobalEventsManager.instance.selectedDice[GlobalEventsManager.instance.currentActionIndex + 1, 0]].rollDiceDown();
        Destroy(gameObject);
    }
}
