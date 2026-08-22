using UnityEngine;
using UnityEngine.SceneManagement;
public class BaseHealth : MonoBehaviour
{
    [SerializeField] private string cenaGameOver = "GameOver";
    [SerializeField] private int health = 10;

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
