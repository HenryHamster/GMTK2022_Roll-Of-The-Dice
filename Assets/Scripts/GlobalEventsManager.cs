using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
Notes for CMU Pre-College Application (Written 1/28/2023)
----------------------------------------------------------------------------------------------------------
This script is part of The Dice is Right project made for the GMTK Game Jam 2022
This script uses DoTween which I did not write myself for this project.
The rest of the code should be largely original also I can no longer remember/comfirm if I was influenced
by external code for certain parts of this script, however, most of it should be fully self-written and 
original as it is a game jam with its own regulations.
The code itself is largely unedited from its GameJam state which was written around half a year ago.
Due to it being made in a GameJam, there is a chance that some code may be superficial or inefficient
If there are any questions about the code or need for some of the references in this script,
please let me know through my email lihuanshen123@gmail.com. Thank you!
                                                                      -Sincerely, Li-huan (Henry) Shen
------------------------------------------------------------------------------------------------------------
*/
public class GlobalEventsManager : MonoBehaviour
{
    public static GlobalEventsManager instance;
    public int[] neededValues;
    private int[] origValue;
    public int[] prevActionIndexes;
    public int[,] selectedDice= new int[999,2];
    public int currentActionIndex;
    public DiceScript[] dicePrefabs;
    public DiceScript[] dice;//currentDiceOnly
    public float startingXPosition;
    public float yPosition;
    public float xUnitPosition;
    public bool paused;
    public CanvasGroup canvasGroup;
    public int maxMoves;
    private void Awake()
    {
        instance = this;
        origValue = neededValues;
    }
    private void Start()
    {
        summonNewDice();
        updateDicePosition();
    }
    public void summonNewDice()
    {
        if (GlobalAudioManager.instance != null && dice!= null)
        {
            GlobalAudioManager.instance.resetButtonSound();
        }
        for(int i = 0; i < dice.Length;i++)
        {
            Destroy(dice[i].gameObject);
            dice[i] = null;
        }

        DiceScript[] tempArr = new DiceScript[dicePrefabs.Length];
        for (int i = 0; i < dicePrefabs.Length;i++)
        {
            tempArr[i] = Instantiate(dicePrefabs[i].gameObject,transform.position,Quaternion.identity).GetComponent<DiceScript>();
            tempArr[i].gameObject.SetActive(true);
        }
        dice = tempArr;
        updateDicePosition();
        updateDice();

    }
    public void swapDiceOrder(int index1,int index2, bool save)
    {
        GlobalAudioManager.instance.swapDiceSound();
        DiceScript dice1=dice[index1];
        DiceScript dice2 = dice[index2];
        dice1.position = index2;
        dice2.position = index1;
        dice[index1] = dice2;
        dice[index2] = dice1;
        Debug.Log("Swapped dice with indexes " + index1 + " and " + index2);
        dice1.transform.DOLocalMoveY(-1.5f, 0.5f);
        dice2.transform.DOLocalMoveY(1.5f, 0.5f);
        StartCoroutine(moveYBackofDice(dice1.transform, startingXPosition + xUnitPosition *index2));
        StartCoroutine(moveYBackofDice(dice2.transform, startingXPosition + xUnitPosition *index1));
        if(save)updateDice();
    }
    IEnumerator moveYBackofDice(Transform diceTransform, float targetPosition)
    {
        yield return diceTransform.DOMoveX(targetPosition, 1f).WaitForCompletion();
        diceTransform.DOLocalMoveY(0f, 0.5f);

    }
    public void updateDicePosition()
    {
        for(int i = 0; i < dice.Length; i++)
        {
            dice[i].transform.position = new Vector2(startingXPosition + xUnitPosition * i, yPosition);
        }
        updateDice();

    }
    public void updateDice()
    {
        bool[] indivSucess=new bool[6];
        bool success=true;
        int index=0;
        foreach(DiceScript individualDice in dice)
        {
            if (individualDice.currentValueIndex != origValue[index])
            {
                indivSucess[index] = false;
                success = false;
            }
            else
            {
                GlobalAudioManager.instance.correctSound();
                indivSucess[index] = true;
                individualDice.winParticleEffect.Stop();
                individualDice.winParticleEffect.Play();
            }
            index++;
        }
        ActionBarScript.instance.updateCheckMarks(indivSucess);
        if (success)
        {
            canvasGroup.interactable = true;
            StartCoroutine(win());
            Debug.Log("Win");
        }

    }
    IEnumerator win()
    {
        yield return new WaitForSeconds(0.5f);

        GlobalAudioManager.instance.winSound();
        GlobalSceneManager.unlockedLevels[GlobalSceneManager.currentLevel] = true;
        for (int i = 0; i < 40; i++)
        {
            yield return new WaitForSeconds(0.05f);
            OptionsMenuScript.ddolPrevention.winCanvasGroup.alpha = Mathf.Lerp(0, 1, Mathf.Pow(i, 2) / 1600);
            //OptionsMenuScript.ddolPrevention.winCanvasGroup.alpha += 0.025f;
        }

        OptionsMenuScript.ddolPrevention.openWinMenu();
    }
    private void Update()
    {
        if (paused)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}
