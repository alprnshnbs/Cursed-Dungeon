using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Atış Ayarları")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 2f;

    [Header("Hedefleme Ayarları")]
    [SerializeField] private bool aimAtPlayer = true;
    [SerializeField] private Vector2 fixedDirection = Vector2.left;

    private Transform playerTarget;
    private float nextFireTime;

    // Ateş sistemini durduran/başlatan şalter
    private bool isShootingActive = false;

    void Start()
    {
        if (aimAtPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTarget = player.transform;
            }
        }
    }

    void Update()
    {
        // Şalter kapalıysa alt satırları işleme alma
        if (!isShootingActive) return;

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    // CombatEncounter tetiklendiğinde bu fonksiyonu çağıracak
    public void StartShooting()
    {
        isShootingActive = true;
        // Aktif edildiği an, belirlenen süre kadar bekleyip ilk mermiyi atar
        nextFireTime = Time.time + 1f / fireRate;
    }

    // Savaş bittiğinde çağrılacak
    public void StopShooting()
    {
        isShootingActive = false;
    }

    private void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;

        Vector2 shootDir = fixedDirection;
        if (aimAtPlayer && playerTarget != null)
        {
            shootDir = (playerTarget.position - firePoint.position).normalized;
        }

        Projectile newProjectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        newProjectile.Setup(shootDir);
    }
}