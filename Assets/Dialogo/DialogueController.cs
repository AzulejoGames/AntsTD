using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    [Header("Animação de Texto")]
    public float typeDelay = 0.02f;

    private Coroutine typingCoroutine;
    private Coroutine tutorialCoroutine;
    private bool textoDigitando;

    [Header("Dados")]
    [SerializeField] private DialogueData dialogueData;

    [Header("UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;

    private int indiceAtual = 0;

    private bool aguardandoContinuar;
    private bool eventoConcluido;

    private TutorialStepType etapaAtual;

    private void Start()
    {
        IniciarDialogo();
    }

    public void IniciarDialogo()
    {
        if (tutorialCoroutine != null)
            StopCoroutine(tutorialCoroutine);

        indiceAtual = 0;
        Time.timeScale = 1f;

        tutorialCoroutine = StartCoroutine(ExecutarTutorial());
    }

    private IEnumerator ExecutarTutorial()
    {
        while (indiceAtual < dialogueData.talkscrip.Count)
        {
            Dialogue etapa = dialogueData.talkscrip[indiceAtual];

            etapaAtual = etapa.type;

            switch (etapa.type)
            {
                case TutorialStepType.Dialogue:

                    dialoguePanel.SetActive(true);
                    Time.timeScale = 0f;

                    nameText.text = etapa.name;

                    TypeText(etapa.dialogueText);

                    aguardandoContinuar = true;

                    while (aguardandoContinuar)
                        yield return null;

                    break;


                case TutorialStepType.Action:

                    dialoguePanel.SetActive(false);
                    Time.timeScale = 1f;

                    etapa.action?.Invoke();

                    yield return null;

                    break;


                case TutorialStepType.WaitForTowerPlacement:

                    dialoguePanel.SetActive(false);
                    Time.timeScale = 1f;

                    eventoConcluido = false;

                    while (!eventoConcluido)
                        yield return null;

                    break;


                case TutorialStepType.WaitForEnemyDefeated:

                    dialoguePanel.SetActive(false);
                    Time.timeScale = 1f;

                    eventoConcluido = false;

                    while (!eventoConcluido)
                        yield return null;

                    break;
            }

            indiceAtual++;
        }

        EncerrarDialogo();
    }

    // Chamado pelo botão de continuar
    public void ProximaFala()
    {
        // Primeiro clique: termina a animação do texto
        if (textoDigitando)
        {
            StopCoroutine(typingCoroutine);

            typingCoroutine = null;
            textoDigitando = false;

            dialogueText.maxVisibleCharacters =
                dialogueText.textInfo.characterCount;

            return;
        }

        // Segundo clique: avança a etapa
        aguardandoContinuar = false;
    }

    public void TypeText(string text)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueText.text = text;
        dialogueText.maxVisibleCharacters = 0;

        dialogueText.ForceMeshUpdate();

        textoDigitando = true;

        typingCoroutine = StartCoroutine(TypeTextCoroutine());
    }

    private IEnumerator TypeTextCoroutine()
    {
        int totalCaracteres = dialogueText.textInfo.characterCount;

        for (int i = 0; i <= totalCaracteres; i++)
        {
            dialogueText.maxVisibleCharacters = i;

            yield return new WaitForSecondsRealtime(typeDelay);
        }

        textoDigitando = false;
        typingCoroutine = null;
    }

    // Chamado quando o jogador posiciona a torre
    public void ConfirmarTorreColocada()
    {
        if (etapaAtual == TutorialStepType.WaitForTowerPlacement)
            eventoConcluido = true;
    }

    // Chamado quando um inimigo é derrotado
    public void ConfirmarInimigoDerrotado()
    {
        if (etapaAtual == TutorialStepType.WaitForEnemyDefeated)
            eventoConcluido = true;
    }

    public void PularDialogo()
    {
        if (tutorialCoroutine != null)
            StopCoroutine(tutorialCoroutine);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        EncerrarDialogo();
    }

    private void EncerrarDialogo()
    {
        dialoguePanel.SetActive(false);
        Time.timeScale = 1f;

        aguardandoContinuar = false;
        textoDigitando = false;
        tutorialCoroutine = null;
        typingCoroutine = null;
    }
}