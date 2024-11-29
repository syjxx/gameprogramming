using UnityEngine;

public class ItemController : MonoBehaviour
{
    public string itemName; // "mouse", "keyboard", "pen"

    void OnMouseDown()
    {
        ItemGameManager.Instance.CollectItem(itemName); // 클릭한 아이템 전달
        Destroy(gameObject); // 아이템 클릭 시 제거
    }
}
