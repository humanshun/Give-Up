using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabLeftHand : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;
    public Rigidbody hipRb;

    private GameObject grabbedObj;
    private FixedJoint leftHandJoint;
    public bool buttonDown;
    private PlayerController playerController;
    private GrabObject grabObjectScript;
    private string defaltTag = "Object";
    private string canMoveObjTag = "CanMoveObj";

    [SerializeField] private CapsuleCollider[] leftHandColliders; // 左手自身のコライダー
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            buttonDown = true;
            animator.SetBool("isLeftHandUp", true);

            if (grabbedObj != null && leftHandJoint == null)
            {
                playerController.grabLeftHand = true;
                leftHandJoint = grabbedObj.AddComponent<FixedJoint>();
                leftHandJoint.connectedBody = rb;
                leftHandJoint.breakForce = 9001;

                foreach (var a in leftHandColliders)
                {
                    a.enabled = false; // 掴んだら左手のコライダーをオフ
                }
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

            if (leftHandJoint != null)
            {
                Destroy(leftHandJoint);
                playerController.grabLeftHand = false;
                grabbedObj = null;
                leftHandJoint = null;

                foreach (var a in leftHandColliders)
                {
                    a.enabled = true; // 掴んだら左手のコライダーをオン
                }
            }

            if (grabObjectScript != null)
            {
                grabObjectScript.DisengageGrab();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (buttonDown)
        {
            grabbedObj = other.gameObject;
            grabObjectScript = grabbedObj.GetComponent<GrabObject>();

            if (grabObjectScript != null)
            {
                grabObjectScript.GrabLeft();
                grabbedObj.tag = canMoveObjTag;
            }
        }
    }
}

