using UnityEngine;

public class ArrastarComEventos : MonoBehaviour
{
    private Camera cam;
    private InputCabeca inputCabeca;
    private AtaqueBase ataqueBase;
    private Torre torre;
    private bool estaArrastando = true;

    private void Awake()
    {
        cam = Camera.main;
        inputCabeca = FindFirstObjectByType<InputCabeca>();

        ataqueBase = FindFirstObjectByType<AtaqueBase>();
        torre = FindFirstObjectByType<Torre>();
    
        if (ataqueBase != null)
        {
           ataqueBase.enabled = false;
        }
        if (torre != null)
        {
            torre.enabled = false;
        }
       
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

        if (ataqueBase != null)
        {
            ataqueBase.enabled = true;
        }
        if (torre != null)
        {
            torre.enabled = true;
        }
       

        Debug.Log("Torre posicionada!");
    }
}