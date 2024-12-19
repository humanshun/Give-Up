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
    public bool buttonDown;
    private PlayerController playerController;
    private GrabObject grabObjectScript;
    private string defaltTag = "Object";
    private string canMoveObjTag = "CanMoveObj";

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

            if (grabbedObj != null)
            {
                grabbedObj.tag = defaltTag;
            }
            
            // 左手のFixedJointを削除
            if (leftHandJoint != null)
            {
                Destroy(leftHandJoint);
                playerController.grabLeftHand = false;
                grabbedObj = null; // 掴んでいるオブジェクトをクリア
                leftHandJoint = null;
            }

            if(grabObjectScript != null)
            {
                grabObjectScript.DisengageGrab();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (buttonDown == true)
        {
            // 当たったオブジェクトを掴む対象として設定
            grabbedObj = other.gameObject;

            // grabbedObj に GrabObject スクリプトが存在する場合のみ処理を実行
            grabObjectScript = grabbedObj.GetComponent<GrabObject>();
            if (grabObjectScript != null)
            {
                // GrabObject スクリプトの CanMove メソッドを呼び出す
                grabObjectScript.GrabLeft();

                playerController.grabLeftHand = false;

                grabbedObj.tag = canMoveObjTag;
            }
        }
    }
}
