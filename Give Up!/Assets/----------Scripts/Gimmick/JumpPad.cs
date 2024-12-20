using System.Collections;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float boostedJumpForce = 10f; // ジャンプパッドに乗っているときのジャンプ力
    private bool canJump = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                if (canJump)
                {
                    // 上方向の力を加える
                    playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, boostedJumpForce, playerRigidbody.velocity.z);
                }

                StartCoroutine(Jump());
            }
        }
    }
    IEnumerator Jump()
    {
        canJump = false;

        yield return new WaitForSeconds(1f);

        canJump = true;
    }
}
