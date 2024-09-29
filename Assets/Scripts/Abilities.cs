using UnityEngine;
using UnityEngine.UI;
public class Abilities : MonoBehaviour
{
    public Camera cam;
    public GameObject Skillposition;

    [Header("스킬1")]
    public float skillCooltime1 = 5f;
    public GameObject Q;
    
    [Header("스킬2")]
    public float skillCooltime2 = 5f;
    public GameObject W;

    [Header("스킬3")]
    public float skillCooltime3 = 5f;
    public GameObject E;

    [Header("스킬4")]
    public float skillCooltime4 = 5f;
    public GameObject R;

    public Image[] skillImage; 
    private bool[] isCooltime = new bool[4];

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

    Quaternion skillRotation;//스킬을 눌렀을 때 좌표를 저장하고 애니메이션 중간에 생성
    void InstantiateSkill좌표()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 direction = targetPosition - transform.position;

            // 회전할 때 Y축을 고정 (캐릭터가 위아래로 회전하지 않도록)//이거 왜 안해도 되는걸까
            direction.y = 0;

            // 캐릭터가 마우스 방향을 바라보도록 회전
            transform.forward = direction;
            skillRotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, 180);
        }
    }

    void InstantiateSkill(GameObject Y)
    {
        Instantiate(Y, Skillposition.transform.position, skillRotation);
    }

    public void 스킬사용(int i)
    {
        if (!isCooltime[i])
        {
            isCooltime[i] = true;
            skillImage[i].fillAmount = 1;
            InstantiateSkill좌표();
        }
    }

    public bool GetskillCooltime(int i)
    {
        return isCooltime[i];
    }
}