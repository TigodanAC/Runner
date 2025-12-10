using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + new Vector3(0, 6, -10);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, 10f * Time.deltaTime);
        transform.position = smoothPosition;

        transform.LookAt(target);
    }
}
