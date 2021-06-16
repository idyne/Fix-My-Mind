using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static GameManager GM;
    public static DynamicCamera CAM;
    public static DialogueManager DM;
    public static Player PLAYER;
    public static Patient PATIENT;
    public static SelectionScreen SELECTION;
    public static SpeechBalloon SPEECH;
    public static CompleteScreen COMP;
    public static Notebook NOTE;
    public static MoraleBar MORALE;
    public static GameObject LOADING;
    public static GameObject TAPTOPLAY;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
        CAM = FindObjectOfType<DynamicCamera>();
        DM = FindObjectOfType<DialogueManager>();
        PLAYER = FindObjectOfType<Player>();
        PATIENT = FindObjectOfType<Patient>();
        SELECTION = FindObjectOfType<SelectionScreen>();
        SPEECH = FindObjectOfType<SpeechBalloon>();
        COMP = FindObjectOfType<CompleteScreen>();
        NOTE = FindObjectOfType<Notebook>();
        MORALE = FindObjectOfType<MoraleBar>();
        LOADING = GameObject.Find("LoadingScreen");
        TAPTOPLAY = GameObject.Find("TapToPlay");

        HideGameObjects();
    }

    private static void HideGameObjects()
    {
        SELECTION.gameObject.SetActive(false);
        SPEECH.gameObject.SetActive(false);
        COMP.gameObject.SetActive(false);
        MORALE.gameObject.SetActive(false);
        LOADING.SetActive(false);
    }
}
