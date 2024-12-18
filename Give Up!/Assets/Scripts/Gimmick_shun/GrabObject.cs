using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    private Rigidbody rb;
    private bool left;
    private bool right;
    // void Start()
    // {
    //     // Rigidbodyコンポーネントを取得
    //     rb = GetComponent<Rigidbody>();
    // }
    public void GrabLeft()
    {
        left = true;
        CheckCanMove();
    }
    public void GrabRight()
    {
        right = true;
        CheckCanMove();
    }

    public void DisengageGrab()
    {
        left = false;
        right = false;
        CheckCanMove();
    }

    public void CheckCanMove()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (left == true && right == true && PlayerController.editMode)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
        else
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }
}
