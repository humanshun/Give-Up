using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CopyMotion : MonoBehaviour
{
    public float XR;
    public float YR;
    public float ZR;
    public Transform targetLimb;
    public bool mirror;
    ConfigurableJoint cj;

    void Start()
    {
        cj = GetComponent<ConfigurableJoint>();
    }

    void Update()
    {
        // 上下反転用の回転。X軸で180度回転させる例
        Quaternion upsideDown = Quaternion.Euler(XR, YR, ZR);

        if (mirror)
        {
            // ターゲット回転に上下反転を組み合わせ
            cj.targetRotation = targetLimb.rotation * upsideDown;
        }
        else
        {
            // ターゲット回転の逆数に上下反転を組み合わせ
            cj.targetRotation = Quaternion.Inverse(targetLimb.rotation * upsideDown);
        }
    }
}
