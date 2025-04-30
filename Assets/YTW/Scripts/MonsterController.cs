using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent monster;
    [SerializeField] private Transform target;
    private IAttackable attackable;

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        if (monster == null)
        {
            monster = GetComponent<NavMeshAgent>();
            if (monster == null)
            {
                return;
            }
        }

        attackable = GetComponent<IAttackable>();
        if (attackable == null)
        {
            Debug.LogError($"{name}에 IAttackable를 구현한 컴포넌트를 적용 안했습니다.");
            return;
        }

        InitTarget();
    }

    private void InitTarget()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
            if (monster != null)
            {
                monster.isStopped = false;
            }
        }
        else
        {
            target = null;
            if (monster != null)
            {
                monster.isStopped = true;
            }
        }
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        if (target == null)
        {
            if (monster != null)
            {
                monster.isStopped = true;
            }
            InitTarget();
            return;
        }

        monster.isStopped = false;
        monster.SetDestination(target.position);

        float distance = Vector3.Distance(transform.position, target.position);

        // 사정거리 안에 들어왔을 때 수동으로 회전 
        if (distance <= monster.stoppingDistance)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0f;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }

        if (attackable.CanAttack(target))
        {
            attackable.Attack(target);
        }

    }
}
