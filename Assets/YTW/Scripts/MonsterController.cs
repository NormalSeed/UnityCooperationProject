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

        if (attackable == null)
        {
            if (CompareTag("Melee"))
                attackable = GetComponent<MeleeAttack>();
            else if (CompareTag("Ranged"))
                attackable = GetComponent<RangedAttack>();
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

            if (attackable.CanAttack(target))
            {
                attackable.Attack(target);
            }
        
    }
}
