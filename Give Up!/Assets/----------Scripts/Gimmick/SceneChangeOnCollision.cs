using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOnCollision : MonoBehaviour
{
    // トリガー対象のタグ名（インスペクターで設定可能）
    public string targetTag = "Player";
    // 移動先のシーン名
    public string sceneName;

    // オブジェクトに触れたときに呼ばれる
    void OnTriggerEnter(Collider other)
    {
        // 触れたオブジェクトのタグが指定されたものか確認
        if (other.CompareTag(targetTag))
        {
            // シーンを移動
            SceneManager.LoadScene("Clear");
        }
    }
}
