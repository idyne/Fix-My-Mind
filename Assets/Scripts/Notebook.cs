using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Notebook : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Check()
    {
        anim.SetTrigger("Check");
    }


    public void TriggerPatient()
    {
        Singleton.PATIENT.TriggerDialogueManager();
        Singleton.PATIENT.TriggerMoraleBar();
    }

}
