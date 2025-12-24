using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Gravity")]
    public float riseGravity = 1.5f;   // ジャンプ中に適用される重力の強さ
    public float fallGravity = 3.5f;   // 落下中に適用される重力の強さ

    [Header("Player")]
    public float runSpeed = 5f;   // 右方向への自動移動速度
    public float firstJumpForce = 8f;   // 1回目のジャンプ
    public float secondJumpForce = 12f; // 2回目のジャンプ
    public int maxJumpCount = 2;   // ジャンプの最大回数
    private int jumpCount = 0;   // 現在のジャンプ回数

    [Header("HP")]
    public int maxHP = 3;   // プレイヤーの最大HP
    private int currentHP;   // 現在のHP
    public System.Action<int> onHPChanged;   // HPが変化したときに通知するイベント

    [Header("Invincible")]
    public float invincibleTime = 1.0f;   // 無敵時間(秒)
    private bool isInvincible = false;   // 現在無敵かどうか
    public bool IsInvincible => isInvincible;   // プロパティとして外部から無敵状態を参照可能

    [Header("Fade")]
    [SerializeField] private MonoBehaviour fader; // IFaderを実装したコンポーネントをInspectorで設定
    private IFader fadeController;   // フェード処理を呼ぶためのインターフェース参照

    private bool isDead = false;   // 死亡処理の重複防止
    [SerializeField] private string gameOverSceneName;   // ゲームオーバー時に遷移するシーン名


    // プレイヤーのsprite表示えを菅絵里するSpriteRenderer
    [SerializeField] private SpriteRenderer spriteRenderer;

    Rigidbody2D rb;   // 2D物理挙動用のRigidbody2Dコンポーネント
    Animator animator;   // アニメーション制御用のAnimatorコンポーネント



    void Start()
    {
        // Rigidbody2Dを取得
        rb = GetComponent<Rigidbody2D>();
        // Animatorを取得
        animator = GetComponent<Animator>();

        // 初期状態で走るアニメーションに設定
        animator.SetBool("IsMoving", true);

        // 最大値に設定
        currentHP = maxHP;

        // HP変更イベントを呼び出す
        onHPChanged?.Invoke(currentHP);

        // フェード用コンポーネントをIFaderとして取得
        fadeController = fader as IFader;
    }



    void Update()
    {
        // 常に右へ走る
        // ⇔毎フレーム呼ばれる
        // ⇔X方向の速度をrunSpeedに固定。Y方向は物理挙動に任せる
        rb.velocity = new Vector2(runSpeed, rb.velocity.y);

        // スペースキーを押したとき、かつ、ジャンプ関数が上限未満の場合にジャンプ
        if (jumpCount < maxJumpCount && Input.GetKeyDown(KeyCode.Space))
        {
            // rb.velocity.y = 0fで現在の上方向速度をリセット
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            float power = (jumpCount == 0) ? firstJumpForce : secondJumpForce;
            // AddForceで瞬間的に上方向に力を加える
            rb.AddForce(Vector2.up * power, ForceMode2D.Impulse);
            // ジャンプ回数を増やす
            jumpCount++;
        }

        // 重力制御 (上昇中はriseGravity、下降中はfallGravityに設定)
        if (rb.velocity.y > 0.1f)
            rb.gravityScale = riseGravity;
        else if (rb.velocity.y < -0.1f)
            rb.gravityScale = fallGravity;
    }



    // 地面との接触判定
    void OnCollisionEnter2D(Collision2D collision)
    {
        // もし、地面に接触したら
        if (collision.gameObject.CompareTag("Ground"))
        {
            // ジャンプの回数をリセット
            jumpCount = 0;
            // 重力を落下用に戻す
            rb.gravityScale = fallGravity;
        }
    }



    // HP管理関数
    public int GetHP()
    {
        // 現在のHPを返す
        return currentHP;
    }



    // ダメージ処理
    public void TakeDamage(int damage)
    {
        // 無敵中ならダメージを受けない
        if (isInvincible) return;

        // HPを減らして0未満にならないようMathf.Maxにする
        currentHP = Mathf.Max(currentHP - damage, 0);
        // HP変更イベントを呼ぶ
        onHPChanged?.Invoke(currentHP);

        // もし、現在のHPが0なら
        if (currentHP <= 0)
        {
            // Dieメソッドを呼ぶ
            Die();
        }
        // まだ残っている場合は
        else
        {
            // 無敵コルーチン開始
            StartCoroutine(InvincibleCoroutine());
        }
    }



    // 回復処理
    public void Heal(int value)
    {
        // HPが最大を超えないようにMathf.Min
        currentHP = Mathf.Min(currentHP + value, maxHP);
        // HP回復イベントを呼ぶ
        onHPChanged?.Invoke(currentHP);
    }



    // 死亡時の処理
    public void Die()
    {
        if (isDead) return;   // 二重呼び出し防止

        // ゲームオーバー処理
        Debug.Log("Player Dead");

        // プレイヤー操作を止める
        rb.velocity = Vector2.zero;
        rb.simulated = false;   // 物理停止

        // フェード可能なら
        if (fadeController != null)
        {
            // フェードしてゲームオーバーシーンへ
            fadeController.FadeToScene(gameOverSceneName);
        }
        else
        {
            // コンソールにログを表示
            Debug.LogError("Fader が設定されていません");
        }
    }



    // 無敵コルーチン
    private IEnumerator InvincibleCoroutine()
    {
        // プレイヤーを無敵状態に設定
        isInvincible = true;
        // 無敵時間の経過を管理するためのタイマー変数
        // ⇔0からinvincibleTimeまで増やしていきます
        float timer = 0f;

        // 無敵時間が終わるまでループする
        while (timer < invincibleTime)
        {
            // プレイヤーのspriteをON/OFF切り替えている (=点滅しているように見える)
            spriteRenderer.enabled = !spriteRenderer.enabled;
            // コルーチンを0.1秒停止 (=点滅の速度を制御)
            yield return new WaitForSeconds(0.1f);
            // タイマーを0.1秒分進める
            timer += 0.1f;
        }

        // 無敵終了時にspriteを表示状態に戻す
        spriteRenderer.enabled = true;
        // 無敵状態を解除
        isInvincible = false;
    }

}
