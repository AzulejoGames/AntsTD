using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("Regras do jogo")]
     [SerializeField] private int InimigosDerrotados = 5;
    [SerializeField] private string cenaVitoria = "Fase2";
    
  public void InimigosCaiu()
    {
        InimigosDerrotados--;
        Debug.Log("Inimigos restantes: " + InimigosDerrotados);
        if (InimigosDerrotados <= 0)
        {
           CondicaoDeVitoria();
        }
    }
    void CondicaoDeVitoria()
    {
        
        
        
            Debug.Log("Vitória!");
            SceneManager.LoadScene(cenaVitoria);
        
    }
}
