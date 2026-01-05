using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;   // 追従対象
    public Vector3 offset = new Vector3(3f, 0f, -10f);   // カメラとターゲットの相対的な位置のずれ
    public float smoothSpeed = 5f;   // カメラ追従のなめらかさ



    // すべてのUpdateが終わった後に呼ばれる
    void LateUpdate()
    {
        // 追従対象が設定されていない場合の安全対策
        if (target == null) return;

        // カメラが最終的に行きたい位置を計算
        // ⇔target.position.x(=プレイヤーのX座標) + offset.x(=オフセット分、右または左へずらす)
        // ⇔transform.position.y,(=現在のカメラのY座標)
        // ⇔offset.z(=オフセットのZ座標)
        Vector3 desired = new Vector3(
            target.position.x + offset.x,
            transform.position.y,
            offset.z
        );

        // 現在位置から目標位置へなめらかに移動させる処理
        // ⇔transform.position(=Lerpの開始地点(=今のカメラ位置))
        // ⇔desired(=Lerpの到着地点(=さっき計算した目標位置))
        // ⇔smoothSpeed * Time.deltaTime(=移動割合)
        transform.position = Vector3.Lerp(
            transform.position,
            desired,
            smoothSpeed * Time.deltaTime
        );
    }
}