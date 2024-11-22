using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    private Rigidbody rb;
    private GrabLeftHand grabLeftHand;
    private GrabRightHand grabRightHand;
    private FixedJoint fixedJoint;
    void Start()
    {
        // Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {

            GrabLeftHand grabLeftHand = collision.gameObject.GetComponent<GrabLeftHand>();
            GrabRightHand grabRightHand = collision.gameObject.GetComponent<GrabRightHand>();

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
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (fixedJoint != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }
}
