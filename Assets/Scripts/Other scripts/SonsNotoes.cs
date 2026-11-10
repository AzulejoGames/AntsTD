using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class SonsNotoes : MonoBehaviour    
{
    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    // Método chamado pelo botão
    public void TrocarDeCenaComSom(string nomeDaCena)
    {
        StartCoroutine(MudarCenaRotina(nomeDaCena));
    }

    private IEnumerator MudarCenaRotina(string nomeDaCena)
    {
        // Toca o som do clique
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);

            // Espera o tempo do áudio terminar (ou um tempo fixo, ex: 0.3 segundos)
            yield return new WaitForSeconds(clickSound.length);
        }

        // Carrega a nova cena
        SceneManager.LoadScene(nomeDaCena);
    }
}
