using UnityEngine;
using UnityEngine.SceneManagement;
public class TrocaTela : MonoBehaviour
{
    public void MudarCena(string nomeDaCena)
    {
        SceneManager.LoadScene(nomeDaCena);
    }
}
