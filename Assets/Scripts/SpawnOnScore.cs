using UnityEngine;

public class SpawnOnScore : MonoBehaviour
{
    [Header("Configurações de Pontuação")]
    [Tooltip("Quantidade de pontos necessária para ativar o spawn.")]
    public int pontosNecessarios = 50;

    [Header("Prefabs e Referências")]
    [Tooltip("O objeto Y que será instanciado.")]
    public GameObject objetoYPrefab;

    [Tooltip("Referência para o script BaseHealth onde ficam os pontos.")]
    public BaseHealth baseHealth;

 private bool jaInstanciou = false; // Trava para evitar chamadas duplicadas
    private void Start()
    {
        // Se a baseHealth não foi arrastada no Inspector, procura na cena automaticamente
        if (baseHealth == null)
        {
            baseHealth = FindFirstObjectByType<BaseHealth>();
        }
    }

    private void Update()
    {
        if (jaInstanciou || baseHealth == null) return;

        VerificarPontuacao();
    }

    private void VerificarPontuacao()
    {
        // Garante que o script baseHealth foi encontrado antes de ler a variável
        if (baseHealth != null)
        {
            // Lê diretamente a variável 'pontos' de dentro do script BaseHealth
            if (baseHealth.pontos >= pontosNecessarios)
            {
                SpawnEPartir();
                Debug.Log("Objeto gerado com sucesso!");
            }
        }
        else
        {
            Debug.LogWarning("Script BaseHealth não encontrado na cena!", this);
        }
    }

    private void SpawnEPartir()
    {
        
        if (objetoYPrefab != null)
        {
            // Instancia o objeto Y na mesma posição e rotação deste objeto
            Instantiate(objetoYPrefab, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogWarning("Objeto Y Prefab não foi atribuído no Inspector!", this);
        }

        // Destrói o objeto atual que possui este script
        Destroy(gameObject);
    }
}