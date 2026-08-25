using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // Estrutura para configurar cada onda individualmente no Inspector
    [System.Serializable]
    public class Wave
    {
        public string nomeDaOnda = "Onda 1";
        public GameObject prefabInimigo;  // Qual inimigo vai nascer nesta onda
       
       public int quantidade = 5;         // Quantidade de inimigos na onda
       public float taxaDeSpawn = 1f;     // Inimigos por segundo
    }

    [Header("Configurações das Ondas")]
    [SerializeField] private Wave[] ondas;              // Array com todas as ondas
    [SerializeField] private float tempoEntreOndas = 5f; // Pausa antes de iniciar a próxima onda

    [Header("Pontos do Caminho")]
    [SerializeField] private Transform spawnPoint;      // Onde o inimigo nasce na cena
    [SerializeField] private Transform[] pontosCaminho; // Lista de waypoints (2 ou mais pontos!)

    private int indiceOndaAtual = 0;

    void Start()
    {
        StartCoroutine(GerenciarOndas());
    }

    private IEnumerator GerenciarOndas()
    {
        while (indiceOndaAtual < ondas.Length)
        {
            Debug.Log($"Aguardando {tempoEntreOndas}s para iniciar: {ondas[indiceOndaAtual].nomeDaOnda}");
            yield return new WaitForSeconds(tempoEntreOndas);

            // Inicia e aguarda a onda atual ser completamente gerada
            yield return StartCoroutine(SpawnarOnda(ondas[indiceOndaAtual]));

            indiceOndaAtual++;
        }

        Debug.Log("Todas as ondas foram finalizadas!");
    }

    private IEnumerator SpawnarOnda(Wave onda)
    {
        Debug.Log($"Iniciando: {onda.nomeDaOnda}");

        for (int i = 0; i < onda.quantidade; i++)
        {
            SpawnarInimigo(onda.prefabInimigo);
            yield return new WaitForSeconds(1f / onda.taxaDeSpawn);
        }
    }

    private void SpawnarInimigo(GameObject prefab)
    {
        // Define a posição de nascimento (usa a posição do spawner se spawnPoint não for atribuído)
        Vector3 posicao = spawnPoint != null ? spawnPoint.position : transform.position;
        Quaternion rotacao = prefab.transform.rotation;

        // Instancia o inimigo
        GameObject novoInimigo = Instantiate(prefab, posicao, rotacao);

        // Envia o array de pontos completo para o script EnemyDirection
        EnemyDirection enemyDirection = novoInimigo.GetComponent<EnemyDirection>();
        if (enemyDirection != null)
        {
            enemyDirection.ConfigurarPontos(pontosCaminho);
        }
        else
        {
            Debug.LogWarning("O inimigo instanciado não possui o componente 'EnemyDirection'!", novoInimigo);
        }
    }
}