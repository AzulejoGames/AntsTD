using UnityEngine;

public class SistemaDeConstrucao : MonoBehaviour
{
    public GameObject torreSelecionada;

    public void SelecionarTorre(GameObject torre)
    {
        torreSelecionada = torre;

        Debug.Log("Torre selecionada: " + torre.name);
    }
}
