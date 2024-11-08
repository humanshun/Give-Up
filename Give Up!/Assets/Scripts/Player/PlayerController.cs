using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public float strafeSpeed;
    public float jumpForce;
    public Vector3 moveDirection;

    public Rigidbody hips;
    public bool isGrounded;
    private bool canJump = true;

    void Start()
    {
        hips = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isWalk", true);
                animator.SetBool("isRun", true);
                hips.AddForce(hips.transform.forward * speed * 1.5f);
            }
            else
            {
                animator.SetBool("isRun", false);
                animator.SetBool("isWalk", true);
                hips.AddForce(hips.transform.forward * speed);
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isWalk", true);
            hips.AddForce(-hips.transform.right * strafeSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isWalk", true);
            hips.AddForce(-hips.transform.forward * strafeSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isWalk", true);
            hips.AddForce(hips.transform.right * strafeSpeed);
        }
        
        if (Input.GetAxis("Jump") > 0 && isGrounded && canJump)
        {
            if (isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
                canJump = false;
                StartCoroutine(JumpCooldown());
            }
        }
        
    }
    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(0.2f); // 0.2秒待機
        canJump = true; // ジャンプを再度有効化
    }
}
