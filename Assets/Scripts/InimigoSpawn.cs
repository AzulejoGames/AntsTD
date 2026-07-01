
using UnityEngine;
using System.Collections; // Necessário para Coroutines 

public class InimigoSpawn : MonoBehaviour
{
    // --- Início do Padrão Singleton --- 
    public static InimigoSpawn Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }
    // --- Fim do Padrão Singleton --- 

    [Header("Configurações do Spawner")]
    public GameObject targetPrefab; // Arraste o Prefab do Alvo aqui 
    public Transform[] spawnPoints; // Arraste todos os SpawnPoints aqui 

    void Start()
    {
        if (targetPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogError("TargetSpawner não configurado!");
            return;
        }

        // --- Spawn Inicial ---
        // Cria um alvo em cada ponto de spawn no início 
        foreach (Transform point in spawnPoints)
        {
            Instantiate(targetPrefab, point.position, Quaternion.identity);
        }
    }

    // Método PÚBLICO que o alvo chamará antes de morrer 
    public void TargetWasDestroyed()
    {
        // Usa uma Coroutine para o respawn não ser instantâneo 
        StartCoroutine(RespawnTargetCoroutine());
    }

    IEnumerator RespawnTargetCoroutine()
    {
        yield return new WaitForSeconds(1f); // Espera 1 segundo]

        // Sorteia um novo local da nossa lista 
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Cria o novo alvo 
        Instantiate(targetPrefab, spawnPoint.position, Quaternion.identity);
    }
}