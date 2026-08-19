using UnityEngine;


public class AtaqueBase : MonoBehaviour
{
 [SerializeField] private float moveSpeed = 3f;
 [SerializeField] private string AlvoTag = "inimigo";
 private Transform alvo;
 private Rigidbody2D rb;

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
        if (collision.CompareTag(AlvoTag))
        {
            alvo = collision.transform;
            Debug.Log("Alvo encontrado: "); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(AlvoTag))
        {
            alvo = null;
            Debug.Log("Alvo perdido: ");
        }
    }
}
