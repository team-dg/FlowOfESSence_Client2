using UnityEngine;

public class SkillMove : MonoBehaviour
{
    public float speed = 10f;  // �̵� �ӵ�
    void Update()
    {
        // ������Ʈ�� ������ �̵���Ŵ (�����Ӹ��� ����)
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
