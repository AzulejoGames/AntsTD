
using UnityEngine;
using System.Collections;
using UnityEngine.VFX; 

public class InimigoSpawn : MonoBehaviour
{
    [Tooltip("Pontos de movimento do inimigo")]
      [SerializeField] private Transform pontoI;
     [SerializeField] private Transform pontoF;


     [Header("Prefab do inimigo a ser instanciado")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private int Maximo = 5;
    [SerializeField] private int minimo = 0;
    public float timeToSpawn = 3.0f;
    public Transform spawnPoint;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            CriarObjeto();
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
    private void CriarObjeto()
    {
       
       Debug.Log($"Objeto gerador: '{gameObject.name}' | PontoF está: {pontoF}", gameObject);
        if (minimo >= Maximo)
        {
            Debug.Log("LimiteAtingido");
            return;
        }


        minimo++;

        Vector2 spawnPosition = spawnPoint.position;
        Quaternion spawnRotation = prefab.transform.rotation;
        

        GameObject novoInimigo = Instantiate(prefab, spawnPosition, spawnRotation);
        EnemyDirection enemyDirection = novoInimigo.GetComponent<EnemyDirection>();
        if (enemyDirection != null)
        {
            enemyDirection.ConfigurarPontos(pontoI, pontoF);
        }
    }
    public void ReduzirContador()
    {
        if (minimo > 0)
        { minimo--; }
    }
}