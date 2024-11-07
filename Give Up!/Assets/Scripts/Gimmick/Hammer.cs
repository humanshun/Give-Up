using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float knockbackForce = 10f; // ノックバックの強さ
    private HammerSwing hammerSwing;

    private void Start()
    {
        // 親オブジェクトから HammerSwing コンポーネントを取得
        hammerSwing = GetComponentInParent<HammerSwing>();

        if (hammerSwing == null)
        {
            Debug.LogError("親オブジェクトに HammerSwing コンポーネントがありません。");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hammerSwing == null) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                // ハンマーの進行方向に基づくノックバック方向を計算
                Vector3 knockbackDirection;

                // ハンマーが右に振れているなら右方向に、左に振れているなら左方向にノックバックを加える
                if (hammerSwing.IsSwingingRight)
                {
                    knockbackDirection = -transform.forward; // 右に振れている場合、右方向にノックバック
                }
                else
                {
                    knockbackDirection = transform.forward; // 左に振れている場合、左方向にノックバック
                }

                // ノックバックを適用
                playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            }
        }
    }
}
