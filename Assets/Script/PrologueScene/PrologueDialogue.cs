using System.Collections;
using UnityEngine;
using TMPro;

public class PrologueDialogue : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Dialogue Settings")]
    [TextArea(3, 6)]
    [SerializeField] private string[] dialogues;
    [SerializeField] private float charInterval = 0.05f;

    [Header("Character Animation")]
    [SerializeField] private Transform characterRoot; // EmptyObject
    [SerializeField] private float talkAmplitude = 5f; // 上下移動量
    [SerializeField] private float talkSpeed = 15f;    // 揺れ速度

    [Header("Fade & Scene")]
    [SerializeField] private MonoBehaviour fader; // IFader想定
    [SerializeField] private string nextSceneName;

    [Header("Start Delay")]
    [SerializeField] private float startDelay = 2f; // セリフ開始までの待機時間


    private IFader fadeController;
    private Vector3 characterBasePos;

    private int currentIndex = 0;
    private bool isTyping = false;
    private bool isFinished = false;



    void Start()
    {
        dialogueText.text = "";
        characterBasePos = characterRoot.localPosition;
        fadeController = fader as IFader;

        StartCoroutine(StartDialogueAfterDelay());
    }



    IEnumerator StartDialogueAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        StartDialogue();
    }


    void Update()
    {
        // ★ 文字表示中だけキャラを上下に動かす
        if (isTyping)
        {
            float y = Mathf.Sin(Time.time * talkSpeed) * talkAmplitude;
            characterRoot.localPosition = characterBasePos + new Vector3(0, y, 0);
        }
        else
        {
            // 元の位置に戻す
            characterRoot.localPosition = characterBasePos;
        }
    }

    // Nextボタンから呼ぶ
    public void OnNextButton()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = dialogues[currentIndex - 1];
            isTyping = false;
            // ★ ここを追加
            if (currentIndex >= dialogues.Length)
            {
                isFinished = true;
            }
            return;
        }
        // 全セリフ終了後
        if (isFinished)
        {
            FadeToNextScene();
            return;
        }

        ShowNextDialogue();
    }

    void StartDialogue()
    {
        currentIndex = 0;
        ShowNextDialogue();
    }

    void ShowNextDialogue()
    {
        if (currentIndex >= dialogues.Length) return;

        StartCoroutine(TypeText(dialogues[currentIndex]));
        currentIndex++;
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(charInterval);
        }

        isTyping = false;

        if (currentIndex >= dialogues.Length)
        {
            isFinished = true;
        }
    }




    void FadeToNextScene()
    {
        if (fadeController != null)
        {
            fadeController.FadeToScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Fader が設定されていません");
        }
    }
}
