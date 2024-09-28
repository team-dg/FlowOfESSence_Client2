using UnityEngine;
using UnityEngine.UI;
public class Abilities : MonoBehaviour
{
    public Camera cam;
    public GameObject Skillposition;

    [Header("��ų1")]
    public Image skillImage1;
    public float skillCooltime1 = 5f;
    private bool isCooltime1 = false;
    public GameObject Q;
    
    [Header("��ų2")]
    public Image skillImage2;
    public float skillCooltime2 = 5f;
    private bool isCooltime2 = false;
    public GameObject W;

    [Header("��ų3")]
    public Image skillImage3;
    public float skillCooltime3 = 5f;
    private bool isCooltime3 = false;
    public GameObject E;

    [Header("��ų4")]
    public Image skillImage4;
    public float skillCooltime4 = 5f;
    private bool isCooltime4 = false;
    public GameObject R;


    private void Start()
    {
        skillImage1.fillAmount = 0;
        skillImage2.fillAmount = 0;
        skillImage3.fillAmount = 0;
    }
    void Update()
    {
        Skill1();
        Skill2();
        Skill3();
        Skill4();
    }
    void Skill1()
    {
        if (Input.GetKey(KeyCode.Q) && !isCooltime1)
        {
            isCooltime1 = true;
            skillImage1.fillAmount = 1;
            InstantiateSkill(Q);
        }
        if (isCooltime1)
        {
            skillImage1.fillAmount -= 1 / skillCooltime1 * Time.deltaTime;
            if (skillImage1.fillAmount <= 0)
            {
                skillImage1.fillAmount = 0;
                isCooltime1 = false;
            }
        }
       
    }
    void Skill2()
    {
        if (Input.GetKey(KeyCode.W) && !isCooltime2)
        {
            isCooltime2 = true;
            skillImage2.fillAmount = 1;
            InstantiateSkill(W);
        }
        if (isCooltime2)
        {
            skillImage2.fillAmount -= 1 / skillCooltime2 * Time.deltaTime;
            if (skillImage2.fillAmount <= 0)
            {
                skillImage2.fillAmount = 0;
                isCooltime2 = false;
            }
        }
    }
    void Skill3()
    {
        if (Input.GetKey(KeyCode.E) && !isCooltime3)
        {
            isCooltime3 = true;
            skillImage3.fillAmount = 1;
            InstantiateSkill(W);
        }
        if (isCooltime3)
        {
            skillImage3.fillAmount -= 1 / skillCooltime3 * Time.deltaTime;
            if (skillImage3.fillAmount <= 0)
            {
                skillImage3.fillAmount = 0;
                isCooltime3 = false;
            }
        }
    }

    void Skill4()
    {
        if (Input.GetKey(KeyCode.R) && !isCooltime4)
        {
            isCooltime4 = true;
            skillImage4.fillAmount = 1;
            InstantiateSkill(R);
        }
        if (isCooltime4)
        {
            skillImage4.fillAmount -= 1 / skillCooltime4 * Time.deltaTime;
            if (skillImage4.fillAmount <= 0)
            {
                skillImage4.fillAmount = 0;
                isCooltime4 = false;
            }
        }
    }

    void InstantiateSkill(GameObject Y)
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
            Quaternion skillRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            Instantiate(Y, Skillposition.transform.position, skillRotation);
        }
    }
}
