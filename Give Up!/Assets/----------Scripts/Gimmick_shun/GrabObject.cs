using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    private Rigidbody rb;
    private bool left;
    private bool right;
    void Start()
    {
        // Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
    }
    public void GrabLeft()
    {
        left = true;
        if (right)
        {
            CheckCanMove();
        }
    }
    public void GrabRight()
    {
        right = true;
        if (left)
        {
            CheckCanMove();
        }
    }

    public void DisengageGrab()
    {
        rb = GetComponent<Rigidbody>();
        left = false;
        right = false;
        rb.useGravity = false;
        rb.isKinematic = true;
        CheckCanMove();
    }

    public void CheckCanMove()
    {
        MeshCollider mesh = GetComponent<MeshCollider>();
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (left == true && right == true && PlayerController.editMode)
        {
            if (mesh != null)
            {
                mesh.convex = true;
            }
            if (PlayerController.editMode == true)
            {
                gameObject.layer = 9;
            }
            
            rb.useGravity = true;
            rb.isKinematic = false;
        }
        else
        {
            if (mesh != null)
            {
                mesh.convex = false;
            }
            if (PlayerController.editMode == true)
            {
                gameObject.layer = 9;
            }
            
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }
}
