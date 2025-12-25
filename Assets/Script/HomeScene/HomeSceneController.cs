using UnityEngine;
using System.Collections;

public class HomeSceneController : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField] private MonoBehaviour fader; // IFader
    private IFader fadeController;

    [Header("SE")]
    [SerializeField] private AudioSource seAudioSource; // SE再生用
    [SerializeField] private AudioClip clickSE;         // 共通SE

    private bool isPressed = false; // 連打防止

    void Awake()
    {
        fadeController = fader as IFader;
        if (fadeController == null)
        {
            Debug.LogError("Fader が設定されていません");
        }
    }

    // ① StartScene へ
    public void GoToStartScene()
    {
        PlaySEAndFade("StartScene");
    }

    // ② TutorialScene へ
    public void GoToPrologueScene()
    {
        PlaySEAndFade("TutorialScene");
    }

    // ③ Stage1_Prologue へ
    public void GoToStage1PrologueScene()
    {
        PlaySEAndFade("Stage1_Prologue");
    }

    // ★ 共通処理
    void PlaySEAndFade(string sceneName)
    {
        if (isPressed) return;
        isPressed = true;

        StartCoroutine(PlaySEThenFade(sceneName));
    }

    IEnumerator PlaySEThenFade(string sceneName)
    {
        // ① SE再生
        if (seAudioSource != null && clickSE != null)
        {
            seAudioSource.PlayOneShot(clickSE);
            yield return new WaitForSeconds(clickSE.length);
        }

        // ② フェードしてシーン遷移
        if (fadeController != null)
        {
            fadeController.FadeToScene(sceneName);
        }
    }
}
