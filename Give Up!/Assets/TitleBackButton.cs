using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleBackButton : MonoBehaviour
{
    [SerializeField] private Button button;
    void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }
    private void OnClickButton()
    {
        SceneManager.LoadScene("Stage_shindome");
    }
}
