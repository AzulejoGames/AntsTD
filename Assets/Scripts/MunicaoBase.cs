using UnityEngine;


public class MunicaoBase : MonoBehaviour
{
    public GameObject ela;
    void OnTriggerEnter2D(Collider2D colidiu)
    {
        if (colidiu.CompareTag("enemy"))
        {
            Destroy(ela);
            Debug.Log("morre");
        }
    }
}
