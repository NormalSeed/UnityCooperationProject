using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//이 코드는 공이 이동하고, 점프하며, 아이템을 먹고, 목표 지점에 도달하면 다음 스테이지로 이동하는 구조에서 사용
//플레이어가 공을 굴리며 장애물을 피하고 아이템을 전부 수집해서 목표 지점에 도착해야 클리어
public class PB : MonoBehaviour
{
    public float jumpPower;
    public int ItemCount;
    bool isJump;
    Rigidbody rigid;
    AudioSource audio;
    public GameManagerLogic manager;
    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isJump == false)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
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
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            ItemCount++;
            audio.Play();
            other.gameObject.SetActive(false);
            manager.GetItem(ItemCount);
        }
        else if (other.tag == "Point")
        {
            if (manager.totalItemCount == ItemCount)
            {
                //clear
                SceneManager.LoadScene("stage_" + (manager.stage + 1).ToString());
            }
            else
            {
                SceneManager.LoadScene("stage_" + manager.stage.ToString());
            }
        }
    }
}
