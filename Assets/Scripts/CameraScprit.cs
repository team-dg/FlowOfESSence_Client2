using TMPro;
using UnityEngine;

public class CameraScprit : MonoBehaviour
{
    public Transform target;  // ī�޶� ���� Ÿ�� (ĳ����)
    public Vector3 offset;    // ī�޶�� Ÿ�� ���� ������
    public Vector3 StartTransform;
    void LateUpdate()
    {
        // ī�޶��� ��ġ�� Ÿ���� ��ġ�� ���� ������Ʈ
        transform.position = new Vector3(target.position.x+ StartTransform.x, StartTransform.y, target.position.z+ StartTransform.z);
        //transform.position = target.position +StartTransform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        StartTransform = transform.position-target.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
