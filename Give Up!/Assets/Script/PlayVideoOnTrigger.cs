using UnityEngine;
using UnityEngine.Video;

public class PlayVideoOnTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer; // 壁のvideoPlayerをここに割り当てる

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            videoPlayer.Play(); // 動画再生
        }
    }
}