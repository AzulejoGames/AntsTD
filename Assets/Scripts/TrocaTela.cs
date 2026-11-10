using UnityEngine;
using UnityEngine.SceneManagement;
public class TrocaTela : MonoBehaviour
{
    [Header("Configurações de Som")]
    public AudioSource audioSource;
    public AudioClip somClique;

    public void MudarCena(string nomeDaCena)
    {
        if (audioSource != null && somClique != null)
        {
            audioSource.PlayOneShot(somClique);
        }
        SceneManager.LoadScene(nomeDaCena);
    }
   
}
