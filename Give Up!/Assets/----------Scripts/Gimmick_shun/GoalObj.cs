using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalObj : MonoBehaviour
{
    [SerializeField] private string NextStageName;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("finish");
        if (other.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene(NextStageName);
        }
    }
}
