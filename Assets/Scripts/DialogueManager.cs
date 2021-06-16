using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueState state = DialogueState.INIT;
    [HideInInspector] public Dialogue dialogue;

    private void Awake()
    {
        dialogue = new Dialogue(Singleton.GM.level);
        StartCoroutine(dialogue.ReadDialogue());
    }

    public IEnumerator ShowSelectionScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        SelectionScreen screen = Singleton.SELECTION;
        screen.Show();
    }
    public void HideSelectionScreen()
    {
        SelectionScreen screen = Singleton.SELECTION;
        screen.Hide();
    }
    public IEnumerator ShowSpeechBalloon(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpeechBalloon balloon = Singleton.SPEECH;
        balloon.Show();
        Singleton.PATIENT.Answer();
        if (dialogue.currentNode.GetAnswer() != null)
            StartCoroutine(ShowSelectionScreen(1));
        else
            Singleton.GM.EndGame();
    }
    public void HideSpeechBalloon()
    {
        SpeechBalloon balloon = Singleton.SPEECH;
        balloon.Hide();
    }

}

public enum DialogueState { INIT, PATIENT_SPEAKING, PLAYER_SPEAKING, CHECKING_NOTEBOOK, PATIENT_WALKING_IN }
