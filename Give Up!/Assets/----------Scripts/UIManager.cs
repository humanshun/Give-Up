using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [System.Serializable]
    public class KeyButtonPair
    {
        public KeyCode key;      // 対応するキー
        public Button button;    // 対応するボタン
    }

    public KeyButtonPair[] keyButtonPairs; // キーとボタンのペアをInspectorで設定
    public float normalAlpha = 0.4f;       // ノーマルカラーの透明度 (40%)
    public float pressedAlpha = 1.0f;
    public Color normalKeyColor = Color.white; // 通常のボタンの色
    public Color specialKeyColor = Color.yellow; // 特定のキー（例: E）用の色

    private bool isEKeyActive = false; // Eキーの状態を保持

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button homeButton;

    [SerializeField] private CameraControl cameraControl;
    [SerializeField] private GameObject settingMenuUI;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_InputField sensitivityField;
    [SerializeField] private Button settingsBackButton;

    private Button currentButton;
    private bool isPaused = false;

    private const float MinRotationSpeed = 0.1f;
    private const float MaxRotationSpeed = 10.0f;

    

    void Start()
    {
        // cameraControlが設定されていない場合、自動取得を試みる
        if (cameraControl == null)
        {
            cameraControl = FindObjectOfType<CameraControl>();
            if (cameraControl == null)
            {
                Debug.LogError("CameraControlが見つかりません。インスペクターで設定してください。");
            }
        }
        // 全てのボタンを初期状態（透明度: normalAlpha）に設定
        foreach (var pair in keyButtonPairs)
        {
            if (pair.button == null) continue;
            ResetButtonColorState(pair.button);
        }
        
        resetButton.onClick.AddListener(ResetButtonState);
        settingButton.onClick.AddListener(SettingButtonState);
        homeButton.onClick.AddListener(HomeButtonState);
        settingsBackButton.onClick.AddListener(SettingsBackButtonState);

        // BGMManager があれば音量をスライダーに合わせる
        if (BGMManager.Instance != null)
        {
            volumeSlider.value = BGMManager.Instance.GetVolume();
        }
        else
        {
            volumeSlider.value = 0.5f; // 念のため
        }

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        sensitivityField.onValueChanged.AddListener(OnSensitivityChanged);

        OnVolumeChanged(volumeSlider.value);

        // 初期値を反映
        sensitivityField.text = CameraControl.savedSpeed.ToString("F1");
        OnSensitivityChanged(sensitivityField.text);

        pauseMenuUI.gameObject.SetActive(false);
        settingMenuUI.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isPaused)
        {
            foreach (var pair in keyButtonPairs)
            {
                if (pair.button == null) continue;

                if (pair.key == KeyCode.E)
                {
                    // Eキーが押された場合
                    if (Input.GetKeyDown(pair.key))
                    {
                        isEKeyActive = !isEKeyActive; // 状態を切り替える
                        if (isEKeyActive)
                        {
                            SetButtonSpecialState(pair.button, specialKeyColor);
                        }
                        else
                        {
                            ResetButtonColorState(pair.button);
                        }
                    }
                }
                else
                {
                    // 通常のキーの処理
                    if (Input.GetKey(pair.key))
                    {
                        SetButtonState(pair.button, true);
                    }
                    else
                    {
                        SetButtonState(pair.button, false);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    // private void HandleArrowNavigation()
    // {
    //     // 矢印キーでボタンを切り替える
    //     if (Input.GetKeyDown(KeyCode.DownArrow))
    //     {
    //         NavigateToButton(currentButton.navigation.selectOnDown);
    //     }
    //     else if (Input.GetKeyDown(KeyCode.UpArrow))
    //     {
    //         NavigateToButton(currentButton.navigation.selectOnUp);
    //     }
    // }

    // private void NavigateToButton(Selectable nextSelectable)
    // {
    //     if (nextSelectable != null && nextSelectable is Button nextButton)
    //     {
    //         currentButton = nextButton;
    //         SelectButton(currentButton);
    //     }
    // }

    private void SelectButton(Button button)
    {
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
    private void SelectSlider(Slider slider)
    {
        EventSystem.current.SetSelectedGameObject(slider.gameObject);
    }

    private void SetButtonState(Button button, bool isPressed)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage == null) return;

        Color color;

        if (isPressed)
        {
            // 押された状態の色（透明度を最大に）
            color = buttonImage.color;
            color.a = pressedAlpha;
        }
        else
        {
            // ノーマル状態の色（透明度と色をリセット）
            color = normalKeyColor;
            color.a = normalAlpha;
        }

        buttonImage.color = color;
    }

    private void SetButtonSpecialState(Button button, Color specialColor)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage == null) return;

        // 特定のキーが押された場合の色を設定
        buttonImage.color = specialColor;
    }

    private void ResetButtonColorState(Button button)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage == null) return;

        // ノーマルカラーに戻す（透明度も設定）
        Color color = normalKeyColor;
        color.a = normalAlpha;
        buttonImage.color = color;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // ポーズ時に最初のボタンを選択
        SelectButton(resetButton);

        Cursor.lockState = CursorLockMode.None;
    }

    private void ResetButtonState()
    {
        //現在のステージ情報を取得して、ステージを再読み込みする予定
        // Debug.Log("ステージをリセットします");

        Time.timeScale = 1.0f;

        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
        
    }
    private void SettingButtonState()
    {
        //セッティング
        Debug.Log("セッティング");

        pauseMenuUI.SetActive(false);
        settingMenuUI.SetActive(true);

        SelectSlider(volumeSlider);
    }
    private void HomeButtonState()
    {
        //titleにシーン遷移
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Title");
    }

    private void OnVolumeChanged(float value)
    {
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.SetVolume(value);
        }
    }
    private void OnSensitivityChanged(string newValue)
    {
        if (float.TryParse(newValue, out float parsedValue))
        {
            CameraControl.savedSpeed = Mathf.Clamp(parsedValue, MinRotationSpeed, MaxRotationSpeed);
        }
        else
        {
            sensitivityField.text = CameraControl.savedSpeed.ToString("F1");
        }
    }
    private void SettingsBackButtonState()
    {
        settingMenuUI.gameObject.SetActive(false);
        pauseMenuUI.gameObject.SetActive(true);
    }
}
