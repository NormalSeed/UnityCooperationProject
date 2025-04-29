using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent monster;
    [SerializeField] private Transform target;
    [SerializeField] private MonsterAttack attack;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        if (monster == null)
        {
            monster = GetComponent<NavMeshAgent>();
            if (monster == null)
            {
                return; 
            }
        }

        if (attack == null)
        {
            attack = GetComponent<MonsterAttack>();
            if (attack == null)
            {
                return;
            }
        }

        if (target == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                target = playerObject.transform;
            }
            else
            {
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        if (target != null)
        {
            monster.SetDestination(target.position);
            if (attack.CanAttack(target))
            {
                attack.Attack(target);
            }
        }
    }
}
