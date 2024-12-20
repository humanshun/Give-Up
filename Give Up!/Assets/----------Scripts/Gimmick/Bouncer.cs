using UnityEngine;

public class BounceBall : MonoBehaviour
{
    public float bounceForce = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトの法線方向に対して跳ね返す
        Vector3 bounceDirection = collision.contacts[0].normal;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(-bounceDirection * bounceForce, ForceMode.Impulse);
    }
}
