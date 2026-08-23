using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class BaseHealth : MonoBehaviour
{
    
    
    
    [Header("Configurações da base")]
    
    [SerializeField] private int health = 10;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private string cenaGameOver = "GameOver";
    public void  LateUpdate()
    {
        healthText.text = " vidas: " + health.ToString();
        
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
