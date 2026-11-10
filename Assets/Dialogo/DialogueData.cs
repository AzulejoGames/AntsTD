using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

[Serializable]
public struct Dialogue
{
    public TutorialStepType type;

    public string name;

    [TextArea(5, 10)]
    public string dialogueText;

    public UnityEvent action;
}

[CreateAssetMenu(
    fileName = "DialogueData",
    menuName = "ScriptableObjects/DialogueData",
    order = 1
)]
public class DialogueData : ScriptableObject
{
    public List<Dialogue> talkscrip;
}