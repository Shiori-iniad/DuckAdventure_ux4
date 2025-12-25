using UnityEngine;

public class HomeSceneController : MonoBehaviour
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

    // ① StartScene へ
    public void GoToStartScene()
    {
        FadeTo("StartScene");
    }

    // ② PrologueScene へ
    public void GoToPrologueScene()
    {
        FadeTo("TutorialScene");
    }

    // ③ Stage1_PrologueScene へ
    public void GoToStage1PrologueScene()
    {
        FadeTo("Stage1_Prologue");
    }

    void FadeTo(string sceneName)
    {
        if (fadeController != null)
        {
            fadeController.FadeToScene(sceneName);
        }
    }
}
