using UnityEngine;
using System.Collections;

public class StartSceneController : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField] private MonoBehaviour fader; // IFaderを想定
    [SerializeField] private string nextSceneName;

    [Header("SE")]
    [SerializeField] private AudioSource seAudioSource; // 効果音用
    [SerializeField] private AudioClip startSE;         // ボタンSE

    private IFader fadeController;
    private bool isPressed = false;

    void Start()
    {
        fadeController = fader as IFader;

        if (fadeController == null)
        {
            Debug.LogError("Fader が設定されていません");
        }
    }

    // ★ Startボタンから呼ばれる
    public void OnStartButton()
    {
        if (isPressed) return;
        isPressed = true;
        StartCoroutine(StartSequence());
    }


    private IEnumerator StartSequence()
    {
        // ① 効果音再生
        if (seAudioSource != null && startSE != null)
        {
            seAudioSource.PlayOneShot(startSE);
            yield return new WaitForSeconds(startSE.length);
        }

        // ② フェードしてシーン遷移
        if (fadeController != null)
        {
            fadeController.FadeToScene(nextSceneName);
        }
    }
}
