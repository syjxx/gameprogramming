using UnityEngine;

public class ItemController : MonoBehaviour
{
    public string itemName; // "mouse", "keyboard", "pen"

    void OnMouseDown()
    {
        ItemGameManager.Instance.CollectItem(itemName); // Ŭ���� ������ ����
        Destroy(gameObject); // ������ Ŭ�� �� ����
    }
}
