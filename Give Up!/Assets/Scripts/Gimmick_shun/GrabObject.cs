using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    private Rigidbody rb;
    private FixedJoint fixedJoint;
    void Start()
    {
        // Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        // 毎フレームでのチェックも可能
        if (fixedJoint == null)
        {
            fixedJoint = GetComponent<FixedJoint>();
        }

        if (fixedJoint != null)
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
