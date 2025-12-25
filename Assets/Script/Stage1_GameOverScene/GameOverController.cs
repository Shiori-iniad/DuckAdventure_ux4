using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField] private MonoBehaviour fader; // IFader
    private IFader fadeController;

    void Awake()
    {
        fadeController = fader as IFader;
        if (fadeController == null)
        {
            Debug.LogError("Fader が設定されていません");
        }
    }

    // ① HomeScene へ
    public void GoToTitleScene()
    {
        FadeTo("HomeScene");
    }

    void FadeTo(string sceneName)
    {
        if (fadeController != null)
        {
            fadeController.FadeToScene(sceneName);
        }
    }
}

