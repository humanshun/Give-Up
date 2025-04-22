using UnityEngine;
using TMPro;

public class SensitivityChanger : MonoBehaviour
{
    [Header("true なら感度を上げる、false なら感度を下げる")]
    public bool isIncrease;

    [Header("感度の増減量")]
    public float changeAmount = 1.0f;

    [Header("プレイヤー用のタグ名")]
    public string playerTag = "Player";

    // Inspector で CameraControl をセットしておく
    public CameraControl cameraControl;

    // Inspector で TextMeshPro (TMP_Text or TextMeshProUGUI) をセットしておく
    [Header("現在の感度を表示する TMP")]
    public TMP_Text sensitivityText;

    void Start()
    {
        sensitivityText.text = CameraControl.savedSpeed.ToString("F1");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーに触れたかどうかをタグで判定
        if (collision.gameObject.CompareTag(playerTag))
        {
            // すでに Inspector で関連付けてあるため GetComponent系は不要
            if (cameraControl != null)
            {
                // 感度を上げる or 下げる
                if (isIncrease)
                {
                    CameraControl.savedSpeed += changeAmount;
                }
                else
                {
                    CameraControl.savedSpeed -= changeAmount;
                }

                // TMP に現在の感度を反映
                if (sensitivityText != null)
                {
                    sensitivityText.text = CameraControl.savedSpeed.ToString("F1");
                }
            }
            else
            {
                Debug.LogWarning("[SensitivityChanger] CameraControl がアタッチされていません！");
            }
        }
    }
}
