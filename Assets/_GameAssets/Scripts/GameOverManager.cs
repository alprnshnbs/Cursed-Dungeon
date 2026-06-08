using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI Referansları")]
    [SerializeField] private GameObject gameOverPanel;

    void Start()
    {
        // Oyun başladığında panelin kapalı olduğundan emin ol
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // Karakter öldüğünde (HealthSystem üzerinden) çağrılacak fonksiyon
    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Arka planda oyunun akmasını durdurur (düşmanlar, mermiler vb. donar)
        Time.timeScale = 0f;
    }

    // Yeniden Başla (Restart) butonuna tıklandığında çağrılacak fonksiyon
    public void RestartGame()
    {
        // Zamanı normal hızına (1) döndür
        Time.timeScale = 1f;

        // Mevcut açık olan sahneyi baştan yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}