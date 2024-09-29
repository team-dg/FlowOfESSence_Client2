using UnityEngine;

public class SkillMove : MonoBehaviour
{
    public float speed = 10f;  // 이동 속도
    void Update()
    {
        // 오브젝트를 앞으로 이동시킴 (프레임마다 실행)
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
} 