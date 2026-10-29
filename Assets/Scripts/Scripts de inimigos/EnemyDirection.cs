using UnityEngine;

public class EnemyDirection : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform[] pontosCaminho; // Suporta 2 ou mais pontos!
    [SerializeField] private AudioSource somPassoI;
    private int indicePontoAtual = 0;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Método para receber o array de pontos do Spawner
    public void ConfigurarPontos(Transform[] novosPontos)
    {
        pontosCaminho = novosPontos;
        indicePontoAtual = 0;
        ValidarPontos();
    }

    // Sobrecarga mantida para compatibilidade (caso envie só inicio e fim)
    public void ConfigurarPontos(Transform pontoInicial, Transform pontoFinal)
    {
        pontosCaminho = new Transform[] { pontoInicial, pontoFinal };
        indicePontoAtual = 0;
        ValidarPontos();
    }

    private void ValidarPontos()
    {
        if (pontosCaminho == null || pontosCaminho.Length == 0)
        {
            Debug.LogError("Nenhum ponto de caminho foi atribuído ao EnemyDirection!", gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (rb == null ||
            pontosCaminho == null ||
            indicePontoAtual >= pontosCaminho.Length)
        {
            return;
        }

        Transform pontoAlvo = pontosCaminho[indicePontoAtual];

        if (pontoAlvo == null)
            return;

        Vector2 direcao =
            ((Vector2)pontoAlvo.position - rb.position).normalized;

        float angulo =
            Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

        rb.MoveRotation(angulo - 90f);

        rb.MovePosition(
            rb.position +
            direcao * moveSpeed * Time.fixedDeltaTime
        );

        if (somPassoI != null && !somPassoI.isPlaying)
        {
            somPassoI.Play();
        }

        if (Vector2.Distance(rb.position, pontoAlvo.position) < 0.1f)
        {
            indicePontoAtual++;

            if (indicePontoAtual >= pontosCaminho.Length)
            {
                ChegouAoFinal();

                if (somPassoI != null)
                    somPassoI.Stop();
            }
        }
    }

    private void ChegouAoFinal()
    {
        Debug.Log("Inimigo chegou no ponto final do caminho!");
        // AQUI: Lógica de causar dano na base e se destruir
        Destroy(gameObject);
    }
}