using UnityEngine;

public class EnemyDirection : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform[] pontosCaminho; // Suporta 2 ou mais pontos!

    private int indicePontoAtual = 0;

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

    void Update()
    {
        // Se não tiver pontos ou se já percorreu todos os pontos, interrompe o movimento
        if (pontosCaminho == null || indicePontoAtual >= pontosCaminho.Length) return;

        Transform pontoAlvo = pontosCaminho[indicePontoAtual];

        if (pontoAlvo == null) return;

        // Move o inimigo em direção ao ponto alvo atual
        transform.position = Vector2.MoveTowards(transform.position, pontoAlvo.position, moveSpeed * Time.deltaTime);

        // Verifica se chegou próximo do ponto alvo atual
        if (Vector2.Distance(transform.position, pontoAlvo.position) < 0.1f)
        {
            indicePontoAtual++; // Avança para o próximo ponto da lista

            // Se o novo índice for igual ou maior que a quantidade de pontos, chegou ao final da rota
            if (indicePontoAtual >= pontosCaminho.Length)
            {
                ChegouAoFinal();
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