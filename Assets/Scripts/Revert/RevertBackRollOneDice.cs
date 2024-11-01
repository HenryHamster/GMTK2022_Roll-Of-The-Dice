using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevertBackRollOneDice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalEventsManager.instance.dice[GlobalEventsManager.instance.selectedDice[GlobalEventsManager.instance.currentActionIndex + 1, 0]].rollDiceForward();
        Destroy(gameObject);
    }
}
