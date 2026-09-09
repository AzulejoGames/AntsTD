using UnityEngine;

public class SistemaDeConstrucao : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private GameObject torreSelecionada;
    private InputCabeca inputCabeca;
    private BaseHealth baseHealth;

    private void Awake()
    {
        inputCabeca = FindFirstObjectByType<InputCabeca>();
        baseHealth = FindFirstObjectByType<BaseHealth>();

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

    AtaqueBase ataqueBase = torreSelecionada.GetComponent<AtaqueBase>();

    if (ataqueBase == null)
    {
        Debug.LogError("A torre não possui AtaqueBase.");
        return;
    }

    int custo = ataqueBase.Custo;

    if (baseHealth.pontos < custo)
    {
        Debug.Log("Pontos insuficientes!");
        Debug.Log("Pontos atuais: " + baseHealth.pontos);
        Debug.Log("Custo da torre: " + custo);
        return;
    }

    Vector3 posicao = cam.ScreenToWorldPoint(inputCabeca.PosicaoInput);

    posicao.z = 0f;

    Instantiate(
        torreSelecionada,
        posicao,
        Quaternion.identity
    );

    baseHealth.pontos -= custo;

    Debug.Log("Torre construída!");
    Debug.Log("Pontos restantes: " + baseHealth.pontos);

    torreSelecionada = null;
    }   
}