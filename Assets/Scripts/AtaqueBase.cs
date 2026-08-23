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
            alvo = null;
            Debug.Log("Alvo perdido: ");
        }
    }
  
}
