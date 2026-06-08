using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Mermi Ayarları")]
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 5f;

    private Vector2 moveDirection;

    public void Setup(Vector2 direction)
    {
        moveDirection = direction.normalized;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Çarpılan objede HealthSystem betiği olup olmadığını kontrol et
            HealthSystem targetHealth = collision.GetComponent<HealthSystem>();

            if (targetHealth != null)
            {
                // Can sistemindeki TakeDamage fonksiyonunu çağırarak hasar ver
                targetHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}