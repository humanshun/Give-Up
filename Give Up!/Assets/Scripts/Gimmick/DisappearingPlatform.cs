using System.Collections;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    private Renderer platformRenderer;
    private Collider platformCollider;

    // 待機時間を調整できるようにpublic float変数を追加
    public float timeToDisappear = 5f;
    public float timeToReappear = 20f;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DisappearAndReappear());
        }
    }

    private IEnumerator DisappearAndReappear()
    {
        // timeToDisappearの秒数後に非表示にする
        yield return new WaitForSeconds(timeToDisappear);
        platformRenderer.enabled = false;
        platformCollider.enabled = false;

        // timeToReappearの秒数後に再表示する
        yield return new WaitForSeconds(timeToReappear);
        platformRenderer.enabled = true;
        platformCollider.enabled = true;
    }
}
