using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    public int damage = 1;   // 障害物が与えるダメージ量



    // 2D衝突イベント
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // もし、衝突したオブジェクトがPlayerタグを持っていたら
        if (collision.gameObject.CompareTag("Player"))
        {
            // 衝突したオブジェクトからPlayerControllerコンポーネントを取得
            // ⇔プレイヤーのHPや、ダメージ処理を呼び出せる
            PlayerController player =
                collision.gameObject.GetComponent<PlayerController>();

            // PlayerControllerの取得に成功したかチェック
            if (player != null)
            {
                // プレイヤーにダメージを与える処理を呼ぶ
                // ⇔damegeの値を引数として渡す
                player.TakeDamage(damage);
            }
        }
    }
}
