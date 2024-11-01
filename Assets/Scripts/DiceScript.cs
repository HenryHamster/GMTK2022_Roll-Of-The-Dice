using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DiceScript : MonoBehaviour
{
    public int currentValueIndex;
    public int topValueIndex;
    public int leftValueIndex;
    public int[] values;
    public int position;
    public int[] sprites;
    public Transform model;
    public Vector3[] setRotations;
    public Vector3 targetRotation;
    public float timeSetRotation;
    public float rotationSpeed;
    public ParticleSystem particleEffect;
    public ParticleSystem winParticleEffect;
    public Collider2D self_Collider;
    public SpriteRenderer topDiceFace;
    public SpriteRenderer leftDiceFace;
    public SpriteRenderer rightDiceFace;
    public SpriteRenderer bottomDiceFace;
    public Sprite[] diceFaceSprites;
    public bool wasHoveringLastFrame;
    private void Awake()
    {
        self_Collider = GetComponent<Collider2D>();
        particleEffect.Stop();
    }
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.W))
        {
            rollDiceForward();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            rollDiceLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            rollDiceRight();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            rollDiceDown();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            rotateDiceLeft();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            rotateDiceRight();
        }
        */
        if (self_Collider.bounds.Contains((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            if (!wasHoveringLastFrame)
            {
                wasHoveringLastFrame = true;
                GlobalAudioManager.instance.buttonHoverSound();
            }
            topDiceFace.gameObject.SetActive(true);
            bottomDiceFace.gameObject.SetActive(true);
            leftDiceFace.gameObject.SetActive(true);
            rightDiceFace.gameObject.SetActive(true);
            topDiceFace.sprite = diceFaceSprites[topValueIndex];
            leftDiceFace.sprite = diceFaceSprites[leftValueIndex];
            bottomDiceFace.sprite = diceFaceSprites[5-topValueIndex];
            rightDiceFace.sprite = diceFaceSprites[5-leftValueIndex];
        }
        else
        {
            wasHoveringLastFrame = false;
            topDiceFace.gameObject.SetActive(false);
            bottomDiceFace.gameObject.SetActive(false);
            leftDiceFace.gameObject.SetActive(false);
            rightDiceFace.gameObject.SetActive(false);
        }
        //Vector3 tempQuat = oldRot * (inverse oldRot* Camera.main.transform.up)
        //Debug.Log("Euler Angles" + Quaternion.Lerp(model.rotation, Quaternion.Euler(targetRotation), Mathf.Clamp01((Time.timeSinceLevelLoad - timeSetRotation) / rotationSpeed)).eulerAngles);
        //Vector3 savedRot = model.localEulerAngles;
        //model.rotation = Quaternion.identity;
        //model.rotation = Quaternion.Lerp(model.rotation, Quaternion.Euler(targetRotation), Mathf.Clamp01((Time.timeSinceLevelLoad - timeSetRotation) / rotationSpeed));
        //model.rotation = Quaternion.Euler(targetRotation);
        //transform.rotation=/*(Quaternion.AngleAxis(Mathf.Lerp(model.localRotation.x, targetRotation.x, Mathf.Clamp01((Time.timeSinceLevelLoad - timeSetRotation) / rotationSpeed)), Camera.main.transform.forward)*
        //Quaternion.AngleAxis( Mathf.Lerp(model.localRotation.z, targetRotation.z, Mathf.Clamp01((Time.timeSinceLevelLoad - timeSetRotation) / rotationSpeed)), Camera.main.transform.right) *
        //Quaternion.AngleAxis(Mathf.Lerp(model.localRotation.y, targetRotation.y, Mathf.Clamp01((Time.timeSinceLevelLoad - timeSetRotation) / rotationSpeed)), Camera.main.transform.up);*/

    }
    public void rollDiceForward()
    {
        //timeSetRotation = Time.timeSinceLevelLoad;
        model.DORotate(new Vector3(-90, 0,0 ), 0.5f, RotateMode.WorldAxisAdd);
        particleEffect.Stop();
        particleEffect.Play();
        //targetRotation += transform.TransformDirection(new Vector3(0, 0, 90));
        int savedValue =  currentValueIndex;
        currentValueIndex = topValueIndex;
        topValueIndex = 5 - savedValue;
        GlobalAudioManager.instance.playDiceSound();
        GlobalEventsManager.instance.updateDice();
    }
    public void rollDiceDown()
    {
        timeSetRotation = Time.timeSinceLevelLoad;
        model.DORotate(new Vector3(90, 0, 0), 0.5f, RotateMode.WorldAxisAdd);

        particleEffect.Stop();
        particleEffect.Play();
        targetRotation += transform.TransformDirection(new Vector3(0, 0, -90));
        int savedValue = currentValueIndex;
        currentValueIndex = 5 - topValueIndex;
        topValueIndex = savedValue;
        GlobalAudioManager.instance.playDiceSound();
        GlobalEventsManager.instance.updateDice();
    }
    public void rollDiceLeft()
    {
        model.DORotate(new Vector3(0, -90, 0), 0.5f, RotateMode.WorldAxisAdd);

        timeSetRotation = Time.timeSinceLevelLoad;

        particleEffect.Stop();
        particleEffect.Play();
        targetRotation += transform.TransformDirection(new Vector3(0, 90, 0));

        int savedValue = currentValueIndex;

        currentValueIndex =  leftValueIndex;
        leftValueIndex = 5 - savedValue;
        GlobalAudioManager.instance.playDiceSound();
        GlobalEventsManager.instance.updateDice();
    }
    public void rollDiceRight()
    {
        model.DORotate(new Vector3(0, 90,0), 0.5f, RotateMode.WorldAxisAdd);

        particleEffect.Stop();
        particleEffect.Play();
        timeSetRotation = Time.timeSinceLevelLoad;

        targetRotation += transform.TransformDirection(new Vector3(0, -90, 0));

        int savedValue = currentValueIndex;

        currentValueIndex = 5-leftValueIndex;
        leftValueIndex =  savedValue;
        GlobalAudioManager.instance.playDiceSound();
        GlobalEventsManager.instance.updateDice();
    }
    public void rotateDiceLeft()
    {
        model.DORotate(new Vector3(0, 0, 90), 0.5f, RotateMode.WorldAxisAdd);

        particleEffect.Stop();
        particleEffect.Play();
        timeSetRotation = Time.timeSinceLevelLoad;

        targetRotation += transform.TransformDirection(new Vector3(90,0 , 0));

        int savedValue = topValueIndex;

        topValueIndex = 5 - leftValueIndex;
        leftValueIndex =  savedValue;
        GlobalAudioManager.instance.playDiceSound();
        GlobalEventsManager.instance.updateDice();
    }
    public void rotateDiceRight()
    {
        model.DORotate(new Vector3(0, 0, -90), 0.5f, RotateMode.WorldAxisAdd);

        timeSetRotation = Time.timeSinceLevelLoad;

        targetRotation += transform.TransformDirection(new Vector3(-90, 0, 0));

        int savedValue = topValueIndex;

        topValueIndex = leftValueIndex;
        leftValueIndex = 5 - savedValue;
        GlobalAudioManager.instance.playDiceSound();
        GlobalEventsManager.instance.updateDice();
    }
    public void rollDiceForward(bool save)
    {
        //timeSetRotation = Time.timeSinceLevelLoad;
        model.DORotate(new Vector3(-90, 0, 0), 0.5f, RotateMode.WorldAxisAdd);
        particleEffect.Stop();
        particleEffect.Play();
        //targetRotation += transform.TransformDirection(new Vector3(0, 0, 90));
        int savedValue = currentValueIndex;
        currentValueIndex = topValueIndex;
        topValueIndex = 5 - savedValue;
        GlobalAudioManager.instance.playDiceSound();
        if(save)GlobalEventsManager.instance.updateDice();
    }
    public void rollDiceDown(bool save)
    {
        timeSetRotation = Time.timeSinceLevelLoad;
        model.DORotate(new Vector3(90, 0, 0), 0.5f, RotateMode.WorldAxisAdd);

        particleEffect.Stop();
        particleEffect.Play();
        targetRotation += transform.TransformDirection(new Vector3(0, 0, -90));
        int savedValue = currentValueIndex;
        currentValueIndex = 5 - topValueIndex;
        topValueIndex = savedValue;
        GlobalAudioManager.instance.playDiceSound();
        if (save) GlobalEventsManager.instance.updateDice();
    }
    public void rollDiceLeft(bool save)
    {
        model.DORotate(new Vector3(0, -90, 0), 0.5f, RotateMode.WorldAxisAdd);

        timeSetRotation = Time.timeSinceLevelLoad;

        particleEffect.Stop();
        particleEffect.Play();
        targetRotation += transform.TransformDirection(new Vector3(0, 90, 0));

        int savedValue = currentValueIndex;

        currentValueIndex = leftValueIndex;
        leftValueIndex = 5 - savedValue;
        GlobalAudioManager.instance.playDiceSound();
        if (save) GlobalEventsManager.instance.updateDice();
    }
    public void rollDiceRight(bool save)
    {
        model.DORotate(new Vector3(0, 90, 0), 0.5f, RotateMode.WorldAxisAdd);

        particleEffect.Stop();
        particleEffect.Play();
        timeSetRotation = Time.timeSinceLevelLoad;

        targetRotation += transform.TransformDirection(new Vector3(0, -90, 0));

        int savedValue = currentValueIndex;

        currentValueIndex = 5 - leftValueIndex;
        leftValueIndex = savedValue;
        GlobalAudioManager.instance.playDiceSound();
        if (save) GlobalEventsManager.instance.updateDice();
    }
    public void rotateDiceLeft(bool save)
    {
        model.DORotate(new Vector3(0, 0, 90), 0.5f, RotateMode.WorldAxisAdd);

        particleEffect.Stop();
        particleEffect.Play();
        timeSetRotation = Time.timeSinceLevelLoad;

        targetRotation += transform.TransformDirection(new Vector3(90, 0, 0));

        int savedValue = topValueIndex;

        topValueIndex = 5 - leftValueIndex;
        leftValueIndex = savedValue;
        GlobalAudioManager.instance.playDiceSound();
        if (save) GlobalEventsManager.instance.updateDice();
    }
    public void rotateDiceRight(bool save)
    {
        model.DORotate(new Vector3(0, 0, -90), 0.5f, RotateMode.WorldAxisAdd);

        timeSetRotation = Time.timeSinceLevelLoad;

        targetRotation += transform.TransformDirection(new Vector3(-90, 0, 0));

        int savedValue = topValueIndex;

        topValueIndex = leftValueIndex;
        leftValueIndex = 5 - savedValue;
        GlobalAudioManager.instance.playDiceSound();
        if (save) GlobalEventsManager.instance.updateDice();
    }

}
