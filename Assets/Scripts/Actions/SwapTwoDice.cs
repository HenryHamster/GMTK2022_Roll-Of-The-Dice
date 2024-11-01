using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Notes for CMU Pre-College Application (Written 1/28/2023)
----------------------------------------------------------------------------------------------------------
This script is part of the The Dice is Right made for the GMTK Game Jam 2022
This script is an example of one of the actions in the game, found in the bottom bar.
This script is made within a game jam and is unedited(outside of notes) from its previous state and might contain superficial code as a result
If there are any questions about the code or need for some of the references in this script,
please let me know through my email lihuanshen123@gmail.com. Thank you!
                                                                      -Sincerely, Li-huan (Henry) Shen
------------------------------------------------------------------------------------------------------------
*/
public class SwapTwoDice : MonoBehaviour
{
    public int firstSelected=-1;
    public int secondSelected=-1;
    // Start is called before the first frame update
    void Start()
    {
        GlobalEventsManager.instance.paused = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (firstSelected != -1 && secondSelected != -1)
        {
            GlobalEventsManager.instance.swapDiceOrder(firstSelected,secondSelected,true);
            GlobalEventsManager.instance.paused = false;
            Debug.Log("Saved CurrentActionIndex " + GlobalEventsManager.instance.currentActionIndex);
            GlobalEventsManager.instance.selectedDice[GlobalEventsManager.instance.currentActionIndex, 0] = firstSelected;
            GlobalEventsManager.instance.selectedDice[GlobalEventsManager.instance.currentActionIndex, 1] = secondSelected;
            Destroy(gameObject);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D selectedDice=Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition),0.1f);
            if (selectedDice == null) return;
            if (firstSelected!=-1&& selectedDice.gameObject.GetComponent<DiceScript>() == GlobalEventsManager.instance.dice[firstSelected]) return;//same dice
            GlobalAudioManager.instance.diceSelectSound();
            if (firstSelected == -1)
            {
                firstSelected = selectedDice.gameObject.GetComponent<DiceScript>().position;
            }
            else
            {
                secondSelected = selectedDice.gameObject.GetComponent<DiceScript>().position;
            }
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
