using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    private Dialogue.Node[] answers;
    [SerializeField] private Text option_1, option_2;
    [SerializeField] private Image option_1_image, option_2_image;
    [SerializeField] private Color badColor, goodColor, neutralColor;

    public void SelectAnswer(int index)
    {
        DialogueManager DM = Singleton.DM;
        Patient PATIENT = Singleton.PATIENT;
        MoraleBar MORALE = Singleton.MORALE;
        Dialogue.Node answer = answers[index];
        PATIENT.GainMorale(answer.GetMoralePoint());
        MORALE.SetMorale(PATIENT.GetMorale());
        DM.HideSelectionScreen();
        DM.HideSpeechBalloon();
        DM.dialogue.SetCurrentNode(answer.GetAnswer());
        Singleton.PLAYER.Answer();

    }
    public void SetAnswers(Dialogue.Node[] answers)
    {
        this.answers = answers;
        if (answers[0] != null)
        {
            option_1.text = answers[0].GetWord();
            if (answers[0].GetMoralePoint() > 0)
                option_1_image.color = goodColor;
            else if (answers[0].GetMoralePoint() < 0)
                option_1_image.color = badColor;
            else
                option_1_image.color = neutralColor;
        }
        if (answers[1] != null)
        {
            option_2.text = answers[1].GetWord();
            if (answers[1].GetMoralePoint() > 0)
                option_2_image.color = goodColor;
            else if (answers[1].GetMoralePoint() < 0)
                option_2_image.color = badColor;
            else
                option_2_image.color = neutralColor;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            //AnimationPlayer.INSTANCE.AddAnimation(new ScaleAnimation(child, 0.5f, Vector3.zero, Vector3.one, FateGames.Animation.EaseInOutQuint));
            child.localScale = Vector3.zero;
            child.LeanScale(Vector3.one, 0.3f).setEaseInOutQuint();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
