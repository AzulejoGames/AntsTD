using UnityEngine;
using System;
using System.Collections.Generic    ; 

[Serializable]
public struct Dialogue
{

    public string name;
    [TextArea(5, 10)]
    public string dialogueText;
}
[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/DialogueData", order = 1) ]
public class DialogueData : ScriptableObject
{
public List<Dialogue> talkscrip;
}
