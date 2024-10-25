using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    public GameObject headObject;
    public float antiGravityForce = 9.8f; // 反重力の強さを調整

    private Rigidbody rb;

    void Start()
    {
        // Rigidbodyコンポーネントがアタッチされていなければ追加
        if (headObject.GetComponent<Rigidbody>() == null)
        {
            rb = headObject.AddComponent<Rigidbody>();
        }
        else
        {
            rb = headObject.GetComponent<Rigidbody>();
        }
        
        // 重力を無効化
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        // 上向きの力を追加して反重力効果を実現
        rb.AddForce(Vector3.up * antiGravityForce, ForceMode.Acceleration);
    }
}
