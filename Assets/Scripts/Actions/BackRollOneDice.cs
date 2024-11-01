using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRollOneDice : MonoBehaviour
{
    public int firstSelected = -1;
    // Start is called before the first frame update
    void Start()
    {
        GlobalEventsManager.instance.paused = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (firstSelected != -1)
        {
            GlobalEventsManager.instance.dice[firstSelected].rollDiceDown();
            GlobalEventsManager.instance.paused = false;
            Debug.Log("Saved CurrentActionIndex " + GlobalEventsManager.instance.currentActionIndex);
            GlobalEventsManager.instance.selectedDice[GlobalEventsManager.instance.currentActionIndex, 0] = firstSelected;
            Destroy(gameObject);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D selectedDice = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
            if (selectedDice == null) return;
            GlobalAudioManager.instance.diceSelectSound();
            firstSelected = selectedDice.gameObject.GetComponent<DiceScript>().position;
        }
        if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.Backspace))
        {
            GlobalEventsManager.instance.paused = false;
            ActionBarScript.instance.cancelledAction(); 
            ActionBarScript.instance.movesLeftText.text = " Moves Left: " + (GlobalEventsManager.instance.maxMoves - GlobalEventsManager.instance.currentActionIndex).ToString();

            Destroy(gameObject);
        }
    }
}
