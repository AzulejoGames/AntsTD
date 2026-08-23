using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
   
   
    private GameManager gameManager;
    [Tooltip("vidas do inimigo")]
    [SerializeField] private int vidas = 3;


    public void TakeDamage(int damage)
    {
        vidas -= damage;
        if (vidas <= 0)
        {
            Die();
        }
    }
    void Start()
    {
        
         gameManager = FindObjectOfType<GameManager>();
    }

    

    // Update is called once per frame
    void Update()
    {

       
   
    }
    void OnTriggerEnter2D(Collider2D colidiu)
    {
        if (colidiu.CompareTag("base"))
        {
            Debug.Log("Inimigo chegou na base");
            Destroy(gameObject);
        
        }
    }
   void Die()
    {
        gameManager.InimigosCaiu();
        Destroy(gameObject);
        Debug.Log("Inimigo morreu");
        
    }

}
