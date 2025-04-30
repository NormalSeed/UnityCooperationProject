using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuffText : MonoBehaviour
{
    public GameObject attackBuffText;

    public void ShowAttackBuff(float buffTime)
    {
        StartCoroutine(TempText(buffTime));
    }
    private IEnumerator TempText(float buffTime)
    {
        attackBuffText.SetActive(true);
        yield return new WaitForSeconds(buffTime);
        attackBuffText.SetActive(false);
    }
}
