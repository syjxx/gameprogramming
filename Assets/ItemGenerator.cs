using System.Collections;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject[] itemPrefabs; // mouse, keyboard, pen ������
    // ���� ������ (���� �ӵ� ������ ����)
    public float minSpawnInterval = 0.1f; // �ּ� ���� ����
    public float maxSpawnInterval = 0.5f; // �ִ� ���� ����

    void Start()
    {
        StartCoroutine(GenerateItems());
    }

    IEnumerator GenerateItems()
    {
        while (true)
        {
            // ���� ������ �������� ����
            float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

            // �������� �������� ����
            int randomIndex = Random.Range(0, itemPrefabs.Length);

            // �������� ���� ��ġ�� ȭ�� ����� ������ ��ġ�� ����
            Vector3 spawnPosition = new Vector3(Random.Range(-7f, 7f), 6f, 0f);

            // ������ ����
            Instantiate(itemPrefabs[randomIndex], spawnPosition, Quaternion.identity);

            // ������ ������ ���ݸ�ŭ ���
            yield return new WaitForSeconds(randomInterval);
        }
    }
}
