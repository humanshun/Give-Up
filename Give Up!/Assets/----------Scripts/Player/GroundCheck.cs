using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Untagged") || other.gameObject.CompareTag("Object"))
        {
            playerController.isGrounded = true;
        }
    }
}
