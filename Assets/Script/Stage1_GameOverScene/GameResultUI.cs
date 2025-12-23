using UnityEngine;
using TMPro;

public class GameResultUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI stagePointText;

    void Start()
    {
        // 今回のステージで獲得したポイント
        stagePointText.text =
        GameCoinManager.Instance.StagePoint + " pt";

    }
}
