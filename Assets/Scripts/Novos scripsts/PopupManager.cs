using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.UI;
public class PopupManager : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject popupPanel;
    public TextMeshProUGUI popupText;
    
    public float tempoVisivel = 1f;
    private float contadorTempo;
    void Start()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);
    }

   public void MostrarAviso(string mensagem, Color corTexto)
    {
        if (popupPanel != null)
        {
            if (popupText != null)
            {
                popupText.text = mensagem;
                popupText.color = corTexto; // Muda a cor das letras dinamicamente
            }

            popupPanel.SetActive(true);
            contadorTempo = tempoVisivel;
        }
    }
    void Update()
    {
        if (popupPanel != null && popupPanel.activeSelf)
        {
            contadorTempo -= Time.deltaTime;
            if (contadorTempo <= 0)
            {
                popupPanel.SetActive(false);
            }
        }
    }
}
