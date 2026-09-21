using UnityEngine;

public class ArrastarComEventos : MonoBehaviour
{
    private Camera cam;
    private InputCabeca inputCabeca;

    private bool estaArrastando = true;

    private void Awake()
    {
        cam = Camera.main;
        inputCabeca = FindFirstObjectByType<InputCabeca>();
    }

    private void OnEnable()
    {
        InputCabeca.OnContatoFinalizado += PararArrasto;
    }

    private void OnDisable()
    {
        InputCabeca.OnContatoFinalizado -= PararArrasto;
    }

    private void Update()
    {
        if (!estaArrastando || inputCabeca == null)
            return;

        Vector2 posicaoTela = inputCabeca.PosicaoInput;

        Vector3 posicaoMundo = cam.ScreenToWorldPoint(
            new Vector3(
                posicaoTela.x,
                posicaoTela.y,
                -cam.transform.position.z
            )
        );

        posicaoMundo.z = 0f;

        transform.position = posicaoMundo;
    }

    private void PararArrasto()
    {
        estaArrastando = false;

        Debug.Log("Torre posicionada!");
    }
}