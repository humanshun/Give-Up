using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOnTargetFall : MonoBehaviour
{
    // 対象のオブジェクト
    public Transform targetObject;
    // 閾値のY座標（インスペクターで設定）
    public float yThreshold = 215f;
    // 移動先のシーン名
    public string sceneName;

    void Update()
    {
        // 対象のオブジェクトが指定されているか確認
        if (targetObject != null)
        {
            // 対象のオブジェクトのY座標が閾値以下になった場合
            if (targetObject.position.y <= yThreshold)
            {
                // シーンを移動
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
