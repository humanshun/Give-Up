using UnityEngine;
using UnityEngine.Video;

public class CollisionVideoPlayback : MonoBehaviour
{
    public VideoPlayer videoPlayer; // 壁に設置したVideoPlayer

    private void Start()
    {
        videoPlayer.Stop(); // ゲーム開始時に動画を停止
    }

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーのみ反応するようにタグで判定
        if (collision.gameObject.CompareTag("Player"))
        {
            videoPlayer.Play(); // 衝突時に動画を再生
        }
    }
}
