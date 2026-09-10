using UnityEngine;

public class DadosFormigas : MonoBehaviour
{
    [Tooltip("custo necessário para colocar torre")]
        [SerializeField] private int custo = 1;
        public int Custo => custo; 
}
