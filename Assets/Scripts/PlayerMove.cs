using UnityEngine;
using UnityEngine.AI;
public class PlayerMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;
    NavMeshAgent agent;
    public Camera cam;
    Vector3 destination;
    bool isMove;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.acceleration = float.MaxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            agent.velocity = Vector3.zero;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                SetDestination(hit.point);

                //agent.SetDestination(hit.point);
            }
        }
        Move();
    }
    private void SetDestination(Vector3 dest)
    {
        if (agent != null && agent.isOnNavMesh) // NavMesh 위에 있는지 확인
        {
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(dest, path) && path.status == NavMeshPathStatus.PathComplete) // 경로가 유효한지 확인
            {
                agent.SetPath(path);
                destination = dest;
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
            transform.forward = dir;
        }


    }
}
