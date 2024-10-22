using System.Collections;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    private Renderer platformRenderer;
    private Collider platformCollider;

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
        // 5秒後に非表示にする
        yield return new WaitForSeconds(5f);
        platformRenderer.enabled = false;
        platformCollider.enabled = false;

        // 20秒後に再表示する
        yield return new WaitForSeconds(20f);
        platformRenderer.enabled = true;
        platformCollider.enabled = true;
    }
}
