using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabLeftHand : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;
    public Rigidbody hipRb;
    private GameObject grabbedObj; // 掴んでいるオブジェクト
    private FixedJoint leftHandJoint; // 左手のFixedJoint
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
        // 左手で掴む操作
        if (Input.GetMouseButton(0))
        {
            buttonDown = true;
            animator.SetBool("isLeftHandUp", true);

            // 左手が掴んでいるオブジェクトに FixedJoint がない場合に掴む
            if (grabbedObj != null && leftHandJoint == null)
            {
                playerController.grabLeftHand = true;
                leftHandJoint = grabbedObj.AddComponent<FixedJoint>();
                leftHandJoint.connectedBody = rb;
                leftHandJoint.breakForce = 9001;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            buttonDown = false;
            animator.SetBool("isLeftHandUp", false);

            // 左手のFixedJointを削除
            if (leftHandJoint != null)
            {
                playerController.grabLeftHand = false;
                Destroy(leftHandJoint);
                grabbedObj = null; // 掴んでいるオブジェクトをクリア
                leftHandJoint = null;
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
