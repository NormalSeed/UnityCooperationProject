using UnityEngine;
using UnityEngine.Events;

public class OnDamaged : MonoBehaviour
{
    [SerializeField] int MaxHP;
    [SerializeField] int CurHP;
    float amount = 1;
    public int CURHP { get { return CurHP; } }
    [SerializeField] PooledMonster pooledMonster;

    private HealthBar healthBar;


    private void OnEnable()
    {
        CurHP = MaxHP;
        healthBar = GetComponentInChildren<HealthBar>();
        if (pooledMonster == null && CompareTag("Monster"))
        {
            pooledMonster = GetComponent<PooledMonster>();
            if (pooledMonster == null)
            {
                Debug.LogError($"PooledMonster 컴포넌트가 {gameObject.name}에 없습니다!", gameObject);
            }
        }
        healthBar?.SetHP(amount);

    }

    public void TakeDamaged(int damage)
    {
        CurHP -= damage;
        healthBar?.SetHP((float)CurHP / MaxHP);

        Debug.Log($"{damage}데미지 받아서 현재 채력 {CurHP}");
        if (CurHP <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                PlayerDie();
            }
            if (pooledMonster != null)
            {
                returnMonster();
            }
        }
    }
    void PlayerDie()
    {
        gameObject.SetActive(false);
        StageManager.Instance.StageFailed();
    }

    void returnMonster()
    {
        gameObject.SetActive(false);
        pooledMonster.ReturnPool();
    }
    public bool Heal(int amount)
    {
        if (CurHP >= MaxHP)
        {
            return false;
        }
        CurHP += amount;
        if (CurHP > MaxHP)
            CurHP = MaxHP;
        healthBar?.SetHP((float)CurHP / MaxHP);
        return true;
    }
}
