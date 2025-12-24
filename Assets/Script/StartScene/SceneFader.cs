using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// フェード演出用クラス(=IFaderでフェード用インターフェースを実装)
public class SceneFader : MonoBehaviour, IFader
{

    [SerializeField] private Image fadeImage;   //フェード用の画像
    [SerializeField] private float fadeSpeed = 1f;   //フェード速度



    private void Start()
    {
        //シーン開始時にフェードイン
        StartCoroutine(FadeIn());
    }

    //シーン切り替え用のメソッド
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }



    //フェードイン(画面を明るくする)
    public IEnumerator FadeIn()
    {
        float alpha = 1f;   //初期状態は真っ暗
        SetAlpha(alpha);
        
        //徐々に透明にしていく
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            SetAlpha(alpha);
            yield return null;   //1フレーム待機
        }
    }



    //フェードアウトして指定シーンに切り替える
    public IEnumerator FadeOutAndLoad(string sceneName)
    {
        float alpha = 0f;   //初期は透明
        
        //徐々に画面を黒くする
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            SetAlpha(alpha);
            yield return null;
        }
        //完全に黒くなった状態で少し待つ
        yield return new WaitForSeconds(0.2f);
        //シーンを読み込む
        SceneManager.LoadScene(sceneName);
    }



    //画像の透明度を変更する
    private void SetAlpha(float a)
    {
        Color c = fadeImage.color;
        c.a = a;
        fadeImage.color = c;
    }



    //フェードアウトだけ行う(シーン遷移なし)
    public IEnumerator FadeOutOnly()
    {
        float alpha = 0f;   //初期は透明
        
        //徐々に画面を黒くする
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            SetAlpha(alpha);
            yield return null;   //1フレーム待機
        }
    }

}

