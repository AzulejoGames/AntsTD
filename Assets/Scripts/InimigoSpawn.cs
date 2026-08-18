
using UnityEngine;
using System.Collections;
using UnityEngine.VFX; // Necessário para Coroutines 

public class InimigoSpawn : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int Maximo = 5;
    [SerializeField] private int minimo = 0;
    public float timeToSpawn = 3.0f;

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
       
        if (minimo >= Maximo)
        {
            Debug.Log("LimiteAtingido");
            return;
        }


        minimo++;

        Vector2 spawnPosition = new Vector3(0f, 2f, 0f);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(prefab   );

    }
    public void ReduzirContador()
    {
        if (minimo > 0)
        { minimo--; }
    }
}