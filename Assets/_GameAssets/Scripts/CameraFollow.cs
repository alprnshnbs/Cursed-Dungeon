using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Takip Edilecek Hedef")]
    public Transform target;

    [Header("Kamera Ayarlari")]
    public float smoothTime = 0.2f;
    public Vector3 offset = new Vector3(0f, 0f, -10f); // 2D'de Z ekseni genellikle -10'da durmalıdır

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}