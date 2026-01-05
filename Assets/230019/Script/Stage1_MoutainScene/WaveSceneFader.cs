using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// フェード演出用クラス(=IFaderでフェード用インターフェースを実装)
public class WaveSceneFader : MonoBehaviour, IFader
{
    [Header("Wave Images (順番に入れる)")]
    [SerializeField] private Image[] waveImages;   // 波アニメーション用の画像配列

    [Header("1枚ごとの表示時間")]
    [SerializeField] private float frameTime = 0.08f;   // 1枚の画像を表示する時間

    [Header("フェード開始までの待機時間")]
    [SerializeField] private float waitBeforeFade = 1f;   // フェード演出が始まるまでの待ち時間



    private void Awake()
    {
        // waveImages配列の中身を1枚ずつ処理
        foreach (var img in waveImages)
        {
            // すべての画像を非表示にする
            img.gameObject.SetActive(false);
        }
    }



    // IFaderインターフェースで定義されたメソッド
    public void FadeToScene(string sceneName)
    {
        // フェード演出をコルーチンで開始
        StartCoroutine(FadeCoroutine(sceneName));
    }



    // フェード演出本体
    private IEnumerator FadeCoroutine(string sceneName)
    {
        // フェード開始前に指定秒数待つ
        yield return new WaitForSeconds(waitBeforeFade);

        // 画像配列を先頭から順に処理
        for (int i = 0; i < waveImages.Length; i++)
        {
            // 前の画像はそのままで、i番目の画像を表示
            waveImages[i].gameObject.SetActive(true);
            // 次の画像表示まで待機
            yield return new WaitForSeconds(frameTime);
        }

        // シーン遷移
        SceneManager.LoadScene(sceneName);
    }
}
