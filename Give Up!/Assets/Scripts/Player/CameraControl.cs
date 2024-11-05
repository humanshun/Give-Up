using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotatoinSpeed = 1;
    public Transform root;

    float mouseX, mouseY;

    public float stomachOffset;

    public ConfigurableJoint hipJoint, stomachiJoint;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        CamControl();
    }
    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotatoinSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotatoinSpeed;
        mouseY  = Mathf.Clamp(mouseY, -35, 60);

        Quaternion rootRotation = Quaternion.Euler(mouseY, mouseX, 0);

        root.rotation = rootRotation;

        hipJoint.targetRotation = Quaternion.Euler(0, -mouseX, 0);
        stomachiJoint.targetRotation = Quaternion.Euler(-mouseY + stomachOffset, 0, 0);
    }
}
