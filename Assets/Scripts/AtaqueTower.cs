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

    // A Torre vai chamar esta função logo após criar a bala
    public void DefinirAlvo(Transform novoAlvo)
    {
        alvo = novoAlvo;
    }

    private void FixedUpdate()
    {
        // Se o inimigo morreu ou sumiu antes da bala chegar, destrói a bala
        if (alvo == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move a bala em direção ao alvo mantendo a física do Rigidbody2D
        Vector2 direcao = (alvo.position - transform.position).normalized;
        rb.MovePosition(rb.position + direcao * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se colidiu especificamente com o Inimigo
        if (collision.CompareTag("inimigo"))
        {
            EnemyController inimigo = collision.GetComponent<EnemyController>();

            if (inimigo != null)
            {
                inimigo.TakeDamage(damage);
                Debug.Log("Inimigo atingido: " + damage + " de dano");
            }

            // Destrói o projétil após causar o dano
            Destroy(gameObject);
        }
    }
}