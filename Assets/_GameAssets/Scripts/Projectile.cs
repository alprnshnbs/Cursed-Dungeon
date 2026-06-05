using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Mermi Ayarları")]
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 5f; // Ekranda sonsuza kadar gidip RAM'i doldurmaması için

    private Vector2 moveDirection;

    // Düşman ateş ettiğinde mermiye yönünü vermek için bu fonksiyonu çağıracak
    public void Setup(Vector2 direction)
    {
        moveDirection = direction.normalized;

        // Mermiyi belirli bir süre sonra otomatik yok et
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Mermiyi belirlenen yönde hareket ettir
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mermi oyuncuya çarparsa
        if (collision.CompareTag("Player"))
        {
            // İleride buraya oyuncunun canını düşürme kodunu ekleyeceksin
            Debug.Log($"Oyuncuya {damage} hasar vuruldu!");
            Destroy(gameObject);
        }
        // Mermi duvara veya zemine çarparsa
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}