using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    public float jumpForce = 5f; // ジャンプ力
    public float wallJumpForce = 5f; // 壁ジャンプ力
    public float mouseSensitivity = 100f; // マウス感度

    private CharacterController controller;
    private Vector3 moveDirection;
    private float gravity = -9.81f;
    private Transform cameraTransform;
    private float xRotation;
    private bool isGrounded;
    private bool isWalled;
    public LayerMask groundMask; // 地面を判定するレイヤー
    public LayerMask wallMask; // 壁を判定するレイヤー
    public float groundCheckRadius = 0.4f; // 球体の半径
    public float wallCheckRadius = 0.4f; // 球体の半径
    public Transform groundCheck; // 地面チェック用のTransform
    public Transform wallCheck; // 壁チェック用のTransform

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // カメラの回転処理
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // WASDキーによる移動処理
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // 地面と壁に接しているかの判定
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        isWalled = Physics.CheckSphere(wallCheck.position, wallCheckRadius, wallMask);

        // 重力とジャンプ処理
        if (isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = -2f; // 地面に接しているときはy方向の速度をリセット
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                moveDirection.y = Mathf.Sqrt(jumpForce * -2f * gravity); // 通常ジャンプ
            }
            else if (isWalled)
            {
                // 壁ジャンプ処理
                Vector3 wallNormal = (transform.position - wallCheck.position).normalized;
                
                // 壁の法線方向に対して反発力を加える
                moveDirection.x = wallNormal.x * wallJumpForce; // 壁の反対方向
                moveDirection.z = wallNormal.z * wallJumpForce; // 壁の反対方向
                
                // 上昇する力を少し加える
                moveDirection.y = Mathf.Sqrt(jumpForce * -2f * gravity) * 0.5f; // ジャンプのy方向の速度を設定（0.5fで減衰）
            }
        }

        // 重力の影響を受ける
        moveDirection.y += gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        // ジャンプ後、X-Z方向の力を減衰させる
        if (!Input.GetButton("Jump") && !isWalled)
        {
            moveDirection.x = Mathf.Lerp(moveDirection.x, 0, Time.deltaTime * 5f);
            moveDirection.z = Mathf.Lerp(moveDirection.z, 0, Time.deltaTime * 5f);
        }
    }

    // 地面と壁チェックの範囲をエディタで表示する
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red; // 球体の色を設定（例: 赤）
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
        if (wallCheck != null)
        {
            Gizmos.color = Color.blue; // 球体の色を設定（例: 青）
            Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
        }
    }
}
