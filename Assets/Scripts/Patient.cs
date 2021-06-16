using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

[System.Serializable]
public class FaceAnimation
{
    [SerializeField] public Texture[] faceTextures;
    [SerializeField] public float animationTime = 2.4f;
}
public class Patient : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private int morale;
    public int moraleThreshold;
    [SerializeField] FaceAnimation happyFace, sadFace, normalFace;
    [SerializeField] private Renderer faceRenderer;
    private bool animatingFace = false;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        SetFace();
    }

    public void Answer()
    {
        AnimateFace();
    }

    public void React()
    {
        print("React");
        if (morale >= moraleThreshold)
        {
            anim.SetTrigger("Success");
        }
        else
        {
            anim.SetTrigger("Fail");
        }
    }

    public void Fail()
    {
        LevelManager.INSTANCE.LoadCurrentLevel();
    }

    public void Success()
    {
        LevelManager.INSTANCE.LoadNextLevel();
    }

    public void AnimateFace()
    {
        if (!animatingFace)
        {
            animatingFace = true;
            FaceAnimation face = GetFace();
            if (face.faceTextures.Length > 0)
            {
                //int count = (int)((faceAnimationTime / answerAnimationFaceTextures.Length) / 0.1f);
                StartCoroutine(AnimateFaceCoroutine(face, 2, 0));
            }
        }
    }

    private IEnumerator AnimateFaceCoroutine(FaceAnimation face, int count, int frame)
    {
        faceRenderer.materials[0].SetTexture("_BaseMap", face.faceTextures[frame]);
        yield return new WaitForSeconds(0.1f);
        if (count > 0)
        {
            if (frame == face.faceTextures.Length - 1)
            {
                count--;
                frame = -1;
            }
            frame++;
            StartCoroutine(AnimateFaceCoroutine(face, count, frame));
        }
        else
        {
            SetFace();
            animatingFace = false;
        }
    }

    public void TriggerDialogueManager()
    {
        StartCoroutine(Singleton.DM.ShowSpeechBalloon(0));
    }

    public void TriggerMoraleBar()
    {
        Singleton.MORALE.Show();
    }
    public void GainMorale(int moralePoint)
    {
        morale += moralePoint;
    }

    private void SetFace()
    {
        FaceAnimation face = GetFace();
        faceRenderer.materials[0].SetTexture("_BaseMap", face.faceTextures[0]);
    }

    private FaceAnimation GetFace()
    {
        FaceAnimation face = normalFace;
        if (GetMood() == Mood.HAPPY)
            face = happyFace;
        else if (GetMood() == Mood.SAD)
            face = sadFace;
        return face;
    }

    public int GetMorale()
    {
        return morale;
    }

    private Mood GetMood()
    {
        Mood mood = Mood.NORMAL;
        if (morale < moraleThreshold)
            mood = Mood.SAD;
        else if (morale > moraleThreshold)
            mood = Mood.HAPPY;
        return mood;
    }

    private enum Mood { SAD, NORMAL, HAPPY };
}
