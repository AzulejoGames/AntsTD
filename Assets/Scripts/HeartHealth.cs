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
            animator.SetTrigger("Hurt");
        }


    }

}
