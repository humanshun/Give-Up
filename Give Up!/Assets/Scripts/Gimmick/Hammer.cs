using UnityEngine;
public class Hammer : MonoBehaviour
{
    public float knockbackForce = 10f; // ノックバックの強さ

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController controllerScript = other.GetComponent<PlayerController>();
            if (controllerScript != null)
            {
                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                controllerScript.Knockback(knockbackDirection, knockbackForce);
            }
        }
    }
}
