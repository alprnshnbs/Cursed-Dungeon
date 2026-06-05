using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Takip Edilecek Hedef")]
    public Transform target;

    [Header("Kamera Ayarları")]
    public float smoothTime = 0.2f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    private Vector3 velocity = Vector3.zero;

    // Kamera sabitleme değişkenleri
    private bool isLocked = false;
    private Vector3 lockedPosition;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition;

        // Kamera kilitliyse sabit noktayı, değilse oyuncuyu hedef al
        if (isLocked)
        {
            targetPosition = lockedPosition;
        }
        else
        {
            targetPosition = target.position + offset;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    // Dışarıdan kamerayı kilitlemek için çağrılacak fonksiyon
    public void LockCamera(Vector3 position)
    {
        lockedPosition = new Vector3(position.x, position.y, offset.z);
        isLocked = true;
    }

    // Savaşı bitirip takibi geri açmak için çağrılacak fonksiyon
    public void UnlockCamera()
    {
        isLocked = false;
    }
}