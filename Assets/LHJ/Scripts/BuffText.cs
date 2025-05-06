using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffText : MonoBehaviour
{
    public GameObject attackBuffText;
    public GameObject speedBuffText;
    public GameObject scoreText;
    public GameObject healText;
    public GameObject healFailText;

    public void ShowBuff(string type, float buffTime)
    {
        if(type == "Attack")
        {
            StartCoroutine(TempText(attackBuffText, buffTime));
        }
        else if(type == "Speed")
        {
            StartCoroutine(TempText(speedBuffText, buffTime));
        }
        else if(type == "Score")
        {
            StartCoroutine(TempText(scoreText, buffTime));
        }
        else if(type == "Heal")
        {
            StartCoroutine(TempText(healText, buffTime));
        }
    }
    private IEnumerator TempText(GameObject buffText, float buffTime)
    {
        buffText.SetActive(true);
        yield return new WaitForSeconds(buffTime);
        buffText.SetActive(false);
    }
    public void ShowHealFail(float time = 2f)
    {
        StartCoroutine(TempText(healFailText, time));
    }
}
