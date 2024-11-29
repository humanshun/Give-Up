using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    private Rigidbody rb;
    private bool aa;
    private bool bb;
    void Start()
    {
        // Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
    }
    public void GrabLeft()
    {
        aa = true;
        CheckCanMove();
    }
    public void GrabRight()
    {
        bb = true;
        CheckCanMove();
    }

    public void DisengageGrab()
    {
        aa = false;
        bb = false;
        CheckCanMove();
    }

    public void CheckCanMove()
    {
        if (aa == true && bb == true)
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
