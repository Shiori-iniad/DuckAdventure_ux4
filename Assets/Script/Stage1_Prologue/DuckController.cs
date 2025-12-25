using UnityEngine;
using System.Collections;

public class DuckController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float runSpeed = 5f;

    [Header("Start Delay")]
    [SerializeField] private float walkStartDelay = 2f;

    [Header("Scene Transition")]
    [SerializeField] private float walkDuration = 10f;
    [SerializeField] private MonoBehaviour fader;
    [SerializeField] private string nextSceneName;

    private Rigidbody2D rb;
    private Animator animator;
    private IFader fadeController;

    private bool canWalk = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fadeController = fader as IFader;

        // ★ 最初から歩きアニメーションは再生
        animator.SetBool("IsMoving", true);

        // 歩き出し & シーン遷移制御
        StartCoroutine(WalkSequence());
    }

    void Update()
    {
        if (!canWalk) return;

        // 右に歩くだけ
        rb.velocity = new Vector2(runSpeed, rb.velocity.y);
    }

    IEnumerator WalkSequence()
    {
        // ① 歩く前に2秒待つ
        yield return new WaitForSeconds(walkStartDelay);

        // ② 歩き始める
        canWalk = true;

        // ③ 10秒歩く
        yield return new WaitForSeconds(walkDuration);

        // ④ 次のシーンへ
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
