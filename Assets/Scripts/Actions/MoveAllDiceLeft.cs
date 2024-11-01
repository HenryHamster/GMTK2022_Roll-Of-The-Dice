using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAllDiceLeft : MonoBehaviour
{
    void Start()
    {
        for (int i = GlobalEventsManager.instance.dice.Length - 2; i >0; i--)
        {
            GlobalEventsManager.instance.swapDiceOrder(GlobalEventsManager.instance.dice[i].position, GlobalEventsManager.instance.dice.Length - 1,false);
        }
        GlobalEventsManager.instance.swapDiceOrder(GlobalEventsManager.instance.dice[0].position, GlobalEventsManager.instance.dice.Length - 1,true);
        Destroy(gameObject);
    }
}
