using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameManager gameManager;

    [Tooltip("vidas do inimigo")]
    [SerializeField] private int vidas = 3;
    public int pontosGanhos = 10;

    [Tooltip("Arraste o objeto com o BaseHealth para cá ou deixe o script encontrar na cena")]
    public BaseHealth baseAlvo;

    void Start()
    {
       
        gameManager = FindFirstObjectByType<GameManager>();

  
        if (baseAlvo == null)
        {
            baseAlvo = FindFirstObjectByType<BaseHealth>();
        }
    }

    public void TakeDamage(int damage)
    {
        vidas -= damage;
        if (vidas <= 0)
        {
            Die();
        }
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
        
        if (baseAlvo != null)
        {
            baseAlvo.pontos += pontosGanhos;
            Debug.Log("Pontos adicionados à base. Pontos atuais: " + baseAlvo.pontos);
        }
        else
        {
            Debug.LogWarning("BaseHealth não encontrado pelo inimigo!");
        }

        if (gameManager != null)
        {
            gameManager.InimigosCaiu();
        }

        Debug.Log("Inimigo morreu");
        Destroy(gameObject);
    }
}