using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent monster;
    [SerializeField] private Transform target;
    [SerializeField] private MonsterAttack attack;

    void Start()
    {
        Init();
    }

    void Init()
    {
        monster = GetComponent<NavMeshAgent>();
        attack = GetComponent<MonsterAttack>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
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
