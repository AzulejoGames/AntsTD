 using UnityEngine;


public class AtaqueBase : MonoBehaviour
{
  
 [SerializeField] private float moveSpeed = 3f;
 
 private Transform alvo;
 private Rigidbody2D rb;
 public int damage = 1;
 

 void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
private void FixedUpdate()
    {
        if (alvo != null)
        {
           Vector2 direcao = (alvo.position - transform.position).normalized;
        rb.MovePosition(rb.position + direcao * moveSpeed * Time.fixedDeltaTime);


        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            
            // Se o sprite original apontar para BAICHO (como no seu vídeo), adicione +90f.
            // Se apontar para CIMA, subtraia -90f.
            rb.rotation = angulo - 90f; 

            // Movimento
            rb.MovePosition(rb.position + direcao * moveSpeed * Time.fixedDeltaTime);
        }
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.CompareTag("inimigo"))
        {
            alvo = collision.transform;
            Debug.Log("Alvo encontrado: ");
        }
         EnemyController inimigo = collision.GetComponent<EnemyController>();
            if (inimigo != null)
            {
                inimigo.TakeDamage(damage);
                Debug.Log("Inimigo atingido: " + damage + " de dano");
            }
    }

     private void OnTriggerExit2D(Collider2D collision)
     {
         if (collision.CompareTag("inimigo"))
         {
            //Destroy(gameObject);
         }
     }

}
