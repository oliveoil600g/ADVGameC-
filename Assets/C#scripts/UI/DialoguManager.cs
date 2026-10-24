using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialoguManager : MonoBehaviour
{
    [Header("Dialogue Data")]
    [SerializeField] private DialogueData dialogueData;

    [Header("Dialogue UI")]
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Image characterPortraitImage;

    [Header("Text Settings")]
    [SerializeField] private float textSpeed = 0.05f;

    [Header("Choice UI")]
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Button[] choiceButtons;

    // 現在のセリフ番号
    private int currentLine = 0;

    // 文字送り中か
    private bool isTyping = false;

    // 現在の文字送りコルーチン
    private Coroutine typingCoroutine;

    private void Start()
    {
        // 会話データが設定されているか確認
        if (dialogueData == null)
        {
            Debug.LogError("DialogueDataが設定されていません！");
            return;
        }

        // セリフが存在するか確認
        if (dialogueData.lines == null || dialogueData.lines.Length == 0)
        {
            Debug.LogError("DialogueDataにセリフがありません！");
            return;
        }

        // 次へボタン
        nextButton.onClick.AddListener(NextLine);

        // 最初のセリフを表示
        ShowLine();
    }

    private void ShowLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // 新しいセリフを表示するときは選択肢を隠す
        choicePanel.SetActive(false);

        nextButton.gameObject.SetActive(true);

        DialogueLine currentDialogue =
            dialogueData.lines[currentLine];

        // キャラクター名を変更
        characterNameText.text =
            currentDialogue.characterName;

        // 立ち絵を変更
        characterPortraitImage.sprite =
            currentDialogue.characterPortrait;

        // 立ち絵が設定されていなければ非表示
        characterPortraitImage.gameObject.SetActive(
            currentDialogue.characterPortrait != null
        );

        // 文字送り開始
        typingCoroutine =
            StartCoroutine(
                TypeText(currentDialogue.dialogueText)
            );
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;

        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;

            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        typingCoroutine = null;

        // 文字送りが終わったあとに選択肢を表示
        DialogueLine currentDialogue =
            dialogueData.lines[currentLine];

        if (currentDialogue.choices != null &&
            currentDialogue.choices.Length > 0)
        {
            ShowChoices();
        }
    }

    private void NextLine()
    {
        // 文字送り中なら全文表示
        if (isTyping)
        {
            ShowFullText();
            return;
        }

        // 次のセリフへ
        currentLine++;

        // まだセリフがある
        if (currentLine < dialogueData.lines.Length)
        {
            ShowLine();
        }
        else
        {
            EndDialogue();
        }
    }
    private void ShowChoices()
    {
        DialogueLine currentDialogue =
        dialogueData.lines[currentLine];

        if (currentDialogue.choices == null ||
            currentDialogue.choices.Length == 0)
        {
            return;
        }

        choicePanel.SetActive(true);
        nextButton.gameObject.SetActive(false);

        int buttonIndex = 0;

        // いったん全部の選択肢を非表示にする
        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }

        // 表示条件を満たす選択肢だけ表示する
        for (int i = 0;
             i < currentDialogue.choices.Length;
             i++)
        {
            ChoiceData choice = currentDialogue.choices[i];

            // 必要なフラグがある場合、ONかどうか確認
            if (!string.IsNullOrEmpty(choice.requiredFlag))
            {
                if (FlagManager.Instance == null ||
                    !FlagManager.Instance.HasFlag(choice.requiredFlag))
                {
                    continue;
                }
            }

            // ボタン数の上限を超えたら終了
            if (buttonIndex >= choiceButtons.Length)
            {
                Debug.LogWarning("選択肢のボタン数が足りません！");
                break;
            }

            Button button = choiceButtons[buttonIndex];

            button.gameObject.SetActive(true);

            button.GetComponentInChildren<TMP_Text>().text =
                choice.choiceText;

            // 選択肢の番号を保存
            int choiceIndex = i;

            button.onClick.RemoveAllListeners();

            button.onClick.AddListener(
                () => SelectChoice(choiceIndex)
            );

            buttonIndex++;
        }

        // 表示できる選択肢がなかった場合
        if (buttonIndex == 0)
        {
            choicePanel.SetActive(false);
            nextButton.gameObject.SetActive(true);
        }
    }
    private void SelectChoice(int choiceIndex)
    {
        DialogueLine currentDialogue =
        dialogueData.lines[currentLine];

        ChoiceData selectedChoice =
            currentDialogue.choices[choiceIndex];

        // フラグが設定されていたらONにする
        if (!string.IsNullOrEmpty(selectedChoice.flagToSet))
        {
            FlagManager.Instance.SetFlag(selectedChoice.flagToSet);
        }

        // 選択肢を非表示
        choicePanel.SetActive(false);

        // 次のセリフへ
        currentLine = selectedChoice.nextLineIndex;

        nextButton.gameObject.SetActive(true);

        ShowLine();
    }

    private void ShowFullText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialogueText.text =
            dialogueData.lines[currentLine].dialogueText;

        isTyping = false;
        typingCoroutine = null;

        // 文字送りをスキップした場合でも
        // 選択肢を表示する
        DialogueLine currentDialogue =
            dialogueData.lines[currentLine];

        if (currentDialogue.choices != null &&
            currentDialogue.choices.Length > 0)
        {
            ShowChoices();
        }
    }

    private void EndDialogue()
    {
        Debug.Log("会話終了");

        characterNameText.text = "";
        dialogueText.text = "";

        characterPortraitImage.sprite = null;
        characterPortraitImage.gameObject.SetActive(false);

        nextButton.gameObject.SetActive(false);
    }
}
