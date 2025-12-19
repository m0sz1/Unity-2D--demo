using UnityEngine;

public class CardRotateWithIdle : MonoBehaviour
{
    [Header("拖动设置")]
    public float dragSpeed = 3f;            // 鼠标拖动敏感度
    public float tiltLimit = 20f;           // 最大倾斜角度（上下左右）
    public float verticalScale = 0.5f;      // 上下倾斜缩放（越小越不敏感）

    [Header("自然晃动设置")]
    public float idleAmount = 2f;           // 晃动幅度
    public float idleSpeed = 2f;            // 晃动速度

    private Vector2 dragTilt = Vector2.zero;   // X = 上下倾斜, Y = 左右倾斜
    private Vector2 smoothTilt = Vector2.zero;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // 左右倾斜正常
            dragTilt.y += mouseX * dragSpeed;

            // 上下倾斜缩小
            dragTilt.x -= mouseY * dragSpeed * verticalScale;

            dragTilt.x = Mathf.Clamp(dragTilt.x, -tiltLimit, tiltLimit);
            dragTilt.y = Mathf.Clamp(dragTilt.y, -tiltLimit, tiltLimit);
        }
        else
        {
            dragTilt = Vector2.Lerp(dragTilt, Vector2.zero, Time.deltaTime * 2f);
        }

        float idleX = Mathf.Sin(Time.time * idleSpeed) * idleAmount;
        float idleY = Mathf.Cos(Time.time * idleSpeed) * idleAmount;

        smoothTilt.x = dragTilt.x + idleX;
        smoothTilt.y = dragTilt.y + idleY;

        transform.localRotation = Quaternion.Euler(smoothTilt.x, smoothTilt.y, 0);
    }
}
