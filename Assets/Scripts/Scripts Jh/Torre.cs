using UnityEngine;

public class Torre : MonoBehaviour
{
    [Header("Configurações da Torre")]
    [SerializeField] private float alcance = 4f;
    [SerializeField] private float tempoEntreTiros = 1f;
    [SerializeField] private LayerMask camadaInimigo;

    [Header("Referências")]
    [SerializeField] private GameObject prefabProjetil;
    [SerializeField] private Transform pontoDeDisparo;

    private Transform alvoAtual;
    private float temporizadorTiro = 0f;

    void Start()
    {
        InvokeRepeating(nameof(BuscarAlvo), 0f, 0.2f);
    }

    void Update()
    {
        temporizadorTiro -= Time.deltaTime;

        if (alvoAtual == null) return;

        if (temporizadorTiro <= 0f)
        {
            Atirar();
            temporizadorTiro = tempoEntreTiros;
        }
    }

    void BuscarAlvo()
    {
        Collider2D[] inimigosEncontrados = Physics2D.OverlapCircleAll(transform.position, alcance, camadaInimigo);

        float menorDistancia = Mathf.Infinity;
        Transform inimigoMaisProximo = null;

        foreach (Collider2D col in inimigosEncontrados)
        {
            float distancia = Vector2.Distance(transform.position, col.transform.position);

            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                inimigoMaisProximo = col.transform;
            }
        }

        alvoAtual = inimigoMaisProximo;
    }

    void Atirar()
    {
        Vector3 posicaoSaida = pontoDeDisparo != null ? pontoDeDisparo.position : transform.position;

        GameObject novaBala = Instantiate(prefabProjetil, posicaoSaida, Quaternion.identity);

        // AQUI: Usando o seu novo script AtaqueTower
        AtaqueTower scriptBala = novaBala.GetComponent<AtaqueTower>();

        if (scriptBala != null)
        {
            scriptBala.DefinirAlvo(alvoAtual);
        }
        else
        {
            Debug.LogError("O Prefab da bala NÃO tem o script 'AtaqueTower' anexado!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alcance);
    }
}