using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScriptableUpgrade", order = 1)]

public class ScriptableAction : ScriptableObject
{

    public GameObject actionGameObject;
    public Sprite actionImage;
    public string actionName;
    public string actionDescription;
    public GameObject revertActionGameObject;
}
