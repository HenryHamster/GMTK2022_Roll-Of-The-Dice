using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLeftEachDice : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < GlobalEventsManager.instance.dice.Length - 1; i++)
        {
            GlobalEventsManager.instance.dice[i].rotateDiceLeft(false);
        }
        GlobalEventsManager.instance.dice[GlobalEventsManager.instance.dice.Length - 1].rotateDiceLeft(false);
        Destroy(gameObject);
    }
}
