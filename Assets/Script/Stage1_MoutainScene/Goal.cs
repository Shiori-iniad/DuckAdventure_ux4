using UnityEngine;

public class Goal : MonoBehaviour
{
    // IFaderが直接SerializeFieldできないため、MonoBehaviour型にしている
    [SerializeField] private MonoBehaviour fader;
    private IFader fadeController;   // フェード処理を呼び出すためのインターフェース参照
    [SerializeField] private string gameClearSceneName;   // クリア後に遷移するシーン名
    private bool isCleared = false;   // すでにクリア処理を行ったかどうかのフラグ



    void Start()
    {
        // faderをIFader型として扱えるか試す
        // ⇔成功すればfadeControllerに代入
        // ⇔失敗するとnull
        fadeController = fader as IFader;
    }



    // ゴール判定(=2Dトリガーに他のColliderが入った瞬間に呼ばれる)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // すでにクリア処理済みなら何もしない
        if (isCleared) return;

        // 触れたオブジェクトがPlayerタグか判定
        if (collision.CompareTag("Player"))
        {
            // クリア済みフラグをON
            isCleared = true;

            // プレイヤーからPlayerControllerを取得
            PlayerController player = collision.GetComponent<PlayerController>();

            // PlayerControllerが取得できたか確認
            if (player != null)
            {
                // PlayerControllerを無効化(=操作・移動・入力を停止)
                player.enabled = false;
            }

            // フェード処理が正しく取得できているか確認
            if (fadeController != null)
            {
                // IFaderに定義されたメソッドを呼ぶ
                fadeController.FadeToScene(gameClearSceneName);
            }
            // フェード処理が設定されていない場合
            else
            {
                // コンソールにエラー表示
                Debug.LogError("Fader が設定されていません");
            }
        }
    }
}
