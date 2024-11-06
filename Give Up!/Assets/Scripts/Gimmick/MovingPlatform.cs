using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float speed = 1f;
    private bool movingToEnd = true;

    private void Start()
    {
        startPosition = transform.position; // 初期位置を保存
    }

    private void Update()
    {
        // 床の移動
        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            if (transform.position == endPosition)
            {
                movingToEnd = false; // 逆方向へ
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            if (transform.position == startPosition)
            {
                movingToEnd = true; // 元の方向へ
            }
        }
    }
}
