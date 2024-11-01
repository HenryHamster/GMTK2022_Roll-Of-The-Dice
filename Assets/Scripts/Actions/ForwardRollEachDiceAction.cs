using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardRollEachDiceAction : MonoBehaviour
{
    void Start()
    {
        //Debug.Log(GlobalEventsManager.instance.gameObject.name);
        for (int i=0;i< GlobalEventsManager.instance.dice.Length - 1;i++)
        {
            GlobalEventsManager.instance.dice[i].rollDiceForward(false);
        }
        GlobalEventsManager.instance.dice[GlobalEventsManager.instance.dice.Length-1].rollDiceForward(true);
        Destroy(gameObject);
    }
}
