using UnityEngine;

public class EnemyDirection : MonoBehaviour
{
    //public GameObject enemyPrefab;
     [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform pontoI;
    [SerializeField] private Transform pontoF;
  
    public void ConfigurarPontos(Transform pontoInicial, Transform pontoFinal)
    {
        pontoI = pontoInicial;
        pontoF = pontoFinal;
        onEnimy();
    }
   public void onEnimy()
    {
        if (pontoI == null)
        {
            Debug.LogError("Ponto inicial não atribuído no EnemyController.");
        }
        if (pontoF == null)
        {
            Debug.LogError("Ponto final não atribuído no EnemyController.");
        }
    }
    void Update()
    {
        if (pontoF == null)return;
        
         transform.position = Vector2.MoveTowards(transform.position, pontoF.position, moveSpeed * Time.deltaTime);
        
      if(Vector2.Distance(transform.position, pontoF.position) < 0.1f)
        {
            Debug.Log("Inimigo chegou no ponto final");
        }
    }
  
}
