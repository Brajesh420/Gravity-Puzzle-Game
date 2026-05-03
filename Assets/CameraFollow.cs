using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float height = 2.5f;
    public float smoothSpeed = 6f;
    public float positionSmooth = 5f;
    public float rotationSmooth = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 origin = target.position + target.up * 1.5f;

        // Direction behind player
        Vector3 direction = -target.forward;

        float desiredDistance = distance;
        float minDistance = 1.5f;

        RaycastHit hit;

        // 🔥 Check for obstruction
        if (Physics.Raycast(origin, direction, out hit, distance))
        {
            desiredDistance = Mathf.Clamp(hit.distance - 0.3f, minDistance, distance);
        }

        Vector3 desiredPosition =
            origin + direction * desiredDistance;

        // Smooth position
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        // Always look at player
        transform.LookAt(origin);
    }
}