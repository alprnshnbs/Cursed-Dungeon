using UnityEngine;
using System.Collections; // Coroutine kullanabilmek için gerekli kütüphane

public class CombatEncounter : MonoBehaviour
{
    [Header("Sistem Referansları")]
    [SerializeField] private CameraFollow cameraFollowScript;
    [SerializeField] private Transform cameraLockPosition;
    [SerializeField] private GameObject arenaBoundaries;

    [Header("Düşman Hareket Ayarları")]
    [SerializeField] private Transform targetSlidePosition; // Düşmanın gideceği hedef nokta
    [SerializeField] private float slideSpeed = 5f;         // Kayma hızı

    private bool isCombatStarted = false;

    void Start()
    {
        if (arenaBoundaries != null)
        {
            arenaBoundaries.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCombatStarted && collision.CompareTag("Player"))
        {
            StartCombat();
        }
    }

    // Zaman içinde hareketi sağlayan Coroutine fonksiyonu
    private IEnumerator SlideToPosition(Vector3 targetPos)
    {
        // Hedef noktaya olan mesafe 0.01 birimden büyük olduğu sürece çalışır
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            // Objeyi mevcut konumdan hedefe doğru, belirlenen hızda hareket ettirir
            transform.position = Vector3.MoveTowards(transform.position, targetPos, slideSpeed * Time.deltaTime);

            // Bir sonraki kareye (frame) kadar bekler, böylece oyun donmaz
            yield return null;
        }

        // Döngü bittiğinde milimetrik sapmaları önlemek için objeyi tam hedefe oturtur
        transform.position = targetPos;
    }

    private void StartCombat()
    {
        isCombatStarted = true;

        if (cameraFollowScript != null && cameraLockPosition != null)
        {
            cameraFollowScript.LockCamera(cameraLockPosition.position);
        }

        if (arenaBoundaries != null)
        {
            arenaBoundaries.SetActive(true);
        }

        if (targetSlidePosition != null)
        {
            StartCoroutine(SlideToPosition(targetSlidePosition.position));
        }

        // YENİ EKLENEN KISIM: Savaş başladığında atış kodunu aktif et
        EnemyShooter shooter = GetComponent<EnemyShooter>();
        if (shooter != null)
        {
            shooter.StartShooting();
        }
    }

    public void EndCombat()
    {
        if (cameraFollowScript != null)
        {
            cameraFollowScript.UnlockCamera();
        }

        if (arenaBoundaries != null)
        {
            arenaBoundaries.SetActive(false);
        }

        // YENİ EKLENEN KISIM: Savaş bittiğinde atışı durdur
        EnemyShooter shooter = GetComponent<EnemyShooter>();
        if (shooter != null)
        {
            shooter.StopShooting();
        }
    }
}