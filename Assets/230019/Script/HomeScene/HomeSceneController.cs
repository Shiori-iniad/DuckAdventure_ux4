using UnityEngine;
using System.Collections;

public class HomeSceneController : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField] private MonoBehaviour fader; // IFader
    private IFader fadeController;

    [Header("SE")]
    [SerializeField] private AudioSource seAudioSource;
    [SerializeField] private AudioClip defaultClickSE;   // 共通SE
    [SerializeField] private AudioClip stage1ClickSE;    // ★ Stage1用SE

    private bool isPressed = false;

    void Awake()
    {
        fadeController = fader as IFader;
        if (fadeController == null)
        {
            Debug.LogError("Fader が設定されていません");
        }
    }

    // ① StartScene へ（共通SE）
    public void GoToStartScene()
    {
        PlaySEAndFade("StartScene", defaultClickSE);
    }

    // ② TutorialScene へ（共通SE）
    public void GoToPrologueScene()
    {
        PlaySEAndFade("TutorialScene", defaultClickSE);
    }

    // ③ Stage1_Prologue へ（★専用SE）
    public void GoToStage1PrologueScene()
    {
        GameCoinManager.Instance.ResetStagePoint();
        PlaySEAndFade("Stage1_Prologue", stage1ClickSE);
    }

    void PlaySEAndFade(string sceneName, AudioClip se)
    {
        if (isPressed) return;
        isPressed = true;

        StartCoroutine(PlaySEThenFade(sceneName, se));
    }

    IEnumerator PlaySEThenFade(string sceneName, AudioClip se)
    {
        if (seAudioSource != null && se != null)
        {
            seAudioSource.PlayOneShot(se);
            yield return new WaitForSeconds(se.length);
        }

        if (fadeController != null)
        {
            fadeController.FadeToScene(sceneName);
        }
    }
}
