using System.Drawing;
using UnityEngine;
using System.Collections;
using UnityEngine.VFX;

public class SpawnDeFormigasOP : MonoBehaviour
{
    [Header("Prefab das formigas")]
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
            criarFormiga();
            yield return new WaitForSeconds(timeToSpawn);

        }
    }
    private void criarFormiga()
    {
        if (minimo >= Maximo)
        {
            Debug.Log("LimiteAtingido");
            return;
        }


        minimo++;

       Vector2 spawnPosition = spawnPoint.position;
        Quaternion spawnRotation = prefab.transform.rotation;


        GameObject novaFormiga = Instantiate(prefab, spawnPosition, spawnRotation);
      
      
    }
    public void ReduzirContador()
    {
        if (minimo > 0)
        { minimo--; }
    }
    void Update()
    {
        
    }
}
