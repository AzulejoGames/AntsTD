using UnityEngine;

public class GeradorPorEvento : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int quantidadeMax = 10;
    [SerializeField]private int quantidadeNorm = 0;

    private Camera _cam;
    private InputCabeca _input;

    void Awake()
    {
        _cam = Camera.main;
        _input = FindFirstObjectByType<InputCabeca>();
    }

    // Este script só se interessa pelo MOMENTO INICIAL do toque.
    void OnEnable() => InputCabeca.OnContatoIniciado += CriarObjeto;
    void OnDisable() => InputCabeca.OnContatoIniciado -= CriarObjeto;

    private void CriarObjeto()
    {
        if (_input == null) return;
        if (quantidadeNorm >= quantidadeMax)
        {
            Debug.Log("LimiteAtingido");
            return;
        }

        // Pega a posição onde o dedo está agora.
        Vector2 screenPos = _input.PosicaoInput;
        Vector3 worldPos = _cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, _cam.nearClipPlane));
        worldPos.z = 0f;
        quantidadeNorm++;

        // Instancia o prefab e agenda sua destruição para não pesar na memória.
        GameObject clone = Instantiate(prefab, worldPos, Quaternion.identity);
        
    }
    public void ReduzirContador()
    {
        if(quantidadeNorm > 0)
        { quantidadeNorm--; }
    }

}
