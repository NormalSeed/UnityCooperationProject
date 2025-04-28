using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] Transform eyeTransform;
    [SerializeField] GameObject target;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask WallLayer;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float detectRadius;

    private void Start()
    {
        StartCoroutine(DetectTarget());
    }

    private void Update()
    {
        // 타겟이 있을 경우
        if (target != null)
        {
            Trace();
        }
    }


    private IEnumerator DetectTarget()
    {
        while (true)
        {
            Collider[] targets = Physics.OverlapSphere(eyeTransform.position, detectRadius, playerLayer);


            // 타겟이 "플레이어" 레이어를 가지고 있다면
            if (targets.Length > 0)
            {
                
                Vector3 direction = (targets[0].transform.position - eyeTransform.position).normalized;
                float distance = Vector3.Distance(eyeTransform.position, targets[0].transform.position);
                if (!Physics.Raycast(eyeTransform.position, direction, out RaycastHit hitInfo, distance, WallLayer))
                {
                    target = targets[0].gameObject;
                    Debug.DrawLine(eyeTransform.position, targets[0].transform.position, Color.red);
                    Debug.Log($"{target.name} 감지");
                }
                else
                {
                    target = null;
                }
            }
            else
            {
                target = null;

            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Trace()
    {
        // y축은 변화 x
        Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        Vector3 direction = (targetPos - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (eyeTransform == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(eyeTransform.position, detectRadius);
    }
}
