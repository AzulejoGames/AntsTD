using UnityEngine;

public class AtaqueTower : MonoBehaviour
{
  
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private int damage = 1;

    private Transform alvo;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void DefinirAlvo(Transform novoAlvo)
    {
        alvo = novoAlvo;
    }

    private void FixedUpdate()
    {
        
        if (alvo == null)
        {
            Destroy(gameObject);
            return;
        }

      
        Vector2 direcao = (alvo.position - transform.position).normalized;
        rb.MovePosition(rb.position + direcao * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("inimigo"))
        {
            EnemyController inimigo = collision.GetComponent<EnemyController>();

            if (inimigo != null)
            {
                inimigo.TakeDamage(damage);
                Debug.Log("Inimigo atingido: " + damage + " de dano");
            }

            
            Destroy(gameObject);
        }
    }
}