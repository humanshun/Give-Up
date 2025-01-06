using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleBackButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string sceneName;
    void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }
    private void OnClickButton()
    {
        SceneManager.LoadScene(sceneName);
    }
}
