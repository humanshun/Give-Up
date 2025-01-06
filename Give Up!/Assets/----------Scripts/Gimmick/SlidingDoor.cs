using System.Collections;
using UnityEngine;

public enum ScaleDirection
{
    Right,
    Left,
    Up,
    Down
}

public class SlidingDoor : MonoBehaviour
{
    public ScaleDirection direction;
    public Vector3 openScale = new Vector3(1f, 1f, 1f); // 開いたときのスケール
    public Vector3 closedScale = new Vector3(0.1f, 1f, 1f); // 閉じたときのスケール
    public float openDuration = 1f; // 開くのにかかる時間
    public float waitTime = 2f; // 開いた状態を維持する時間
    public float closeDuration = 1f; // 閉じるのにかかる時間

    public float openOffset = 1f; // 開くときの位置のオフセット

    private bool isOpen = false;

    public void ToggleDoor()
    {
        if (isOpen)
        {
            // StartCoroutine(CloseDoor());
        }
        else
        {
            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = GetScaleForDirection(openScale);
        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = GetPositionForDirection(openOffset);

        float elapsedTime = 0f;
        while (elapsedTime < openDuration)
        {
            // スケールを変更
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / openDuration);
            // 位置を変更
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / openDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale; // 最終スケールに設定
        transform.localPosition = endPosition; // 最終位置に設定

        isOpen = true;
        yield return new WaitForSeconds(waitTime);
        // StartCoroutine(CloseDoor());
    }

    private IEnumerator CloseDoor()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = GetScaleForDirection(closedScale);
        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = GetPositionForDirection(-openOffset);

        float elapsedTime = 0f;
        while (elapsedTime < closeDuration)
        {
            // スケールを変更
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / closeDuration);
            // 位置を変更
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / closeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale; // 最終スケールに設定
        transform.localPosition = endPosition; // 最終位置に設定

        isOpen = false;
    }

    private Vector3 GetScaleForDirection(Vector3 scale)
    {
        switch (direction)
        {
            case ScaleDirection.Right:
                return new Vector3(scale.x, scale.y, scale.z); // 右に開く
            case ScaleDirection.Left:
                return new Vector3(-scale.x, scale.y, scale.z); // 左に開く
            case ScaleDirection.Up:
                return new Vector3(scale.x, scale.y, scale.z); // 上に開く
            case ScaleDirection.Down:
                return new Vector3(scale.x, -scale.y, scale.z); // 下に開く
            default:
                return scale; // デフォルト
        }
    }

    private Vector3 GetPositionForDirection(float offset)
    {
        switch (direction)
        {
            case ScaleDirection.Right:
                return transform.localPosition + new Vector3(offset, 0f, 0f); // 右にオフセット
            case ScaleDirection.Left:
                return transform.localPosition + new Vector3(-offset, 0f, 0f); // 左にオフセット
            case ScaleDirection.Up:
                return transform.localPosition + new Vector3(0f, offset, 0f); // 上にオフセット
            case ScaleDirection.Down:
                return transform.localPosition + new Vector3(0f, -offset, 0f); // 下にオフセット
            default:
                return transform.localPosition; // デフォルト
        }
    }
}
