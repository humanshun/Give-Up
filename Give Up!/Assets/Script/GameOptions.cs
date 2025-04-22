using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour
{
    public GameObject controlsPanel; // 操作説明用のパネル
    public Slider mouseSensitivitySlider; // マウス感度調整用スライダー
    // public PlayerController playerController; // マウス操作を制御するスクリプト

    void Start()
    {
        // 現在の感度をスライダーに反映
        // mouseSensitivitySlider.value = playerController.mouseSensitivity;
    }

    // 操作説明を表示
    public void ShowControls()
    {
        controlsPanel.SetActive(true);
    }

    // 操作説明を閉じる
    public void HideControls()
    {
        controlsPanel.SetActive(false);
    }

    public void RestartFromCheckpoint()
    {
        // シーンの再読み込みでチェックポイントに戻る
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        // playerController.mouseSensitivity = sensitivity;
    }

    public void ReturnToTitle()
    {
        // タイトルシーンに移動
        SceneManager.LoadScene("TitleScene");
    }
}
