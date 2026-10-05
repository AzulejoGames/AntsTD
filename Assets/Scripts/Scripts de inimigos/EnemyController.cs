using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NectarRecebe nectar;
    private SpriteRenderer spriteRenderer;

    EnemyDirection enemyDirection;
    private GameManager gameManager;
    private Animator animator;

    private Color corOriginal;
    [SerializeField] private Color corDano = Color.red;
    [SerializeField] private float duracaoEfeito = 0.2f;
    [SerializeField, Range(0f, 1f)] private float intensidadeDano = 0.5f;

    [Tooltip("vidas do inimigo")]
    [SerializeField] private int vidas = 3;
    public int pontosGanhos = 10;
    [Tooltip("tempo de morte do inimigo")]
    [SerializeField] private float tempoMorte = 1f;

    [Tooltip("Arraste o objeto com o BaseHealth para cá ou deixe o script encontrar na cena")]
    public BaseHealth baseAlvo;

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        corOriginal = spriteRenderer.color;
        gameManager = FindFirstObjectByType<GameManager>();
        animator = GetComponent<Animator>();
        enemyDirection = GetComponent<EnemyDirection>();

        if (nectar == null)
        {
            nectar = FindFirstObjectByType<NectarRecebe>();
        }

        if (baseAlvo == null)
        {
            baseAlvo = FindFirstObjectByType<BaseHealth>();
        }
    }

    public void TakeDamage(int damage)
    {
         Debug.Log("INIMIGO RECEBEU DANO: " + damage + " | VIDAS ANTES: " + vidas);

    vidas -= damage;
    StartCoroutine(EfeitoDanoCouroutine());

    Debug.Log("VIDAS AGORA: " + vidas);
        
        if (vidas <= 0)
        {
            Die();
        }
    }
    IEnumerator EfeitoDanoCouroutine()
    {
        spriteRenderer.color = Color.Lerp(corOriginal, corDano, intensidadeDano);
        yield return new WaitForSeconds(duracaoEfeito);

      
        spriteRenderer.color = corOriginal;


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
            if (nectar) 
            {
                nectar.Recebeu();
            }
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