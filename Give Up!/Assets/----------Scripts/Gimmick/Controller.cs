using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    public float jumpForce = 5f; // ジャンプ力
    public float horizontalJumpMultiplier = 20f; // 水平方向のジャンプ力を増加させるための乗数
    public float verticalJumpForce = 4f; // 上方向のジャンプ力
    public float mouseSensitivity = 100f; // マウス感度
    public int numberOfRays = 36; // レイの数（360度に均等に配置）
    public Color rayColor = Color.red; // レイの色

    private CharacterController controller;
    private Vector3 moveDirection;
    private float gravity = -9.81f;
    private Transform cameraTransform;
    private float xRotation;
    private bool isGrounded;
    public LayerMask groundMask; // 地面を判定するレイヤー
    public float groundCheckRadius = 0.4f; // 球体の半径
    public Transform groundCheck; // 地面チェック用のTransform

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

        // 地面に接しているかの判定
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        // 重力とジャンプ処理
        if (isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = -2f; // 地面に接しているときはy方向の速度をリセット
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            moveDirection.y = Mathf.Sqrt(jumpForce * -2f * gravity); // 通常ジャンプ
        }

        // 重力の影響を受ける
        moveDirection.y += gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        // ジャンプ後、X-Z方向の力を減衰させる
        if (!Input.GetButton("Jump"))
        {
            moveDirection.x = Mathf.Lerp(moveDirection.x, 0, Time.deltaTime * 5f);
            moveDirection.z = Mathf.Lerp(moveDirection.z, 0, Time.deltaTime * 5f);
        }

        // プレイヤーの周り360度にレイを発射する
        ShootRays(move);
    }

    void ShootRays(Vector3 inputDirection)
    {
        // レイの長さ
        float rayLength = 1f;
        Vector3 totalDirection = Vector3.zero; // 当たったレイの方向を合計する変数
        int hitCount = 0; // 当たったレイの数をカウントする変数

        for (int i = 0; i < numberOfRays; i++)
        {
            // 各レイの角度を計算
            float angle = i * (360f / numberOfRays);
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward; // 各レイの方向を計算
            
            RaycastHit hit;
            Vector3 rayOrigin = transform.position; // レイの発射位置
            Debug.DrawRay(rayOrigin, direction * rayLength, rayColor); // レイを描画
            
            if (Physics.Raycast(rayOrigin, direction, out hit, rayLength))
            {
                // Debug.Log($"レイが当たったオブジェクト: {hit.collider.name} (方向: {direction})");
                
                // 当たった方向を合計
                totalDirection += hit.normal; // 壁の法線を合計
                hitCount++; // ヒットカウントを増やす
            }
            else
            {
                // Debug.Log($"方向: {direction} に何もありません。");
            }
        }

        // 当たったレイがあった場合
        if (hitCount > 0)
        {
            // 合計した法線の平均を計算
            Vector3 averageDirection = totalDirection.normalized; // 正規化して平均方向を求める
            
            // ジャンプ処理
            if (Input.GetButtonDown("Jump"))
            {
                // 平均の法線ベクトルの y 成分をゼロにして、X-Z平面に投影
                averageDirection.y = 0; // y成分を0にすることで上方向の成分を排除

                // ジャンプする方向を逆にする
                moveDirection = averageDirection + inputDirection.normalized; // 平均方向と入力方向を合成
                moveDirection = moveDirection.normalized * horizontalJumpMultiplier; // 合成した方向を正規化し、横方向の力を加える
                moveDirection.y = verticalJumpForce; // 上方向のジャンプ力を設定
            }
        }
    }

    // 地面チェックの範囲をエディタで表示する
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red; // 球体の色を設定（例: 赤）
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    } 
    public void Knockback(Vector3 direction, float force)
{
    moveDirection += direction * force; // ノックバック方向と強さを合成
    moveDirection.y = Mathf.Max(moveDirection.y, force * 0.5f); // Y軸方向にも持ち上げるように設定
}
}