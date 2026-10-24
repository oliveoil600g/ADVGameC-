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

    // 表示するために必要なフラグ
    public string requiredFlag;
}


[Serializable]
public class DialogueLine
{
    public string characterName;

    public Sprite characterPortrait;

    [TextArea(2, 5)]
    public string dialogueText;

    public ChoiceData[] choices;

    // このセリフを表示するために必要なフラグ
    public string requiredFlag;
}
