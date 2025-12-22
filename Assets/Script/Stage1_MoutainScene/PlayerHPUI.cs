using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUI : MonoBehaviour
{
    [SerializeField, Header("HPアイコン")]
    private GameObject hpIconPrefab;   // HPを表示するハートアイコンのPrefabを指定する変数

    [SerializeField]
    private PlayerController player;   // HP情報を取得する対象のプレイヤーを設定する変数

    [SerializeField] private Sprite fullHeartSprite;    // 満タンハートsprite
    [SerializeField] private Sprite emptyHeartSprite;   // 空洞ハートsprite

    // プレイヤーのHPに応じて生成されるハートアイコンのImageコンポーネント配列
    private Image[] hpIcons;   



    void Start()
    {
        // hpIcons配列をプレイヤーの最大HP数で初期化
        hpIcons = new Image[player.maxHP];

        // プレイヤーの最大HP分、ハートアイコンを生成してUIに配置
        for (int i = 0; i < player.maxHP; i++)
        {
            // Instantiate(hpIconPrefab, transform)で、このスクリプトがアタッチされたオブジェクトの子として生成
            GameObject obj = Instantiate(hpIconPrefab, transform);
            // 生成したオブジェクトのImageコンポーネントをhpIcons配列に格納
            hpIcons[i] = obj.GetComponent<Image>();
        }

        // プレイヤーのHPが変化した時にUpdateHPUI関数を呼ぶようにイベント登録
        player.onHPChanged += UpdateHPUI;

        // ゲーム開始時に現在のHPでUIを初期化
        UpdateHPUI(player.GetHP());
    }



    // HPUI更新関数
    private void UpdateHPUI(int currentHP)
    {
        for(int i = 0; i < hpIcons.Length; i++)
        {
            // 配列のインデックスがcurrentHP未満なら満タンハート
            if (i < currentHP)
                hpIcons[i].sprite = fullHeartSprite;
            // そうでなければ空洞ハート
            else
                hpIcons[i].sprite = emptyHeartSprite;
        }
    }



    // イベント解除処理
    private void OnDestroy()
    {
        if (player != null)
        {
            // スクリプトが破棄されるときにイベント登録を解除
            player.onHPChanged -= UpdateHPUI;
        }
    }
}
