using TMPro;
using UnityEngine;

public class CameraScprit : MonoBehaviour
{
    public Transform target;  // ī�޶� ���� Ÿ�� (ĳ����)
    public Vector3 offset;    // ī�޶�� Ÿ�� ���� ������
    public Vector3 StartTransform;

    bool canMoveCamera = true;

    void LateUpdate()
    {
        // ī�޶��� ��ġ�� Ÿ���� ��ġ�� ���� ������Ʈ
        if (Input.GetKey(KeyCode.Space)|| !canMoveCamera)
        {
            transform.position = new Vector3(target.position.x + StartTransform.x, StartTransform.y, target.position.z + StartTransform.z);
        }
            
        //transform.position = target.position +StartTransform;
    }

    void Awake()
    {
        StartTransform = transform.position-target.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            transform.position = new Vector3(target.position.x + StartTransform.x, StartTransform.y, target.position.z + StartTransform.z);
            canMoveCamera = !canMoveCamera;
        }
        if(canMoveCamera)
        {
            Vector3 pos = transform.position;
            if (Input.mousePosition.y >= Screen.height - 10)// ��ũ�� ��ǥ�� ���� ���� (0,0)�̰� ������ ���� (Screen.width, Screen.height)�̴�.
            {
                pos.z -= 20 * Time.deltaTime;
            }
            if (Input.mousePosition.y <= 10)
            {
                pos.z += 20 * Time.deltaTime;
            }
            if (Input.mousePosition.x >= 10)
            {
                pos.x -= 20 * Time.deltaTime;
            }
            if (Input.mousePosition.x <= Screen.width - 10)
            {
                pos.x += 20 * Time.deltaTime;
            }

            if (pos.x >= -139 && pos.x <= -4.5 && pos.z >= -13 && pos.z <= 135f)
            {
                transform.position = pos;
            }
        }
        
    }
}
 