using TMPro;
using UnityEngine;

public class CameraScprit : MonoBehaviour
{
    public Transform target;  // 카메라가 따라갈 타겟 (캐릭터)
    public Vector3 offset;    // 카메라와 타겟 간의 오프셋
    public Vector3 StartTransform;
    void LateUpdate()
    {
        // 카메라의 위치를 타겟의 위치에 맞춰 업데이트
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
