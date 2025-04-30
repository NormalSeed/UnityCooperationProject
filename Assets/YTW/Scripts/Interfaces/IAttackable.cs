using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    public bool CanAttack(Transform target);
    public void Attack(Transform target);
}
