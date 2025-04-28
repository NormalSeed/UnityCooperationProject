using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 


public class GameManagerLogic : MonoBehaviour
{
    public int totalItemCount; // �Ծ���ϴ� �������� ����
    public int stage; //�������� �ܰ�
    public Text stageCountText; // UI ������ ���� ���� ȭ�鿡 ������������ �Ծ���ϴ� �������� ������ ǥ��
    public Text playerCountText; // UI ������ ���� ���� ȭ�鿡 ���� ������������ ������ ȹ���� �������� ������ ǥ����
    void Awake()
    {
        stageCountText.text = "/ " + totalItemCount.ToString();
    }
    public void GetItem(int count)
    {
        playerCountText.text = count.ToString();
    }
    void OnTriggerEnter(Collider other) //Collider�� �ٸ� Ʈ���� �̺�Ʈ�� ħ������ �� OnTriggerEnter�� ȣ��

    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(stage);
        }
    }
}

