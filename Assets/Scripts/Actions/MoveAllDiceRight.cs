using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAllDiceRight : MonoBehaviour
{
    void Start()
    {

        //Debug.Log(GlobalEventsManager.instance.gameObject.name);
        for (int i = 1; i < GlobalEventsManager.instance.dice.Length-1; i++)
        {
            GlobalEventsManager.instance.swapDiceOrder(GlobalEventsManager.instance.dice[i].position, 0,false);
        }
        GlobalEventsManager.instance.swapDiceOrder(GlobalEventsManager.instance.dice[GlobalEventsManager.instance.dice.Length-1].position, 0,true);

        Destroy(gameObject);
    }
}
