using UnityEngine;
using System.Collections;

public class MinionCrate : MonoBehaviour
{
    public GameObject minionPrefab;  // �̴Ͼ� ������
    private float spawnInterval = 20f;  // 20�ʸ��� ����
    private int minionsPerWave = 6;  // �� ���� ������ �̴Ͼ� ��
    private float timeBetweenMinions = 0.5f;  // �̴Ͼ� ������ ���� (1��)

    void Start()
    {
        StartCoroutine(SpawnMinions());
    }

    IEnumerator SpawnMinions()
    {
        
        while (true)  // ���� �ݺ� (���� ���� �̴Ͼ� ����)
        {
            // 6������ �̴Ͼ��� 1�� �������� ����
            for (int i = 0; i < minionsPerWave; i++)
            {
                print("�����");
                Instantiate(minionPrefab, transform.position, transform.rotation);  // �̴Ͼ� ����
                yield return new WaitForSeconds(timeBetweenMinions);  // 1�� ��� �� ���� �̴Ͼ� ����
            }
            yield return new WaitForSeconds(spawnInterval);//20�� ���
        }
    }
}
