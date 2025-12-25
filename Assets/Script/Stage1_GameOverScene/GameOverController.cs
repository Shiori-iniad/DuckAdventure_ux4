using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField] private MonoBehaviour fader; // IFader
    private IFader fadeController;

    [Header("SE")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonSE;

    private bool isPressed = false; // 多重押し防止

    void Awake()
    {
        fadeController = fader as IFader;
        if (fadeController == null)
        {
            Debug.LogError("Fader が設定されていません");
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // ① HomeScene へ
    public void GoToTitleScene()
    {
        if (isPressed) return;
        isPressed = true;

        StartCoroutine(PlaySEAndFade("HomeScene"));
    }

    IEnumerator PlaySEAndFade(string sceneName)
    {
        // ★ SE再生
        if (audioSource != null && buttonSE != null)
        {
            audioSource.PlayOneShot(buttonSE);
            yield return new WaitForSeconds(buttonSE.length);
        }

        // ★ フェード遷移
        if (fadeController != null)
        {
            fadeController.FadeToScene(sceneName);
        }
    }
}

