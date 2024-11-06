using UnityEngine;

public class HammerSwing : MonoBehaviour
{
    public GameObject hammer;           // ハンマー本体
    public float swingAngle = 90f;      // 角度の振り幅（±90度）
    public float swingDuration = 3f;    // 一振りの時間

    private float timer = 0f;
    private float previousAngle = 0f;

    public bool IsSwingingRight { get; private set; } // 右に振れているかどうかのプロパティ

    void Update()
    {
        // 時間経過に応じて角度を計算
        timer += Time.deltaTime;
        float angle = Mathf.Sin(timer / swingDuration * Mathf.PI * 2) * swingAngle;

        // ハンマーの回転を設定
        transform.localRotation = Quaternion.Euler(angle, 0f, 0f);

        // どちらの方向に動いているかを確認し、進行方向を設定
        IsSwingingRight = angle > previousAngle;

        // 前回の角度を更新
        previousAngle = angle;
    }
}
