using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnswapTwoDice : MonoBehaviour
{
    void Start()
    {

        Debug.Log("CurrentActionIndex: " + (GlobalEventsManager.instance.currentActionIndex+1));
        Debug.Log("Dice 1: " + GlobalEventsManager.instance.selectedDice[GlobalEventsManager.instance.currentActionIndex+1, 0]);
        Debug.Log("Dice 2: " + GlobalEventsManager.instance.selectedDice[GlobalEventsManager.instance.currentActionIndex+1, 1]);
        //Debug.Log(GlobalEventsManager.instance.gameObject.name);
        GlobalEventsManager.instance.swapDiceOrder(GlobalEventsManager.instance.selectedDice[GlobalEventsManager.instance.currentActionIndex+1,0], GlobalEventsManager.instance.selectedDice[GlobalEventsManager.instance.currentActionIndex+1, 1],true);
        Destroy(gameObject);
    }
}
