using UnityEngine;
using TMPro;
using DG.Tweening;

public class TitleAnimator : MonoBehaviour
{
    public TextMeshProUGUI[] texts; // T1〜T7 を入れる
    public float[] delays; // 塊ごとの遅延時間
    public float bounceScale = 1.2f;

    private void Start()
    {
        StartCoroutine(PlayTitle());
    }

    private System.Collections.IEnumerator PlayTitle()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            var t = texts[i];

            // 最初は見えなくする
            t.alpha = 0;
            t.transform.localScale = Vector3.zero;

            // 透明→表示
            t.DOFade(1, 0.2f);

            // ぷるんバウンド（Scaleアニメーション）
            t.transform.DOScale(bounceScale, 0.3f).SetEase(Ease.OutBack);
            t.transform.DOScale(1f, 0.2f).SetDelay(0.3f);

            // 次の塊に進むまで待つ
            yield return new WaitForSeconds(delays[i]);
        }
    }
}
