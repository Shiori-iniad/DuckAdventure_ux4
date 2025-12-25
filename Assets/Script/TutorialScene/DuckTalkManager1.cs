using UnityEngine;
using TMPro;
using System.Collections;
public class DuckTalkManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject talkPanel;
    [SerializeField] private TextMeshProUGUI talkText;

    [SerializeField] private float charInterval = 0.05f;

    private string[] dialogues;
    private int currentIndex;
    private bool isTyping;
    private bool isActive;

    public void StartTalk(string[] newDialogues)
    {
        StopAllCoroutines();
        talkText.text = ""; 
        isTyping = false;

        dialogues = newDialogues;
        currentIndex = 0;
        isActive = true;
         
        talkPanel.SetActive(true);
        ShowNext();
    }

    public void NextShow()
    {
        if (!isActive) return;

        if (isTyping)
        {
            StopAllCoroutines();
            talkText.text = dialogues[currentIndex - 1];
            isTyping = false;
        }
        else
        {
            ShowNext();
        }
    }

    void ShowNext()
    {
        if (currentIndex >= dialogues.Length)
        {
            EndTalk();
            return;
        }

        StartCoroutine(TypeText(dialogues[currentIndex]));
        currentIndex++;
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        talkText.text = "";

        foreach (char c in text)
        {
            talkText.text += c;
            yield return new WaitForSeconds(charInterval);
        }

        isTyping = false;
    }

    void EndTalk()
    {
        isActive = false;
        isTyping = false;
        talkPanel.SetActive(false);
        TutorialManager.Instance.OnDuckTalkFinished();
    }
}
