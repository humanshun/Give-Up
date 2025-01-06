using UnityEngine;
using System.Collections; // IEnumeratorを使用するための名前空間
using System.Collections.Generic;

public class Switch : MonoBehaviour
{
    public List<SlidingDoor> slidingDoors; // SlidingDoorのリスト
    private bool isCooldown = false; // クールダウン中かどうかのフラグ
    public float cooldownTime = 5f; // クールダウンの時間（秒）

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCooldown) // プレイヤーがスイッチに触れてかつクールダウン中でない場合
        {
            foreach (SlidingDoor slidingDoor in slidingDoors) // 各スライディングドアに対して
            {
                slidingDoor.ToggleDoor(); // ドアを開閉
            }
            StartCoroutine(Cooldown()); // クールダウンを開始
        }
    }

    private IEnumerator Cooldown()
    {
        isCooldown = true; // クールダウンを有効にする
        yield return new WaitForSeconds(cooldownTime); // 指定された時間待機
        isCooldown = false; // クールダウン終了
    }
}
