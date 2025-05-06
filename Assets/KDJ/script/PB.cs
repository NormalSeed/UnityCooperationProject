using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PB : MonoBehaviour
{
    public float jumpPower = 5f;
    public int ItemCount = 0;

    private bool isJump = false;
    private Rigidbody rigid;
    private AudioSource audio;

    // 플레이어 능력치 변수
    public int attack = 10;
    public float playerSpeed = 5f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.AddForce(new Vector3(h, 0, v) * playerSpeed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemCount++;
            audio.Play();
            other.gameObject.SetActive(false);
            Debug.Log("Item Collected: " + ItemCount);
        }
    }

    // 능력치 증가 메서드
    public void IncreaseAttack(int amount)
    {
        attack += amount;
        Debug.Log("공격력 증가: " + attack);
    }

    public void IncreaseSpeed(float amount)
    {
        playerSpeed += amount;
        Debug.Log("속도 증가: " + playerSpeed);
    }

    public void IncreaseScore(int amount)
    {
        if (StageManager.Instance != null)
        {
            StageManager.Instance.StageScore += amount;
            Debug.Log("점수 증가: " + StageManager.Instance.StageScore);
        }
    }
}

