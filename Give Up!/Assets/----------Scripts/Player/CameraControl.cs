using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotationSpeed = 3;
    public Transform root;
    public Animator animator;

    public float stomachOffset;
    public ConfigurableJoint hipJoint, stomachiJoint;

    private float mouseX, mouseY;
    private PlayerController playerController; // FindObjectOfTypeで自動取得
    private bool hasReachedThreshold = false; // 30度を超えたかどうかのフラグ
    public bool grabLeftHand = false;
    public bool grabRightHand = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // PlayerControllerの参照をFindObjectOfTypeで取得
        playerController = FindObjectOfType<PlayerController>();
    }

    void FixedUpdate()
    {
        CamControl();
    }

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -90, 90);

        Quaternion rootRotation = Quaternion.Euler(mouseY, mouseX, 0);
        root.rotation = rootRotation;

        hipJoint.targetRotation = Quaternion.Euler(0, -mouseX, 0);
        stomachiJoint.targetRotation = Quaternion.Euler(-mouseY + stomachOffset, 0, 0);

        // 角度が30度以上になったら一度だけPlayerControllerのcameraYをtrueにする
        if (mouseY >= 30)
        {
            if (!hasReachedThreshold && playerController != null)
            {
                playerController.cameraY = true;
                hasReachedThreshold = true;
            }
        }
        else
        {
            // 30度未満になった場合はcameraYをfalseにし、フラグをリセット
            if (hasReachedThreshold && playerController != null)
            {
                playerController.cameraY = false;
                hasReachedThreshold = false;
            }
        }
    }
}
