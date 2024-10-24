using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public DoorController[] doors; // 複数のドアの参照

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // プレイヤーがスイッチに接触したら
        {
            foreach (DoorController door in doors)
            {
                door.ToggleDoor(); // 各ドアの開閉をトグル
            }
        }
    }
}
