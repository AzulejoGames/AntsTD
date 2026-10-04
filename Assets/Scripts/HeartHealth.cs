using UnityEngine;

public class HeartHealth : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator=GetComponent<Animator>();
    }
    public void TakeDamage()
    {

        if (animator != null)
        {
            Debug.Log("TakeDamage called");
            animator.SetTrigger("Hurt");
        }


    }

}
