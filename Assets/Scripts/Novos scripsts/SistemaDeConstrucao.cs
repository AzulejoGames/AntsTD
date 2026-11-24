using UnityEngine;
using System.Collections.Generic;
public class SistemaDeConstrucao : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private PopupManager popupManager;
    [SerializeField] private DialogueController dialogueController;

    private GameObject torreSelecionada;
    private InputCabeca inputCabeca;
    private BaseHealth baseHealth;

    [SerializeField] private LayerMask camadaTorres;
    [SerializeField] private LayerMask camadaparede;
    [SerializeField] private float raioColisao = 0.4f;
    [SerializeField] private int limiteTorres = 10;

    private List<GameObject> torresConstruidas = new List<GameObject>();

    private Vector3 ultimaPosicao;

    private void Awake()
    {
        inputCabeca = FindFirstObjectByType<InputCabeca>();
        baseHealth = FindFirstObjectByType<BaseHealth>();

        if (dialogueController == null)
            dialogueController = FindFirstObjectByType<DialogueController>();
    }

    public void SelecionarTorre(GameObject torre)
    {
        torreSelecionada = torre;

        if (popupManager != null)
        {
            popupManager.MostrarAviso(
                "Posicione a torre!",
                new Color(0.2f, 0.8f, 0.2f, 0.9f)
            );
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
        // Remove referências de torres que já foram destruídas
        torresConstruidas.RemoveAll(torre => torre == null);

        // Verifica o limite de construções
        if (torresConstruidas.Count >= limiteTorres)
        {
            Debug.Log("Limite de torres atingido!");

            if (popupManager != null)
                popupManager.MostrarAviso("Limite de torres atingido!", Color.red);

            return;
        }
        DadosFormigas dadosFormigas =
            torreSelecionada.GetComponent<DadosFormigas>();

        if (dadosFormigas == null)
        {
            
            return;
        }

        int custo = dadosFormigas.Custo;

        if (baseHealth.pontos < custo)
        {
            

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

        // Verifica colisão com outras torres
        Collider2D[] colisores = Physics2D.OverlapCircleAll(
            posicao,
            raioColisao,
            camadaTorres
        );

        if (colisores.Length > 0)
        {
           
            if (popupManager != null)
                popupManager.MostrarAviso("Muito perto de outra torre!", Color.red);

            return;
        }

        // Verifica locais proibidos
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

        // Constrói a torre
        GameObject novaTorre = Instantiate(
            torreSelecionada,
            posicao,
            Quaternion.identity
        );
        torresConstruidas.Add(novaTorre);

        baseHealth.pontos -= custo;

        if (popupManager != null)
            popupManager.MostrarAviso("Torre construída com sucesso!", Color.green);

        // CONFIRMA A CONSTRUÇÃO PARA O TUTORIAL
        if (novaTorre != null && dialogueController != null)
        {
            dialogueController.ConfirmarTorreColocada();
        }

        torreSelecionada = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ultimaPosicao, raioColisao);
    }
}