using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] lines;
}

[System.Serializable]
public class ChoiceData
{
    public string choiceText;
    public int nextLineIndex;
    public string flagToSet;
}


[Serializable]
public class DialogueLine
{
    public string characterName;

    public Sprite characterPortrait;

    [TextArea(2, 5)]
    public string dialogueText;

    public ChoiceData[] choices;
}
