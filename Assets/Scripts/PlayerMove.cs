using UnityEngine;
using UnityEngine.AI;
public class PlayerMove : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private Vector3 destination;

    private bool isMove;
    private bool isSkill = false;

    public Camera cam;
    public GameObject Mouseclick;
    public GameObject HPbar;
    public int �Ϲݰ��ݹ���;

    Abilities abilities;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        abilities= GetComponent<Abilities>();
        agent.updateRotation = false;//Nav�� �����̼��� ����Ǵ� ���� ����
        agent.acceleration = float.MaxValue;//���ӵ��� �ִ�� �ؼ� �� �ε巴��
    }

    // Update is called once per frame
    void Update()
    {
        if (isSkill==false)
        {
            if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Alpha1))
            {
                isMove = false;
                agent.ResetPath();//�̵��� ��� 
                animator.SetInteger("IsCtrl", 1);
                animator.SetBool("IsMove", false);
            }
            if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Alpha2))
            {
                isMove = false;
                agent.ResetPath();//�̵��� ���
                animator.SetInteger("IsCtrl", 2);
                animator.SetBool("IsMove", false);
            }
            if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Alpha3))
            {
                isMove = false;
                agent.ResetPath();//�̵��� ���
                animator.SetInteger("IsCtrl", 3);
                animator.SetBool("IsMove", false);
            }
            if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Alpha4))
            {
                isMove = false;
                agent.ResetPath();//�̵��� ���
                animator.SetInteger("IsCtrl", 4);
                animator.SetBool("IsMove", false);
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                isMove = false;
                agent.ResetPath();//�̵��� ���
                AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
                if (!currentState.IsTag("Return"))
                {
                    animator.SetTrigger("Recall");
                    animator.SetBool("IsMove", false);
                }
            }
            if (Input.GetKeyDown(KeyCode.Q)&& !abilities.GetskillCooltime(0))
            {
                isMove = false;
                agent.ResetPath();//�̵��� ���
                animator.SetTrigger("IsSkill1");
                animator.SetBool("IsMove", false);
                isSkill = true;
                abilities.��ų���(0);
            }
            if (Input.GetKeyDown(KeyCode.W) && !abilities.GetskillCooltime(1))
            {
                isMove = false;
                agent.ResetPath();//�̵��� ���
                animator.SetTrigger("IsSkill2");
                animator.SetBool("IsMove", false);
                isSkill = true;
                abilities.��ų���(1);
            }

            if (Input.GetKeyDown(KeyCode.E) && !abilities.GetskillCooltime(2))
            {
                isMove = false;
                agent.ResetPath();//�̵��� ���
                animator.SetTrigger("IsSkill3");
                animator.SetBool("IsMove", false);
                isSkill = true;
                abilities.��ų���(2);
            }

            if (Input.GetKeyDown(KeyCode.R) && !abilities.GetskillCooltime(3))
            {
                isMove = false;
                agent.ResetPath();//�̵��� ���
                animator.SetTrigger("IsSkill4");
                animator.SetBool("IsMove", false);
                isSkill = true;
                abilities.��ų���(3);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                isMove = false;
                agent.ResetPath();//�̵��� ���
                animator.SetInteger("IsAttack", 1);
            }
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetInteger("IsAttack", 0);
                animator.SetInteger("IsCtrl", 0);
                agent.velocity = Vector3.zero;

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                
                // ù ��° Raycast: ���� Ŭ���ߴ��� Ȯ��
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, hit.collider.transform.position);

                    if (hit.collider.CompareTag("Enemy")&& distanceToEnemy <= �Ϲݰ��ݹ���)//���� if������ �÷��̾� �߽����κ��� ���������� ���� �Ÿ��� �ִ� ��븸 �����ϰ�;�
                    {
                        isMove = false;
                        agent.ResetPath();//�̵��� ���
                        animator.SetTrigger("CanAttack");
                        Debug.Log("������");

                        Vector3 targetPosition = hit.point;
                        Vector3 direction = targetPosition - transform.position;

                        // ȸ���� �� Y���� ���� (ĳ���Ͱ� ���Ʒ��� ȸ������ �ʵ���)//�̰� �� ���ص� �Ǵ°ɱ�
                        //direction.y = 0;

                        // ĳ���Ͱ� ���콺 ������ �ٶ󺸵��� ȸ��
                        transform.forward = direction;
                    }
                    else
                    {
                        Instantiate(Mouseclick).transform.position = hit.point;
                        // ���� �ƴϴ��� Ŭ�� ��ġ�� ������Ʈ�� ���� ��� �̵�
                        SetDestination(hit.point);
                    }
                }
                else
                {
                    // �� ��° Raycast: �ƹ� ������Ʈ���� �浹���� �ʾ��� ��� Ŭ�� ��ġ�� �̵�
                    if (Physics.Raycast(ray, out hit))
                    {
                        Instantiate(Mouseclick).transform.position = hit.point; // Ŭ�� ��� ����
                        SetDestination(hit.point); // ������ ����
                    }
                }
            }
        }

        Move();
        Vector3 hpPosition = new Vector3(0,2.5f,0)+transform.position;//Hp�ٰ� ������ �������� 2.5��ŭ �÷���
        HPbar.transform.position = hpPosition;
    }
    private void SetDestination(Vector3 dest)
    {
        if (agent != null && agent.isOnNavMesh) // NavMesh ���� �ִ��� Ȯ��
        {
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(dest, path) && path.status == NavMeshPathStatus.PathComplete) // ��ΰ� ��ȿ���� Ȯ��
            {
                agent.SetPath(path);
                destination = dest;
                if(isMove==false)//���� �����̴� ���°� �ƴ϶��
                {
                    animator.SetTrigger("CanMove");//anyState���� Ʈ���Ÿ� �ߵ��ؼ� �ٷ� Move�� �̵�
                }
                isMove = true;
                animator.SetBool("IsMove", true);
            }
            else
            {
                Debug.LogWarning("Unable to calculate path or incomplete path.");
            }
        }
        else
        {
            Debug.LogError("NavMeshAgent is not on the NavMesh.");
        }

    }
    private void Move()
    {
        if (isMove)
        {
            if (Vector3.Distance(transform.position, destination) <= 0.1f)
            {
                isMove = false;
                animator.SetBool("IsMove", false);
                return;
            }
            var dir = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z) - transform.position;
            transform.forward = dir;//ĳ������ rotation�� �̵��ϴ� �������� ����
        }
    }

    public void AnimationCtrlEnd()
    {
        Debug.Log("AnimationCtrlEnd �Լ��� ȣ��Ǿ����ϴ�");
        animator.SetInteger("IsCtrl", 0);
    }
    public void AnimationSkillEnd()
    {
        Debug.Log("AnimationSkillEnd �Լ��� ȣ��Ǿ����ϴ�");
        isSkill = false;
    }
    public void AnimationAttackEnd()
    {
        animator.SetInteger("IsAttack", 0);
    }

    public bool GetisSkill()
    {
        return isSkill;
    }
    public void ��ȯ()
    {
        gameObject.transform.position = new Vector3(-4.1f, 2.46f, 107.78f);
    }
}
