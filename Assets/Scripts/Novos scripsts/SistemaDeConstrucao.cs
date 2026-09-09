using UnityEngine;

public class SistemaDeConstrucao : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private GameObject torreSelecionada;
    private InputCabeca inputCabeca;

    private void Awake()
    {
        inputCabeca = FindFirstObjectByType<InputCabeca>();
    }

    public void SelecionarTorre(GameObject torre)
    {
        torreSelecionada = torre;

        Debug.Log("Torre selecionada: " + torre.name);
    }

    private void OnEnable()
    {
        InputCabeca.OnContatoIniciado += IniciarConstrucao;
    }

    private void OnDisable()
    {
        InputCabeca.OnContatoIniciado -= IniciarConstrucao;
    }

    private void IniciarConstrucao()
    {
        if (torreSelecionada == null)
            return;

        Vector3 posicao = cam.ScreenToWorldPoint(inputCabeca.PosicaoInput);

        posicao.z = 0;

        Debug.Log("Torre colocada em: " + posicao);

        Instantiate(torreSelecionada, posicao, Quaternion.identity);

        torreSelecionada = null;
    }
}   