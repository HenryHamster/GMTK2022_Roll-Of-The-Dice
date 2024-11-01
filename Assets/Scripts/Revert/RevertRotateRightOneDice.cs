using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevertRotateRightOneDice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalEventsManager.instance.dice[GlobalEventsManager.instance.selectedDice[GlobalEventsManager.instance.currentActionIndex + 1, 0]].rotateDiceLeft();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
