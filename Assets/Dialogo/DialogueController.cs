using UnityEngine;
using TMPro;
using System.Collections;
public class DialogueController : MonoBehaviour
{

    [Header("Animação de Texto")]
    public float typeDelay = 0.02f;
    private Coroutine typingCoroutine;

      [Header("Dados")]
    [SerializeField] private DialogueData dialogueData;

    [Header("UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;

    private int indiceAtual = 0;

    public void TypeText(string text)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeTextCoroutine(text));
    }

    IEnumerator TypeTextCoroutine(string text)
    {
        dialogueText.text = text;
        dialogueText.maxVisibleCharacters = 0;

        for (int i = 0; i <= text.Length; i++)
        {
            dialogueText.maxVisibleCharacters = i;
            yield return new WaitForSecondsRealtime(typeDelay);
        }
    }

    private void Start()
    {
       IniciarDialogo();
    }

    public void IniciarDialogo()
    {
        indiceAtual = 0;
        Time.timeScale = 0f; // Pausa o jogo
        dialoguePanel.SetActive(true);
        MostrarFala();
    }

    public void ProximaFala()
    {
        indiceAtual++;

        if (indiceAtual >= dialogueData.talkscrip.Count)
        {
            EncerrarDialogo();
            return;
        }

        MostrarFala();
    }

    private void MostrarFala()
    {
        Dialogue fala = dialogueData.talkscrip[indiceAtual];

        nameText.text = fala.name;
        dialogueText.text = fala.dialogueText;
        TypeText(fala.dialogueText);
    }

    private void EncerrarDialogo()
    {
        dialoguePanel.SetActive(false);
        Time.timeScale = 1f; // Retoma o jogo
    }
}
