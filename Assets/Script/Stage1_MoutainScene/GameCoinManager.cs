using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoinManager : MonoBehaviour
{
    public static GameCoinManager Instance;   // シングルトン用の変数
    public int StagePoint { get; private set; }   // ステージ中に獲得したポイント
    public int TotalPoint { get; private set; }   // 全ステージ通算のポイント
    public System.Action<int> onStagePointChanged;   // StagePointが変わった時に呼ばれるイベント通知用



    void Awake()
    {
        // すでにインスタンスが存在しているか確認
        if (Instance != null)
        {
            // 2つ目以降のGameCoinManagerを削除
            Destroy(gameObject);
            // これ以上処理せずAwakeを終了
            return;
        }

        // 自分自身を唯一のインスタンスとして登録
        Instance = this;
        // シーン切り替え時に削除されないようにする
        DontDestroyOnLoad(gameObject);

        // 保存されている累計ポイントを読み込む
        LoadTotalPoint();
        // ステージポイントを0に初期化
        ResetStagePoint();
    }



    // ===== コイン取得 =====
    // ステージポイントを増やす関数(=コイン取得時に呼ばれる)
    public void AddStagePoint(int value)
    {
        // ステージポイントを加算
        StagePoint += value;
        // イベントを呼び出す
        onStagePointChanged?.Invoke(StagePoint);
    }



    // ===== GameClear 時に呼ぶ =====
    // ステージ終了時に呼ぶ関数
    public void CommitStagePoint()
    {
        // ステージポイントを累計ポイントに加算
        TotalPoint += StagePoint;
        // 累計ポイントを保存
        SaveTotalPoint();
    }



    // ===== ステージ開始 or 終了時 =====
    // ステージポイントを0に戻す
    public void ResetStagePoint()
    {
        // 初期化
        StagePoint = 0;
        // UIを0表示に更新
        onStagePointChanged?.Invoke(StagePoint);
    }



    // ===== セーブ関連 =====
    // 累計ポイントを保存する関数
    void SaveTotalPoint()
    {
        // TotalPointというキーで保存され、アプリ終了時も保持される
        PlayerPrefs.SetInt("TotalPoint", TotalPoint);
    }



    // 保存された累計ポイントを読み込む
    void LoadTotalPoint()
    {
        // 保存が無ければ0を初期値として使用
        TotalPoint = PlayerPrefs.GetInt("TotalPoint", 0);
    }
}
