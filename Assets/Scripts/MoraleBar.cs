using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoraleBar : MonoBehaviour
{
    [SerializeField] private Slider bar;
    [SerializeField] private Image emoji;
    [SerializeField] private Sprite[] emojis;
    [SerializeField] private float speed = 1;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [SerializeField] private ParticleSystem emojiSparkle;
    private int morale;

    private void Awake()
    {
        SetMorale(Singleton.PATIENT.GetMorale());
        bar.value = morale;
        gradient.Evaluate(bar.normalizedValue);
    }
    private void Update()
    {
        bar.value = Mathf.MoveTowards(bar.value, morale, Time.deltaTime * speed);
        fill.color = gradient.Evaluate(bar.normalizedValue);
        if (Mathf.Abs(bar.value - morale) < 3)
            SetEmoji();
    }

    public void Show()
    {
        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        transform.LeanScale(Vector3.one, 0.2f).setEaseInOutQuint();
    }
    public void SetMorale(int morale)
    {
        this.morale = morale;
    }

    private void SetEmoji()
    {
        if (emojiSparkle)
            emojiSparkle.Play();
        int morale = (int)bar.value;
        emoji.sprite = emojis[Mathf.Clamp(morale, 0, 99) / 20];
    }
}
