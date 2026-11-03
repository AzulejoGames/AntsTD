using UnityEngine;

public class SistemaDeConstrucao : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private PopupManager popupManager;
    private GameObject torreSelecionada;
    private InputCabeca inputCabeca;
    private BaseHealth baseHealth;
    [SerializeField] private LayerMask camadaTorres;
    [SerializeField] private LayerMask camadaparede;
    [SerializeField] private float raioColisao = 0.4f;
    private Vector3 ultimaPosicao;
    private void Awake()
    {
        inputCabeca = FindFirstObjectByType<InputCabeca>();
        baseHealth = FindFirstObjectByType<BaseHealth>();

    }
  

    public void SelecionarTorre(GameObject torre)
    {
        torreSelecionada = torre;

        if (popupManager != null)
        {
            popupManager.MostrarAviso("Posicione a torre!", new Color(0.2f, 0.8f, 0.2f, 0.9f));
        }


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
 
    DadosFormigas dadosFormigas = torreSelecionada.GetComponent<DadosFormigas>();
    if( dadosFormigas == null)
    {
        Debug.LogError("O prefab da torre não possui o componente DadosFormigas.");
        return;
    }
    int custo = dadosFormigas.Custo;

    if (baseHealth.pontos < custo)
    {
        Debug.Log("Pontos insuficientes!");
            if (popupManager != null)
                popupManager.MostrarAviso("Pontos insuficientes!", Color.red);
            return;
    }

   Ray raio = cam.ScreenPointToRay(inputCabeca.PosicaoInput);

Plane plano = new Plane(Vector3.forward, Vector3.zero);

if (!plano.Raycast(raio, out float distancia))
    return;

Vector3 posicao = raio.GetPoint(distancia);
posicao.z = 0f;

    ultimaPosicao = posicao;

        // Verifica se há colisão com outra torre na posição desejada

        Collider2D[] colisores = Physics2D.OverlapCircleAll(posicao,raioColisao,camadaTorres);

        if (colisores.Length > 0)
        {
            Debug.Log("Muito perto de outra torre!");
            if (popupManager != null)
                popupManager.MostrarAviso("Muito perto de outra torre!", Color.red);
            return;
        }
  Collider2D[] bloqueios = Physics2D.OverlapCircleAll(
    posicao,
    raioColisao,
    camadaparede
);

if (bloqueios.Length > 0)
{
    Debug.Log("Local proibido!");
            if (popupManager != null)
                popupManager.MostrarAviso("Local proibido para construção!", Color.red);
            return;
}
        GameObject novaTorre = Instantiate( torreSelecionada,posicao,Quaternion.identity);

    baseHealth.pontos -= custo;
        if (popupManager != null)
            popupManager.MostrarAviso("Torre construída com sucesso!", Color.green);

        torreSelecionada = null;
    }  
   private void OnDrawGizmos()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(ultimaPosicao, raioColisao);
}
}