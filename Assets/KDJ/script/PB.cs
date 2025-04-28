using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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