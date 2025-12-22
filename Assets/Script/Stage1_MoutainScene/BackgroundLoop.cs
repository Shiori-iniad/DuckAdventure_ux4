using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private float width;   // 背景spriteの横幅を保存するための変数
    private Camera mainCamera;   // メインカメラの参照を保存するための変数



    void Start()
    {
        // bounds.size.xは、spriteの幅
        // ⇔この背景の横幅を計算してwidthに保存
        width = GetComponent<SpriteRenderer>().bounds.size.x;

        // シーン内のメインカメラを取得してmainCameraに保存
        mainCamera = Camera.main;
    }



    void Update()
    {
        // カメラの左端のX座標を計算
        // ⇔mainCamera.transform.position.xはカメラの中心X位置
        // ⇔mainCamera.orthographicSizeはカメラの縦方向の表示範囲の半分
        // ⇔main.Camera.aspectはカメラの縦/横の比
        // ⇒中心 - 半幅で左端のX座標を求める
        float cameraLeft =
            mainCamera.transform.position.x
            - mainCamera.orthographicSize * mainCamera.aspect;

        // 背景が完全にカメラ左に出たら
        // ⇔transform.position.xは背景の中心X位置
        // ⇔width / 2f を足すことで、背景の右端のX座標を求める
        if (transform.position.x + width / 2f < cameraLeft)
        {
            // 背景を右に移動させてループする
            // ⇔ width * 3fは背景の幅の3倍移動させている
            transform.position += Vector3.right * width * 3f;
        }
    }
}
