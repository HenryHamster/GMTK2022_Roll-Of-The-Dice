using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionBarScript : MonoBehaviour
{
    public static ActionBarScript instance;
    public int[] assignedActions;
    public Image[] actionImage;
    public Image[] actionIconImage;
    public TextMeshProUGUI[] actionName;
    public TextMeshProUGUI[] actionDescription;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI movesLeftText;
    public Button[] actionButton;
    public ScriptableAction[] possible_Actions;
    public Sprite[] diceIcons;
    public Image[] diceIconsImage;
    public GameObject[] diceIconsCheckMark;
    public float lastActionTime;
    public float timeBetweenActions;
    public void instantiateActions(int index)
    {

        if (GlobalEventsManager.instance.maxMoves <= GlobalEventsManager.instance.currentActionIndex)
        {
            GlobalAudioManager.instance.wrongSound();
            //no moves effect
            return;
        }
        if (Time.timeSinceLevelLoad - lastActionTime < timeBetweenActions)
        {
            GlobalAudioManager.instance.earlySound();
            Debug.Log("TimeNotUp");
            return;
        }
        GlobalAudioManager.instance.buttonClickedSound();
        lastActionTime = Time.timeSinceLevelLoad;
        Debug.Log("Instantiating action with index : " + index);
        GlobalEventsManager.instance.currentActionIndex++;
        GlobalEventsManager.instance.prevActionIndexes[GlobalEventsManager.instance.currentActionIndex]=index;
        Instantiate(possible_Actions[index].actionGameObject,transform.position,Quaternion.identity);
        Debug.Log("Summoned action " + index);
        movesLeftText.text = " Moves Left: " + (GlobalEventsManager.instance.maxMoves - GlobalEventsManager.instance.currentActionIndex).ToString();

    }
    public void resetDice()
    {
        GlobalEventsManager.instance.summonNewDice();
        GlobalEventsManager.instance.currentActionIndex = 0;
        movesLeftText.text = " Moves Left: " + (GlobalEventsManager.instance.maxMoves - GlobalEventsManager.instance.currentActionIndex).ToString();

    }
    public void revertAction()
    {

        if (GlobalEventsManager.instance.currentActionIndex <= 0)
        {
            GlobalAudioManager.instance.wrongSound();
            //unable to revery
            return;
        }
        if (Time.timeSinceLevelLoad - lastActionTime < timeBetweenActions)
        {
            GlobalAudioManager.instance.earlySound();
            Debug.Log("TimeNotUp");
            return;
        }
        lastActionTime = Time.timeSinceLevelLoad;
        GlobalAudioManager.instance.reverseButtonSound();
        int actionIndex = GlobalEventsManager.instance.prevActionIndexes[GlobalEventsManager.instance.currentActionIndex];
        GlobalEventsManager.instance.prevActionIndexes[GlobalEventsManager.instance.currentActionIndex] = -1;
        Instantiate(possible_Actions[actionIndex].revertActionGameObject, transform.position, Quaternion.identity);
        GlobalEventsManager.instance.currentActionIndex--;
        movesLeftText.text = " Moves Left: " + (GlobalEventsManager.instance.maxMoves - GlobalEventsManager.instance.currentActionIndex).ToString();

    }
    public void cancelledAction()
    {
        int actionIndex = GlobalEventsManager.instance.prevActionIndexes[GlobalEventsManager.instance.currentActionIndex];
        GlobalEventsManager.instance.prevActionIndexes[GlobalEventsManager.instance.currentActionIndex] = -1;
        GlobalEventsManager.instance.currentActionIndex--;

    }
    private void Awake()
    {

        instance = this;
    }
    private void Start()
    {
        setButtons();
        setIcons();
        movesLeftText.text = " Moves Left: " + (GlobalEventsManager.instance.maxMoves - GlobalEventsManager.instance.currentActionIndex).ToString();

    }
    public void setIcons()
    {
        for (int i = 0; i < GlobalEventsManager.instance.neededValues.Length;i++) {
            diceIconsImage[i].sprite = diceIcons[GlobalEventsManager.instance.neededValues[i]];
            diceIconsImage[i].gameObject.SetActive(true);
        }
    }
    public void updateCheckMarks(bool[] success)
    {
        for(int i = 0; i < 6; i++)
        {
            diceIconsCheckMark[i].SetActive(success[i]);
        }
    }
    void setButtons()
    {
        for (int i = 0; i < assignedActions.Length;i++) {
            int tempInt=assignedActions[i];
            if (tempInt == -1)
            {
                actionButton[i].gameObject.SetActive(false);
            }
            else
            {
                //actionButton[i].onClick = null;
                actionButton[i].onClick.AddListener(delegate { instantiateActions(tempInt); });
                actionIconImage[i].sprite= possible_Actions[assignedActions[i]].actionImage;
                actionImage[i].sprite = possible_Actions[assignedActions[i]].actionImage;
                actionName[i].text = possible_Actions[assignedActions[i]].actionName;
                actionDescription[i].text = possible_Actions[assignedActions[i]].actionDescription;
            }
        }
        for(int i=assignedActions.Length; i < 6; i++)
        {
            actionButton[i].gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        levelText.text = "Level: " + GlobalSceneManager.currentLevel;
        if (!GlobalEventsManager.instance.paused&&(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("CTRL+Z ACTION");
            revertAction();
        }
    }
}
