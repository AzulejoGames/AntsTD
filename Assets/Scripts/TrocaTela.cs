
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

        // Salva a cena atual antes de trocar
        if (nomeDaCena == "GameOver")
        {
            string cenaAtual = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("UltimaCena", cenaAtual);
            PlayerPrefs.Save();

            Debug.Log("Última cena salva: " + cenaAtual);
        }

        SceneManager.LoadScene(nomeDaCena);
    }

    public void VoltarUltimaCena()
    {
        string ultimaCena = PlayerPrefs.GetString("UltimaCena", "");

        if (!string.IsNullOrEmpty(ultimaCena))
        {
            
            SceneManager.LoadScene(ultimaCena);
        }
      
    }
}
