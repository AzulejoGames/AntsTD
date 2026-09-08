using UnityEngine;

[CreateAssetMenu(fileName = "NovaTorre", menuName = "AntsTD/Torre")]
public class TorresData : MonoBehaviour
{
    [Header("Informações")]
    public string nome;
    [TextArea]
    public string descricao;
    public int preco;

    [Header("Visual")]
    public Sprite icone;

    [Header("Prefab")]
    public GameObject prefab;
}
