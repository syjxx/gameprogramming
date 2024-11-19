using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환

public class ItemGameManager : MonoBehaviour
{
    public static ItemGameManager Instance; // 싱글톤 인스턴스

    public Transform[] itemDisplayPositions; // 수집된 아이템 표시 위치
    public Sprite[] itemIcons; // 아이템 아이콘 이미지
    private string targetItem = ""; // 목표 아이템
    private int collectedCount = 0; // 수집한 아이템 개수

    void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectItem(string itemName)
    {
        if (targetItem == "" || targetItem == itemName)
        {
            // 동일한 아이템 수집
            targetItem = itemName; // 목표 아이템 설정
            collectedCount++; // 수집 개수 증가
            UpdateItemDisplay(itemName); // UI 업데이트

            if (collectedCount >= 3)
            {
                Debug.Log("게임 클리어! NameScene으로 전환됩니다.");
                SceneManager.LoadScene("NameScene"); // NameScene으로 전환
            }
        }
        else
        {
            // 다른 아이템 클릭 시 초기화
            Debug.Log($"다른 아이템({itemName}) 클릭: 초기화");
            collectedCount = 0; // 수집 개수 초기화
            targetItem = ""; // 목표 아이템 초기화
            ResetItemDisplay(); // 화면 초기화
        }
    }

    private void UpdateItemDisplay(string itemName)
    {
        ResetItemDisplay(); // 기존 표시를 초기화

        for (int i = 0; i < collectedCount; i++)
        {
            // 아이템 아이콘을 표시
            SpriteRenderer sr = itemDisplayPositions[i].GetComponent<SpriteRenderer>();
            sr.sprite = GetItemIcon(itemName); // 해당 아이템의 아이콘 설정
            sr.color = Color.white; // 아이콘 표시
        }
    }

    private void ResetItemDisplay()
    {
        // 아이콘 초기화 (투명하게 변경)
        foreach (Transform position in itemDisplayPositions)
        {
            SpriteRenderer sr = position.GetComponent<SpriteRenderer>();
            sr.sprite = null;
            sr.color = Color.clear;
        }
    }

    private Sprite GetItemIcon(string itemName)
    {
        // 아이템 이름에 따라 아이콘 반환
        switch (itemName)
        {
            case "mouse": return itemIcons[0];
            case "keyboard": return itemIcons[1];
            case "pen": return itemIcons[2];
            default: return null;
        }
    }
}
