using UnityEngine;
using TMPro;
using System.Collections;

public enum TutorialTalkType
{
    None,
    Jump,
    Coin,
    Goal,
    Damage
}


public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [SerializeField] private PlayerController2 player;
    [SerializeField] private DuckTalkManager duckTalk;

    [Header("Dialogues")]
    [SerializeField] private DialogueSet talkJump;
    [SerializeField] private DialogueSet talkCoin;
    [SerializeField] private DialogueSet talkGoal;
    [SerializeField] private DialogueSet talkDamage;

    [Header("Fade")]
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private string nextSceneName;


    private TutorialTalkType currentTalk = TutorialTalkType.None;



    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(StartJumpTalkRoutine());
    }

    IEnumerator StartJumpTalkRoutine()
    {
        currentTalk = TutorialTalkType.Jump;
        LockPlayer();

        yield return new WaitForSeconds(1f);

        duckTalk.StartTalk(talkJump.lines);
    }

    public void OnDuckTalkFinished()
    {
        if (currentTalk == TutorialTalkType.Damage)
        {
            // ★ 最後の会話だけ特別処理
            StartCoroutine(FadeAndNextScene());
        }
        else
        {
            UnlockPlayer();
        }
    }

    void LockPlayer()
    {
        player.canControl = false;
    }

    void UnlockPlayer()
    {
        player.canControl = true;
    }

    public void StartCoinTalk()
    {
        StartCoroutine(StartCoinTalkRoutine());
    }

    IEnumerator StartCoinTalkRoutine()
    {
        currentTalk = TutorialTalkType.Coin;
        LockPlayer();
        yield return new WaitForSeconds(1f);
        duckTalk.StartTalk(talkCoin.lines);
    }

    public void StartGoalTalk()
    {
        StartCoroutine(StartGoalTalkRoutine());
    }

    IEnumerator StartGoalTalkRoutine()
    {
        currentTalk = TutorialTalkType.Goal;
        LockPlayer();
        yield return new WaitForSeconds(1f);
        duckTalk.StartTalk(talkGoal.lines);
    }

    public void StartDamageTalk()
    {
        StartCoroutine(StartDamageTalkRoutine());
    }

    IEnumerator StartDamageTalkRoutine()
    {
        currentTalk = TutorialTalkType.Damage;
        LockPlayer();
        yield return new WaitForSeconds(1f);
        duckTalk.StartTalk(talkDamage.lines);
    }



    IEnumerator FadeAndNextScene()
    {
        yield return new WaitForSeconds(1f);
        sceneFader.FadeToScene(nextSceneName);
    }

}
