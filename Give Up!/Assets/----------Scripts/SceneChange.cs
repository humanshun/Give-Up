using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneChange : MonoBehaviour
{
    // インスペクター上でシーンを指定するためのフィールド
    [SerializeField] private SceneAsset sceneAsset;

    // シーンの名前を格納する変数（エディタ上で設定）
    private string sceneName;

    private void OnValidate()
    {
#if UNITY_EDITOR
        // インスペクターでシーンを指定したらシーン名を取得
        if (sceneAsset != null)
        {
            sceneName = sceneAsset.name;
        }
#endif
    }

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("シーンが指定されていません！");
        }
    }
}
