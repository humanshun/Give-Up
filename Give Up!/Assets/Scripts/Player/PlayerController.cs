using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private float strafeSpeed;
    [SerializeField] private float maxSpeed = 3.0f;
    public float jumpForce;

    public Rigidbody hips;

    public bool cameraY;
    public bool grabLeftHand;
    public bool grabRightHand;
    public bool isGrounded;
    private bool canJump = true;
    public static bool editMode = false;

    void Start()
    {
        hips = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // 入力方向を初期化
        Vector3 inputDirection = Vector3.zero;

        // 前後左右の入力を検出
        if (Input.GetKey(KeyCode.W))
        {
            inputDirection += hips.transform.forward; // 前進
            UpdateAnimation(isRunning: Input.GetKey(KeyCode.LeftShift), isWalking: true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputDirection -= hips.transform.forward; // 後退
            UpdateAnimation(isRunning: false, isWalking: true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputDirection -= hips.transform.right; // 左移動
            UpdateAnimation(isRunning: false, isWalking: true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputDirection += hips.transform.right; // 右移動
            UpdateAnimation(isRunning: false, isWalking: true);
        }

        // 入力がなければアニメーションを止める
        if (inputDirection == Vector3.zero)
        {
            UpdateAnimation(isRunning: false, isWalking: false);
        }

        // 入力方向を正規化
        inputDirection = inputDirection.normalized;

        // 移動速度制限を確認して力を加える
        if (CanApplyForce(hips.velocity, inputDirection, maxSpeed))
        {
            float appliedSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * 1.5f : speed;
            hips.AddForce(inputDirection * appliedSpeed);
        }

        // ジャンプ処理
        if (Input.GetAxis("Jump") > 0 && isGrounded && canJump)
        {
            hips.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
            canJump = false;
            StartCoroutine(JumpCooldown());
        }
        ClimbUp();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!editMode)
            {
                editMode = true;
            }
            else
            {
                editMode = false;
            }
        }
    }

    // アニメーションの更新
    private void UpdateAnimation(bool isRunning, bool isWalking)
    {
        animator.SetBool("isRun", isRunning);
        animator.SetBool("isWalk", isWalking);
    }

    // 指定方向への速度が一定以上出ている場合、力を加えない
    private bool CanApplyForce(Vector3 currentVelocity, Vector3 inputDirection, float maxSpeed)
    {
        float velocityInDirection = Vector3.Dot(currentVelocity, inputDirection);
        return velocityInDirection < maxSpeed;
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
        // Debug.Log("あたった");
        // 衝突したオブジェクトにRigidBodyをつける
        if (other.gameObject.CompareTag("Object") && other.gameObject.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = other.gameObject.AddComponent<Rigidbody>();
            
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.mass = 0.1f;
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
