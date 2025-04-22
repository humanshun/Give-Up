using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    [Header("true なら音量を上げる、false なら音量を下げる")]
    public bool isIncrease = true;

    [Header("音量の増減量 (0～1 の範囲が望ましい)")]
    public float changeAmount = 0.1f;

    [Header("プレイヤーに設定されているタグ")]
    public string playerTag = "Player";

    [Header("音量操作に使う UI スライダー")]
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        // ゲーム開始時に、BGMManager の現在音量をスライダーに反映
        if (volumeSlider != null && BGMManager.Instance != null)
        {
            volumeSlider.value = BGMManager.Instance.GetVolume() / 2;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーと衝突したかどうかをタグで判定
        if (collision.gameObject.CompareTag(playerTag))
        {
            if (BGMManager.Instance != null)
            {
                // 現在の音量を取得
                float currentVolume = BGMManager.Instance.GetVolume();

                // 音量を上げる or 下げる
                if (isIncrease)
                {
                    currentVolume += changeAmount;
                }
                else
                {
                    currentVolume -= changeAmount;
                }

                // 音量を 0～1 の間にクランプ
                currentVolume = Mathf.Clamp(currentVolume, 0f, 1f);

                // 実際に音量を適用
                BGMManager.Instance.SetVolume(currentVolume);

                // スライダーにも反映
                if (volumeSlider != null)
                {
                    volumeSlider.value = currentVolume;
                }
            }
            else
            {
                Debug.LogWarning("[VolumeChanger] BGMManager.Instance が見つかりません。");
            }
        }
    }
}
