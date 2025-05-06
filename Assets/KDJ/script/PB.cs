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

        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
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

    // 공격력, 속도, 점수 관련 메서드는 다른 스크립트PlayerStats아니면GameManager로 분리해서 넣어야함
}
//확인
