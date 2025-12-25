using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int pointValue = 1;   // コイン1枚当たりの獲得ポイント

    [Header("SE")]
    [SerializeField] private AudioClip getSE;     // コイン取得音
    [SerializeField] private AudioSource audioSource;



    private void Awake()
    {
        // AudioSourceが未設定なら自動取得
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    

    // 2Dトリガーに他のColliderが入った瞬間に自動で呼ばれる関数
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突した相手がPlayerタグでなければ処理しない
        if (!collision.CompareTag("Player")) return;

        // GameCoinManager(シングルトン)にアクセス
        // ⇔AddStagePoint(pointValue)で、今回のステージポイントを増やす処理
        GameCoinManager.Instance.AddStagePoint(pointValue);

        if (audioSource != null && getSE != null)
        {
            audioSource.PlayOneShot(getSE);
        }

        // このコイン自身をシーンから削除
        Destroy(gameObject, 0.3f);

    }
}
