using UnityEngine;

public class SistemaDeConstrucao : MonoBehaviour
{
    [SerializeField] private Camera cam;

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
        Debug.Log("Pontos atuais: " + baseHealth.pontos);
        Debug.Log("Custo da torre: " + custo);
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
    return;
}
        GameObject novaTorre = Instantiate( torreSelecionada,posicao,Quaternion.identity);

    baseHealth.pontos -= custo;

    Debug.Log("Torre construída!");
    Debug.Log("Pontos restantes: " + baseHealth.pontos);

    torreSelecionada = null;
    }  
   private void OnDrawGizmos()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(ultimaPosicao, raioColisao);
}
}