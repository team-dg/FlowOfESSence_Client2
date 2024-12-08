using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Abilities : MonoBehaviour
{
    public Camera cam;
    public GameObject Skillposition;

    [Header("��ų1")]
    public float skillCooltime1 = 5f;
    public GameObject Q;
    
    [Header("��ų2")]
    public float skillCooltime2 = 5f;
    public GameObject W;

    [Header("��ų3")]
    public float skillCooltime3 = 5f;
    public GameObject E;

    [Header("��ų4")]
    public float skillCooltime4 = 5f;
    public GameObject R;

    [Header("��ųF")]
    public float skillCooltimeF = 5f;

    public Image[] skillImage; 
    private bool[] isCooltime = new bool[6];

    PlayerMove PlayerMove;
    

    private void Start()
    {
        for(int i=0; i<isCooltime.Length; i++)
        {
            isCooltime[i] = false;
            skillImage[i].fillAmount = 0;
        }
        PlayerMove = GetComponent<PlayerMove>();
    }
    void Update()
    {
        Skill1();
        Skill2();
        Skill3();
        Skill4();
        SkillF();
    }
    void Skill1()
    {
        if (isCooltime[0])
        {
            skillImage[0].fillAmount -= 1 / skillCooltime1 * Time.deltaTime;
            if (skillImage[0].fillAmount <= 0)
            {
                skillImage[0].fillAmount = 0;
                isCooltime[0] = false;
            }
        }
    }
    void Skill2()
    {
        if (isCooltime[1])
        {
            skillImage[1].fillAmount -= 1 / skillCooltime2 * Time.deltaTime;
            if (skillImage[1].fillAmount <= 0)
            {
                skillImage[1].fillAmount = 0;
                isCooltime[1] = false;
            }
        }
    }
    void Skill3()
    {
        if (isCooltime[2])
        {
            skillImage[2].fillAmount -= 1 / skillCooltime3 * Time.deltaTime;
            if (skillImage[2].fillAmount <= 0)
            {
                skillImage[2].fillAmount = 0;
                isCooltime[2] = false;
            }
        }
    }

    void Skill4()
    {
        if (isCooltime[3])
        {
            skillImage[3].fillAmount -= 1 / skillCooltime4 * Time.deltaTime;
            if (skillImage[3].fillAmount <= 0)
            {
                skillImage[3].fillAmount = 0;
                isCooltime[3] = false;
            }
        }
    }
    void SkillF()
    {
        if (isCooltime[5])
        {
            skillImage[5].fillAmount -= 1 / skillCooltime4 * Time.deltaTime;
            if (skillImage[5].fillAmount <= 0)
            {
                skillImage[5].fillAmount = 0;
                isCooltime[5] = false;
            }
        }
    }

    Quaternion skillRotation;//��ų�� ������ �� ��ǥ�� �����ϰ� �ִϸ��̼� �߰��� ����
    Vector3 tempSkillPosition;
    void InstantiateSkill��ǥ(int i)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 direction = targetPosition - transform.position;

            // ȸ���� �� Y���� ���� (ĳ���Ͱ� ���Ʒ��� ȸ������ �ʵ���)//�̰� �� ���ص� �Ǵ°ɱ�
            direction.y = 0;

            // ĳ���Ͱ� ���콺 ������ �ٶ󺸵��� ȸ��
            transform.forward = direction;
            if(i!=5)
            {
                tempSkillPosition = Skillposition.transform.position;
                skillRotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, 180);
            }
            
        }
    }

    void InstantiateSkill(GameObject Y)
    {
        Instantiate(Y, tempSkillPosition, skillRotation);
    }

    public void ��ų���(int i)
    {
        if (!isCooltime[i])
        {
            isCooltime[i] = true;
            skillImage[i].fillAmount = 1;
            InstantiateSkill��ǥ(i);
        }

        if(i==5)
        {
            StartCoroutine(SmoothMove(transform.position, transform.position + transform.forward * 3.5f, 0.001f));
        }
    }

    public void �����̵�() // E��ų ��� �� ���� ������ �ε巴�� �̵�
    {
        float forwardDistance = 3.5f;  // �̵��� �Ÿ�
        float duration = 0.01f;  // �̵��� �ð� (0.05�� ���� �̵�)
        StartCoroutine(SmoothMove(transform.position, transform.position + transform.forward * forwardDistance, duration));
    } 

    // �ڷ�ƾ: �ð��� ������ ���� �ε巴�� �̵�
    private IEnumerator SmoothMove(Vector3 startPosition, Vector3 targetPosition, float duration)
    { 
        float elapsedTime = 0f;

        // duration ���� ���������� �̵�
        while (elapsedTime < duration)
        {
            // ���� �ð��� �������� ����
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);

            // ��� �ð� ����
            elapsedTime += Time.deltaTime;

            // ���� �����ӱ��� ���
            yield return null;
        }

        // ������ ��ġ ���� (��Ȯ�� ��ǥ ��ġ�� �����ϵ���)
        transform.position = targetPosition;
    }

    public bool GetskillCooltime(int i)
    {
        return isCooltime[i];
    }
}