using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public float strafeSpeed;
    public float jumpForce;
    public Vector3 moveDirection;

    public Rigidbody hips;

    public bool cameraY;
    public bool grabLeftHand;
    public bool grabRightHand;
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
        
        ClimbUp();
    }
    public void ClimbUp()
    {
        if (cameraY && grabLeftHand && grabRightHand)
        {
            hips.AddForce(0, 300f, 0);
        }
    }
    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(0.7f); // 0.2秒待機
        canJump = true; // ジャンプを再度有効化
    }

    private void OnTriggerEnter(Collider other)
    {
        // 衝突したオブジェクトにRigidBodyをつける
        if (other.gameObject.CompareTag("Object") && GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = other.gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // 離れたオブジェクトが"Object"タグを持っている場合のみRigidbodyを削除
        if (other.gameObject.CompareTag("Object"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }
        }
    }
}
