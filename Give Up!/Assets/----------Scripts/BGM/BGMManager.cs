using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance { get; private set; }

    [SerializeField] private AudioClip homeBGM;   // ホームシーン用BGM
    [SerializeField] private AudioClip gameBGM;   // 全ゲームシーン共通のBGM

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // シングルトンの重複を防ぐ
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // シーンをまたいでも破棄されないように設定

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;

        // シーンロードイベントを登録
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // イベント解除
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // シーン名に応じてBGMを切り替え
        PlayBGM(scene.name);
    }

    public void PlayBGM(string sceneName)
    {
        AudioClip clipToPlay = null;

        // "HomeScene"かどうかで分岐
        if (sceneName == "HomeScene")
        {
            clipToPlay = homeBGM;
        }
        else // その他のシーンはすべてゲームシーンとみなす
        {
            clipToPlay = gameBGM;
        }

        // 別のBGMが再生中の場合のみ切り替える
        if (clipToPlay != null && audioSource.clip != clipToPlay)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
