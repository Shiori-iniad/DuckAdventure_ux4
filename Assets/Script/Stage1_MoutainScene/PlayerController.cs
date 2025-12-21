using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Gravity")]
    public float riseGravity = 1.5f;   // 上昇中
    public float fallGravity = 3.5f;   // 落下中

    [Header("Player")]
    public float runSpeed = 5f;
    public float jumpForce = 12f;
    public int maxJumpCount = 2;
    int jumpCount = 0;

    Rigidbody2D rb;
    Animator animator;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsMoving", true);
    }

    void Update()
    {
        // 常に右へ走る
        rb.velocity = new Vector2(runSpeed, rb.velocity.y);

        // ジャンプ（改良版）
        if (jumpCount < maxJumpCount && Input.GetKeyDown(KeyCode.Space))
        {
            // 落下速度をリセット
            rb.velocity = new Vector2(rb.velocity.x, 0f);

            // 上向きに力を加える
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            jumpCount++;
        }

        if (rb.velocity.y > 0.1f)
        {
            // 上昇中
            rb.gravityScale = riseGravity;
        }
        else if (rb.velocity.y < -0.1f)
        {
            // 落下中
            rb.gravityScale = fallGravity;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
            rb.gravityScale = fallGravity;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
