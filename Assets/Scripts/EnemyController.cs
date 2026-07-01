using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform pontoI;
    [SerializeField] private Transform pontoF;
    public GameObject inimigo;


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

        // Lógica de Movimento
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); 

    }
    void OnTriggerEnter2D(Collider2D colidiu)
    {
        if (colidiu.CompareTag("base"))
        {
            Destroy(gameObject);
            Debug.Log("Destruiu um inimigo");
        }
    }
}
