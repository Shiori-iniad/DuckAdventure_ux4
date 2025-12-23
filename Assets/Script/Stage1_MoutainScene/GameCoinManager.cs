using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoinManager : MonoBehaviour
{
    public static GameCoinManager Instance;
    public int StagePoint { get; private set; }   // 今回のステージ分
    public int TotalPoint { get; private set; }   // 累計ポイント



    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadTotalPoint();
        ResetStagePoint();
    }



    // ===== コイン取得 =====
    public void AddStagePoint(int value)
    {
        StagePoint += value;
    }



    // ===== GameClear 時に呼ぶ =====
    public void CommitStagePoint()
    {
        TotalPoint += StagePoint;
        SaveTotalPoint();
    }



    // ===== ステージ開始 or 終了時 =====
    public void ResetStagePoint()
    {
        StagePoint = 0;
    }



    // ===== セーブ関連 =====
    void SaveTotalPoint()
    {
        PlayerPrefs.SetInt("TotalPoint", TotalPoint);
    }



    void LoadTotalPoint()
    {
        TotalPoint = PlayerPrefs.GetInt("TotalPoint", 0);
    }
}
