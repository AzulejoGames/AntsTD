using UnityEngine;


public class MunicaoBase : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D colidiu)
    {
        if (colidiu.CompareTag("enemy"))
        {
            Debug.Log("morre");
            Destroy(gameObject);
        }
    }
}
