using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;
    public int isLeftorRight; // 左手か右手かのボタンインデックス（0 = 左手、1 = 右手）
    private GameObject grabbedObj; // 掴んでいるオブジェクト
    private FixedJoint leftHandJoint; // 左手のFixedJoint
    private FixedJoint rightHandJoint; // 右手のFixedJoint

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 左手で掴む操作
        if (isLeftorRight == 0 && Input.GetMouseButton(0))
        {
            animator.SetBool("isLeftHandUp", true);

            // 左手が掴んでいるオブジェクトに FixedJoint がない場合に掴む
            if (grabbedObj != null && leftHandJoint == null)
            {
                leftHandJoint = grabbedObj.AddComponent<FixedJoint>();
                leftHandJoint.connectedBody = rb;
                leftHandJoint.breakForce = 9001;
            }
        }
        else if (isLeftorRight == 0 && Input.GetMouseButtonUp(0))
        {
            animator.SetBool("isLeftHandUp", false);

            // 左手のFixedJointを削除
            if (leftHandJoint != null)
            {
                Destroy(leftHandJoint);
                grabbedObj = null; // 掴んでいるオブジェクトをクリア
                leftHandJoint = null;
            }
        }

        // 右手で掴む操作
        if (isLeftorRight == 1 && Input.GetMouseButton(1))
        {
            animator.SetBool("isRightHandUp", true);

            // 右手が掴んでいるオブジェクトに FixedJoint がない場合に掴む
            if (grabbedObj != null && rightHandJoint == null)
            {
                rightHandJoint = grabbedObj.AddComponent<FixedJoint>();
                rightHandJoint.connectedBody = rb;
                rightHandJoint.breakForce = 9001;
            }
        }
        else if (isLeftorRight == 1 && Input.GetMouseButtonUp(1))
        {
            animator.SetBool("isRightHandUp", false);

            // 右手のFixedJointを削除
            if (rightHandJoint != null)
            {
                Destroy(rightHandJoint);
                grabbedObj = null; // 掴んでいるオブジェクトをクリア
                rightHandJoint = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 当たったオブジェクトを掴む対象として設定
        grabbedObj = other.gameObject;
    }
}
