using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class BaseHealth : MonoBehaviour
{
    
    
    
    [Header("Configurações da base")]
    
    [SerializeField] private int health = 10;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private string cenaGameOver = "GameOver";
    [SerializeField] private TMP_Text pontosText;
    public int pontos = 0;
    public void  LateUpdate()
    {
        healthText.text = " vidas: " + health.ToString();
        pontosText.text = " Pontos: " + pontos.ToString();

    }
 public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("inimigo"))
        {
            health--;
            if (health <= 0)
            {
                Die();
            }
        }
    }

   

    private void Die()
    {
        Debug.Log("Base destruída!");
        SceneManager.LoadScene(cenaGameOver);
    }
    
}
