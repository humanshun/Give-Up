using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LimbCollision : MonoBehaviour
{
    public PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerController.isGrounded = true;
    }
}
