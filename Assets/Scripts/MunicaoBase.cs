using UnityEngine;


public class MunicaoBase : MonoBehaviour
{
    public GameObject gameObject;
    void OnTriggerEnter2D(Collider2D colidiu)
    {
        if (colidiu.CompareTag("enemy"))
        {
            Destroy(gameObject);
            Debug.Log("morre");
        }
    }
}
