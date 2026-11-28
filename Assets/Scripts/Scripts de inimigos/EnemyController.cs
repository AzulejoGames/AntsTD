using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NectarRecebe nectar;
    [SerializeField] private DialogueController dialogueController;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioSource audioDanoI;

    EnemyDirection enemyDirection;
    private GameManager gameManager;
    private Animator animator;

    private Color corOriginal;

    [SerializeField] private Color corDano = Color.red;
    [SerializeField] private float duracaoEfeito = 0.2f;
    [SerializeField, Range(0f, 1f)] private float intensidadeDano = 0.5f;

    [Header("Tremor da Câmera")]
    [SerializeField] private float shakeDanoIntensidade = 0.15f;
    [SerializeField] private float shakeDanoDuracao = 0.12f;
    [SerializeField] private float shakeMorteIntensidade = 0.6f;
    [SerializeField] private float shakeMorteDuracao = 0.5f;

    [Tooltip("vidas do inimigo")]
    [SerializeField] private int vidas = 3;

    public int pontosGanhos = 10;

    [Tooltip("tempo de morte do inimigo")]
    [SerializeField] private float tempoMorte = 1f;

    [Tooltip("Arraste o objeto com o BaseHealth para cá ou deixe o script encontrar na cena")]
    public BaseHealth baseAlvo;

    private bool morreu = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        corOriginal = spriteRenderer.color;

        gameManager = FindFirstObjectByType<GameManager>();
        animator = GetComponent<Animator>();
        enemyDirection = GetComponent<EnemyDirection>();

        if (nectar == null)
            nectar = FindFirstObjectByType<NectarRecebe>();

        if (baseAlvo == null)
            baseAlvo = FindFirstObjectByType<BaseHealth>();

        // Encontra o controlador do tutorial
        if (dialogueController == null)
            dialogueController = FindFirstObjectByType<DialogueController>();
    }

    public void TakeDamage(int damage)
    {
        if (morreu)
            return;

      

        vidas -= damage;
        CameraShake.Instance?.Shake(shakeDanoIntensidade, shakeDanoDuracao);
        if (audioDanoI != null)
            audioDanoI.PlayOneShot(audioDanoI.clip);

        StartCoroutine(EfeitoDanoCouroutine());

       

        if (vidas <= 0)
            Die();
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
           
            Destroy(gameObject);
        }
    }

    void Die()
    {
        // Evita registrar a mesma morte mais de uma vez
        if (morreu)
            return;

        morreu = true;
        CameraShake.Instance?.Shake(shakeMorteIntensidade, shakeMorteDuracao);
        if (audioDanoI != null)
            audioDanoI.PlayOneShot(audioDanoI.clip);

        if (baseAlvo != null)
        {
            baseAlvo.pontos += pontosGanhos;

            if (nectar)
                nectar.Recebeu();

           
        }
        else
        {
           
        }

        if (gameManager != null)
            gameManager.InimigosCaiu();

        // CONFIRMA A MORTE PARA O TUTORIAL
        if (dialogueController != null)
            dialogueController.ConfirmarInimigoDerrotado();

        Collider2D colisor = GetComponent<Collider2D>();

        if (colisor != null)
            colisor.enabled = false;

        if (enemyDirection != null)
            enemyDirection.enabled = false;

        this.enabled = false;

        animator.SetBool("Morreu", true);

        Destroy(gameObject, tempoMorte);
    }
}