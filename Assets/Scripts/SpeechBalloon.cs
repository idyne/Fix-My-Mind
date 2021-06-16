using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBalloon : MonoBehaviour
{
    [SerializeField] private Text speechBalloon;

    public void Show()
    {
        gameObject.SetActive(true);
        //AnimationPlayer.INSTANCE.AddAnimation(new FateGames.ScaleAnimation(transform, 0.3f, Vector3.zero, Vector3.one, FateGames.Animation.EaseInOutQuint));
        transform.localScale = Vector3.zero;
        transform.LeanScale(Vector3.one, 0.3f);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetText(string text)
    {
        speechBalloon.text = text;
    }
}
