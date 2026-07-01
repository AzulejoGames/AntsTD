using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class InputCabeca : MonoBehaviour
{
    public Vector2 PosicaoInput { get; private set; }
    public static event Action OnContatoIniciado;
    public static event Action OnContatoFinalizado;

    private CerebroToque controls;
    private void Awake()
    {
        controls = new CerebroToque(); // instancia a classe de controle

        controls.Toque.PrimeiraPos.performed += ctx => PosicaoInput = ctx.ReadValue<Vector2>(); // guarda quando o ponteiro se move, (cordenadas V2)

        controls.Toque.PrimeiraPos.canceled += ctx => PosicaoInput = Vector2.zero; // Se o toque sumir, resetamos a posição para zero
        controls.Toque.PrimeiroContato.started += _ => OnContatoIniciado?.Invoke(); // quando dedo encosta na tela, avisamos
        controls.Toque.PrimeiroContato.canceled += _ => OnContatoFinalizado?.Invoke(); // quando dedo  levanta da telaa, avisamos o fim



    }

    private void OnEnable() => controls.Toque.Enable(); // Obrigatorio o mapa de ações para escutar o hardware
    private void OnDisable() => controls.Toque.Disable(); // desabilitar para economizar bateria e evitar erros de memoria


 
}
