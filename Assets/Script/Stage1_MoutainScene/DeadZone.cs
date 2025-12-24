using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // 2Dトリガーに他のColliderが入った瞬間に自動で呼ばれる関数
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 触れた相手のタグがPlayerかどうかを判定
        if (collision.CompareTag("Player"))
        {
            // コンソールにログを表示
            Debug.Log("Fall Game Over");

            // 衝突したオブジェクトからPlayerControllerコンポーネントを取得
            PlayerController player = collision.GetComponent<PlayerController>();

            // PlayerControllerがちゃんと取得できたか確認
            if (player != null)
            {
                // PlayerControllerにDie()メソッドを呼び出す
                player.SendMessage("Die");
            }
        }
    }
}
