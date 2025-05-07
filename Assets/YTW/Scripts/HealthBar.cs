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
    private float targetAmount = 1f;
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        RenewHP();
        SetCamera();
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

        if (lerpDuration <= 0)
        {
            Debug.LogWarning("lerpDuration이 설정되어있지 않았습니다.");
        }

        fillImage.fillAmount = 1f;
        UpdateColor(1f);
    }
    void SetCamera()
    {
        Vector3 direction = (transform.position - mainCamera.transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }
    public void SetHP(float amount)
    {
        targetAmount = Mathf.Clamp01(amount);
        UpdateColor(targetAmount);
    }

    void RenewHP()
    {
        if (!Mathf.Approximately(fillImage.fillAmount, targetAmount))
        {
            float t = Time.deltaTime / lerpDuration;
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetAmount, t);
            UpdateColor(fillImage.fillAmount);
        }
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
        fillImage.fillAmount = targetAmount;
        UpdateColor(targetAmount);
    }

    private void OnDisable()
    {
        fillImage.fillAmount = 1f;
        targetAmount = 1f;
        UpdateColor(1f);
    }
}
