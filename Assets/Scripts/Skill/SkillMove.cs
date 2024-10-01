using UnityEngine;

public class SkillMove : MonoBehaviour
{
    public float speed = 10f;  // �̵� �ӵ�
    protected float moveDistance;
    private Vector3 startPosition;

    private  void Start()
    {
        startPosition= transform.position;
    }
    protected virtual void Update()
    {
        // ������Ʈ�� ������ �̵���Ŵ (�����Ӹ��� ����)
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        moveDistance = Vector3.Distance(startPosition, transform.position);
    }
} 