using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    [SerializeField] private MonoBehaviour fader; // IFaderを想定
    [SerializeField] private string nextSceneName;

    private IFader fadeController;

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
        if (fadeController != null)
        {
            fadeController.FadeToScene(nextSceneName);
        }
    }
}
