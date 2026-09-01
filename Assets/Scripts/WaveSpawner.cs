using System.Collections;
using UnityEngine;
using TMPro;
public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private TMP_Text WaveTimeText;
    [SerializeField] private TMP_Text quantidadeWaves;
    private bool CronometroATivo = true;
    // Estrutura para configurar cada onda individualmente no Inspector
    [System.Serializable]
    public class Wave
    {
        public string nomeDaOnda = "Onda 1";
        public GameObject prefabInimigo;  
       
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
    private float cronometroRegressa;

    void Start()
    {
        StartCoroutine(GerenciarOndas());
       

    }
   

    private IEnumerator GerenciarOndas()
    {
        while (indiceOndaAtual < ondas.Length)
        {
            cronometroRegressa = tempoEntreOndas;

            Debug.Log($"Aguardando {tempoEntreOndas}s para iniciar: {ondas[indiceOndaAtual].nomeDaOnda}");
            while (cronometroRegressa > 0) 
            {
                AtualizarUi();
                yield return new WaitForSeconds(1f);
                cronometroRegressa --;

              
            }
            cronometroRegressa = 0;
            AtualizarUi();

            // Inicia e aguarda a onda atual ser completamente gerada
            yield return StartCoroutine(SpawnarOnda(ondas[indiceOndaAtual]));

            indiceOndaAtual++;
        }

        quantidadeWaves.text = "Vitória! insetos foram detidos por agora";
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

    void AtualizarUi()
    {
        // Exibe o cronômetro sem casas decimais (números inteiros) para melhor leitura na UI
        WaveTimeText.text = $"Tempo entre Ondas: {Mathf.CeilToInt(cronometroRegressa)} segundos";
        if (quantidadeWaves != null) 
        {
            quantidadeWaves.text = "Round" + (indiceOndaAtual + 1) + "/" + ondas.Length;
        }
    }

}