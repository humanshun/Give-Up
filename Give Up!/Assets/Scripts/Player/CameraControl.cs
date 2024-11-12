using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotationSpeed = 3;
    public Transform root;

    public float stomachOffset;
    public ConfigurableJoint hipJoint, stomachiJoint;

    private float mouseX, mouseY;
    private PlayerController playerController; // FindObjectOfTypeで自動取得

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
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        Quaternion rootRotation = Quaternion.Euler(mouseY, mouseX, 0);
        root.rotation = rootRotation;

        hipJoint.targetRotation = Quaternion.Euler(0, -mouseX, 0);
        stomachiJoint.targetRotation = Quaternion.Euler(-mouseY + stomachOffset, 0, 0);

        // 角度が60度になったらPlayerControllerのjummpp()を呼び出す
        if (mouseY >= 30)
        {
            if (playerController != null)
            {
                playerController.cameraY = true;
            }
            else
            {
                playerController.cameraY = false;
            }
        }
    }
}
