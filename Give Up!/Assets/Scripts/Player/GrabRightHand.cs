using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabRightHand : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;
    public Rigidbody hipRb;
    private GameObject grabbedObj; // 掴んでいるオブジェクト
    private FixedJoint rightHandJoint; // 右手のFixedJoint
    private bool buttonDown;
    private PlayerController playerController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // PlayerControllerの参照をFindObjectOfTypeで取得
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        // 右手で掴む操作
        if (Input.GetMouseButton(1))
        {
            buttonDown = true;
            animator.SetBool("isRightHandUp", true);

            // 右手が掴んでいるオブジェクトに FixedJoint がない場合に掴む
            if (grabbedObj != null && rightHandJoint == null)
            {
                playerController.grabRightHand = true;
                rightHandJoint = grabbedObj.AddComponent<FixedJoint>();
                rightHandJoint.connectedBody = rb;
                rightHandJoint.breakForce = 9001;
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            buttonDown = false;
            animator.SetBool("isRightHandUp", false);

            // 右手のFixedJointを削除
            if (rightHandJoint != null)
            {
                playerController.grabRightHand = false;
                Destroy(rightHandJoint);
                grabbedObj = null; // 掴んでいるオブジェクトをクリア
                rightHandJoint = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (buttonDown == true)
        {
            // 当たったオブジェクトを掴む対象として設定
            grabbedObj = other.gameObject;
        }
    }
}
