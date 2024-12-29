using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        // 全てのボタンを初期状態（透明度: normalAlpha）に設定
        foreach (var pair in keyButtonPairs)
        {
            if (pair.button == null) continue;
            ResetButtonState(pair.button);
        }
    }

    void Update()
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
                        ResetButtonState(pair.button);
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

    private void ResetButtonState(Button button)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage == null) return;

        // ノーマルカラーに戻す（透明度も設定）
        Color color = normalKeyColor;
        color.a = normalAlpha;
        buttonImage.color = color;
    }
}
