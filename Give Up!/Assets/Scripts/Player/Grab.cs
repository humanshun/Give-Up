using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Animator animator;
    GameObject grabbedObj;
    public Rigidbody rb;
    public int isLeftorRight;
    public bool alreadyGrabbing = false;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update ()
    {
        if (Input.GetMouseButtonDown(isLeftorRight))
        {
            if (isLeftorRight == 0)
            {
                animator.SetBool("isLeftHandUp", true);
            }
            else if (isLeftorRight == 1)
            {
                animator.SetBool("isRightHandUp", true);
            }

            if (grabbedObj != null && grabbedObj.GetComponent<FixedJoint>() == null) // ここで確認
            {
                FixedJoint fj = grabbedObj.AddComponent<FixedJoint>();
                fj.connectedBody = rb;
                fj.breakForce = 9001;
            }
        }
        else if (Input.GetMouseButtonUp(isLeftorRight))
        {
            if (isLeftorRight == 0)
            {
                animator.SetBool("isLeftHandUp", false);
            }
            else if (isLeftorRight == 1)
            {
                animator.SetBool("isRightHandUp", false);
            }

            if (grabbedObj != null)
            {
                Destroy(grabbedObj.GetComponent<FixedJoint>());
            }

            grabbedObj = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            grabbedObj = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            grabbedObj = null;
        }
    }
}
