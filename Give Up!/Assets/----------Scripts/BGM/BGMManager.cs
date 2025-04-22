using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance { get; private set; }

    [SerializeField] private AudioClip homeBGM;   // ホームシーン用BGM
    [SerializeField] private AudioClip gameBGM;   // ゲームシーン用BGM

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGM(scene.name);
    }

    public void PlayBGM(string sceneName)
    {
        AudioClip clipToPlay = null;

        if (sceneName == "Title")
        {
            clipToPlay = homeBGM;
        }
        else
        {
            clipToPlay = gameBGM;
        }

        if (clipToPlay != null && audioSource.clip != clipToPlay)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
    }

    /// <summary>
    /// 音量を設定する (0～1)
    /// </summary>
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    /// <summary>
    /// 現在の音量を取得する
    /// </summary>
    public float GetVolume()
    {
        return audioSource.volume;
    }
}
