using UnityEngine;
using UnityEngine.UI;
public class NectarRecebe : MonoBehaviour
{
    
    [SerializeField] private Image minhaImagem;
    [SerializeField] private Color corNova = Color.yellow;

    private void Start()
    {
        if (minhaImagem == null)
        {
            minhaImagem = GetComponent<Image>();
        }
    }
    public void Recebeu() 
    {
        if (minhaImagem != null)
        {
            minhaImagem.color = corNova;
        }
    }

}
