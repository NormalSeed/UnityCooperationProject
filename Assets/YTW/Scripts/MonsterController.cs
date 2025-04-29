using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private MonsterAttack attack;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        attack = GetComponent<MonsterAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
