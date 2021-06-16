using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Answer()
    {
        anim.SetTrigger("Answer");
    }

    public void TriggerDialogueManager()
    {
        StartCoroutine(Singleton.DM.ShowSpeechBalloon(0));
    }
}
