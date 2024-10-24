using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isOpen = false; // 扉が開いているかどうか
    public float openSpeed = 2f; // 扉が開く速度
    public Vector3 openPosition; // 扉の開いた位置
    private Vector3 closedPosition; // 扉の閉じた位置

    void Start()
    {
        closedPosition = transform.position; // 扉の初期位置を保存
    }

    void Update()
    {
        if (isOpen)
        {
            // 扉を開く処理
            transform.position = Vector3.Lerp(transform.position, openPosition, Time.deltaTime * openSpeed);
        }
        else
        {
            // 扉を閉じる処理
            transform.position = Vector3.Lerp(transform.position, closedPosition, Time.deltaTime * openSpeed);
        }
    }

    // 扉を開けたり閉めたりするメソッド
    public void ToggleDoor()
    {
        isOpen = !isOpen; // 扉の状態を反転
    }
}
