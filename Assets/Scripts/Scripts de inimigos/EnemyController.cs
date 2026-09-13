using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyDirection enemyDirection;
    private GameManager gameManager;
    [SerializeField] private Animator animator;

    [Tooltip("vidas do inimigo")]
    [SerializeField] private int vidas = 3;
    public int pontosGanhos = 10;
    [Tooltip("tempo de morte do inimigo")]
    [SerializeField] private float tempoMorte = 1f;

    [Tooltip("Arraste o objeto com o BaseHealth para cá ou deixe o script encontrar na cena")]
    public BaseHealth baseAlvo;

    void Start()
    {
       
        gameManager = FindFirstObjectByType<GameManager>();
        animator = GetComponent<Animator>();
        enemyDirection = GetComponent<EnemyDirection>();
  
        if (baseAlvo == null)
        {
            baseAlvo = FindFirstObjectByType<BaseHealth>();
        }
    }

    public void TakeDamage(int damage)
    {
         Debug.Log("INIMIGO RECEBEU DANO: " + damage + " | VIDAS ANTES: " + vidas);

    vidas -= damage;

    Debug.Log("VIDAS AGORA: " + vidas);
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

        GetComponent<Collider2D>().enabled = false;
       if(enemyDirection != null)
        {
            enemyDirection.enabled = false; // Desativa o script EnemyDirection
        }

        

        this.enabled = false;

        animator.SetBool("Morreu", true);
        Destroy(gameObject, tempoMorte);
    }
}