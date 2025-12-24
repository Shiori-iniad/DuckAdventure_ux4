using UnityEngine;
using TMPro;

public class GameResultUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI stagePointText;   // ポイント表示用テキスト



    void Start()
    {
        // 今回のステージで獲得したポイントを表示
        stagePointText.text =
        GameCoinManager.Instance.StagePoint + "pt";

    }
}
