using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image fillImage;
    [SerializeField] float lerpDuration;
    [SerializeField] AnimationCurve lerpCurve;

    private Coroutine lerpCoroutine;
    private Camera mainCamera;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        Vector3 direction = (transform.position - mainCamera.transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }

    private void Init()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogWarning("UI(HP바)에 카메라가 설정되어있지 않았습니다.");
        }

        if (lerpCurve == null)
        {
            Debug.LogWarning("AnimationCurve가 설정되어있지 않았습니다.");
        }

        if (lerpDuration == null)
        {
            Debug.LogWarning("lerpDuration이 설정되어있지 않았습니다.");
        }
    }

    public void SetHP(float amount)
    {
        amount = Mathf.Clamp01(amount);

        if (Mathf.Approximately(fillImage.fillAmount, amount))
        {
            fillImage.fillAmount = amount;
            return;
        }

        if (lerpCoroutine == null)
        {
            lerpCoroutine = StartCoroutine(LerpHP(amount));
        }
        else
        {
            StopCoroutine(lerpCoroutine);
        }
    }

    private IEnumerator LerpHP(float targetAmount)
    {
        // 현재 hp 바의 체력 비율
        float startAmount = fillImage.fillAmount;
        // 경과시간
        float elapsed = 0f;

        // lerpDuration동안 루프 실행
        while (elapsed < lerpDuration)
        {
            elapsed += Time.deltaTime;
            // 0에서 1사이 값 t
            float t = elapsed / lerpDuration;
            // easyout으로 진행도
            float curveT = 1f - lerpCurve.Evaluate(1f - t);
            // 계산된 진행도로 보간
            fillImage.fillAmount = Mathf.Lerp(startAmount, targetAmount, curveT);
            UpdateColor(fillImage.fillAmount);
            // 한프레임 씩 코루틴 실행
            yield return null;
        }

        fillImage.fillAmount = targetAmount;
        lerpCoroutine = null;
    }

    private void UpdateColor(float amount)
    {
        if (amount > 0.5f)
            fillImage.color = Color.green;
        else if (amount > 0.2f)
            fillImage.color = Color.yellow;
        else
            fillImage.color = Color.red;
    }
    private void OnEnable()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void OnDisable()
    {
        if (lerpCoroutine != null)
        {
            StopCoroutine(lerpCoroutine);
            lerpCoroutine = null;
        }
        fillImage.fillAmount = 1f;
        UpdateColor(1f);
    }
}
