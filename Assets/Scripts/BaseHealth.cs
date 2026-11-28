using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
public class BaseHealth : MonoBehaviour
{
    [SerializeField] private HeartHealth coracao;
    private SpriteRenderer spriteRenderer;
    private Color corOriginal;
    [SerializeField] private Color corDano = Color.red;
    [SerializeField] private float duracaoEfeito = 0.2f;
    [SerializeField, Range(0f, 1f)] private float intensidadeDano = 0.5f;

    [Header("Tremor da Câmera")]
    [SerializeField] private float shakeDanoIntensidade = 0.15f;
    [SerializeField] private float shakeDanoDuracao = 0.12f;
    [SerializeField] private float shakeMorteIntensidade = 0.6f;
    [SerializeField] private float shakeMorteDuracao = 0.5f;
    private Animator animator;
    [Header("Configurações da base")]
    
    [SerializeField] private int health = 10;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private string cenaGameOver = "GameOver";
    [SerializeField] private TMP_Text pontosText;
    public int pontos = 0;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        corOriginal = spriteRenderer.color;
        animator = GetComponent<Animator>();
    }
    public void  LateUpdate()
    {
        healthText.text = "  " + health.ToString();
        pontosText.text = " Nectar" + pontos.ToString();

    }
 public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("inimigo"))
        {
            health--;
            if (coracao)
            {
                coracao.TakeDamage();
            }
            StartCoroutine(EfeitoDanoCouroutine());
            if (health <= 0)
            {
                Die();
            }
            else
            {
                CameraShake.Instance?.Shake(shakeDanoIntensidade, shakeDanoDuracao);
            }
        }
    }
    IEnumerator EfeitoDanoCouroutine()
    {
        spriteRenderer.color = Color.Lerp(corOriginal, corDano, intensidadeDano);
        yield return new WaitForSeconds(duracaoEfeito);


        spriteRenderer.color = corOriginal;


    }



    private void Die()
    {
        CameraShake.Instance?.Shake(shakeMorteIntensidade, shakeMorteDuracao);
        PlayerPrefs.SetString("UltimaCena", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        StartCoroutine(GameOverCoroutine());
    }
    private IEnumerator GameOverCoroutine()
    {
        if (animator != null)
        {
            animator.SetBool("Morreu", true);
        }

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(cenaGameOver);
    }

}
