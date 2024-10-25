using UnityEngine;
using System.Collections.Generic; // Listを使用するための名前空間

public class Switch : MonoBehaviour
{
    public List<SlidingDoor> slidingDoors; // SlidingDoorのリストを作成

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // プレイヤーがスイッチに触れたとき
        {
            foreach (SlidingDoor slidingDoor in slidingDoors) // 各スライディングドアに対して
            {
                slidingDoor.ToggleDoor(); // ドアを開閉
            }
        }
    }
}
