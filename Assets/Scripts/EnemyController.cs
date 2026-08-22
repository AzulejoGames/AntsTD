using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform pontoI;
    [SerializeField] private Transform pontoF;
    public GameObject inimigo;
    [Tooltip("vidas do inimigo")]
    [SerializeField] private int vidas = 3;


    public void TakeDamage(int damage)
    {
        vidas -= damage;
        if (vidas <= 0)
        {
            Die();
        }
    }
    void Start()
    { }

    public void onEnimy()
    {
        if (inimigo == null)
        {
            Debug.LogWarning("Inimigo nao configurado");
        }
    }

    // Update is called once per frame
    void Update()
    {

       
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); 
      
    }
    void OnTriggerEnter2D(Collider2D colidiu)
    {
        if (colidiu.CompareTag("base"))
        {
            Debug.Log("Inimigo chegou na base");
            Destroy(gameObject);
        
        }
    }
   void Die()
    {
        Destroy(gameObject);
        Debug.Log("Inimigo morreu");
    }

}
