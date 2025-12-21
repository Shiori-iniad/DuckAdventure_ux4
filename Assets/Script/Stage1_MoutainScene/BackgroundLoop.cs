using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private float width;
    private Camera mainCamera;

    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        mainCamera = Camera.main;
    }

    void Update()
    {
        float cameraLeft =
            mainCamera.transform.position.x
            - mainCamera.orthographicSize * mainCamera.aspect;

        // 背景が完全にカメラ左に出たら
        if (transform.position.x + width / 2f < cameraLeft)
        {
            transform.position += Vector3.right * width * 3f;
        }
    }
}
