using System.Collections;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject[] itemPrefabs; // mouse, keyboard, pen 프리팹
    // 레벨 디자인 (생성 속도 무작위 설정)
    public float minSpawnInterval = 0.1f; // 최소 생성 간격
    public float maxSpawnInterval = 0.5f; // 최대 생성 간격

    void Start()
    {
        StartCoroutine(GenerateItems());
    }

    IEnumerator GenerateItems()
    {
        while (true)
        {
            // 생성 간격을 무작위로 설정
            float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

            // 아이템을 무작위로 선택
            int randomIndex = Random.Range(0, itemPrefabs.Length);

            // 아이템의 생성 위치를 화면 상단의 무작위 위치로 설정
            Vector3 spawnPosition = new Vector3(Random.Range(-7f, 7f), 6f, 0f);

            // 아이템 생성
            Instantiate(itemPrefabs[randomIndex], spawnPosition, Quaternion.identity);

            // 설정된 무작위 간격만큼 대기
            yield return new WaitForSeconds(randomInterval);
        }
    }
}
