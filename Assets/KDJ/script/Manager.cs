using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManagerLogic : MonoBehaviour
{
    public int totalItemCount; // 먹어야하는 아이템의 갯수
    public int stage; //스테이지 단계
    public Text stageCountText; // UI 설정을 위한 변수 화면에 스테이지에서 먹어야하는 아이템의 갯수를 표시
    public Text playerCountText; // UI 설정을 위한 변수 화면에 현쟈 스테이지에서 유저가 획득한 아이템의 갯수를 표시함
    void Awake()
    {
        stageCountText.text = "/ " + totalItemCount.ToString();
    }
    public void GetItem(int count)
    {
        playerCountText.text = count.ToString();
    }
    void OnTriggerEnter(Collider other) //Collider가 다른 트리거 이벤트에 침입했을 때 OnTriggerEnter가 호출

    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(stage);
        }
    }
}