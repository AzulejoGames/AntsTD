 using UnityEngine;


public class AtaqueBase : MonoBehaviour
{
    
  [Tooltip("configuraçoes de formiga")]
//[SerializeField] private float tempoDeVida = 5f;
//public bool tempoAcabou = false;
 [SerializeField] private float moveSpeed = 3f;
 [SerializeField] private float distanciaDoAtaque = 0.3f;
 private Transform alvo;
 private Rigidbody2D rb;
 public int damage = 1;
 

 void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
private void FixedUpdate()
    {
        if (alvo == null)
            return;

        Vector2 direcao = (alvo.position - transform.position).normalized;

        float distancia = Vector2.Distance(transform.position, alvo.position);


        if (distancia <= distanciaDoAtaque)
        {
            EnemyController inimigo = alvo.GetComponent<EnemyController>();

            if (inimigo != null)
            {
                inimigo.TakeDamage(damage);
                Debug.Log("Inimigo atingido: " + damage + " de dano");
            }

            BaseDie();
            return;
        }

        
        rb.MovePosition(
            rb.position + direcao * moveSpeed * Time.fixedDeltaTime
        );

        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        rb.rotation = angulo - 90f;
    }
    private void Update()
    {
        //if(!tempoAcabou)
       // {
            
          //  tempoDeVida -= Time.deltaTime;
        //if (tempoDeVida <= 0f)
       // {
          //  tempoAcabou = true;
            
       // }
       // }
        
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.CompareTag("inimigo"))
        {
            alvo = collision.transform;
            Debug.Log("Alvo encontrado: ");
        }
        
    }

     private void OnTriggerExit2D(Collider2D collision)
     {
         if (collision.CompareTag("inimigo"))
         {
                alvo = null;
                Debug.Log("Alvo perdido");
         }
     }
    void BaseDie()
    {
        Destroy(gameObject);
        
    }

}
