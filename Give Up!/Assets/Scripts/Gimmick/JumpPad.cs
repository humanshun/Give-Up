using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float boostedJumpForce = 10f; // ジャンプパッドに乗っているときのジャンプ力

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.jumpForce = boostedJumpForce; // ジャンプ力を上げる
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.jumpForce = 5f; // 通常のジャンプ力に戻す（5はデフォルト値）
            }
        }
    }
}
