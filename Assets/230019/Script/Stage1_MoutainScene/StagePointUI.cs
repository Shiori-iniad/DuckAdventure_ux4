using UnityEngine;
using TMPro;

public class StagePointUI : MonoBehaviour
{
    // ポイントを表示するテキストUI
    // ⇔privateだが、[SerializeField]によりInspectorから設定可能
    [SerializeField] private TextMeshProUGUI pointText;



    void Start()
    {
        // 現在のステージポイントを取得し、UIに反映
        // ⇔開始時に 0pt などを正しく表示するため
        UpdatePoint(GameCoinManager.Instance.StagePoint);

        // GameCoinManagerのイベントにUpdatePointメソッドを登録
        // ⇔ポイントが変わるたびに自動でUI更新される
        GameCoinManager.Instance.onStagePointChanged += UpdatePoint;
    }



    // GameObjectが履きされるときに呼ばれる
    void OnDestroy()
    {
        // シーン遷移後などにGameCoinManagerが残っているか確認
        if (GameCoinManager.Instance != null)
        {
            // 登録したイベントを解除
            GameCoinManager.Instance.onStagePointChanged -= UpdatePoint;
        }
    }



    // ポイント表示を更新する
    void UpdatePoint(int point)
    {
        // 表示文字を更新
        pointText.text = point + "pt";
    }
}
