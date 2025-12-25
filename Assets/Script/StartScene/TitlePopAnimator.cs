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
        StartCoroutine(PlayTitleLoop());
    }



    private System.Collections.IEnumerator PlayTitleLoop()
    {
        while (true)
        {
            // タイトル再生
            yield return StartCoroutine(PlayTitleOnce());

            // ★ ループ間隔（5秒）
            yield return new WaitForSeconds(5f);
        }
    }

    private System.Collections.IEnumerator PlayTitleOnce()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            var t = texts[i];

            // 念のため前のTweenを止める
            t.DOKill();

            // 最初は見えなくする
            t.alpha = 0;
            t.transform.localScale = Vector3.zero;

            // フェードイン
            t.DOFade(1, 0.2f);

            // ぷるんバウンド
            t.transform.DOScale(bounceScale, 0.3f).SetEase(Ease.OutBack);
            t.transform.DOScale(1f, 0.2f).SetDelay(0.3f);

            // 次の文字まで待つ
            yield return new WaitForSeconds(delays[i]);
        }
    }
}
