using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public delegate float EaseFunction(float x);
    public Transform obj1, obj2;
    private void Update()
    {
        //print(EaseInSine(Mathf.PingPong(Time.time, 1)));
        Scale(obj1, EaseInSine);
        Scale(obj2, EaseInOutQuint);
    }

    public void Scale(Transform obj, EaseFunction easeFunction)
    {
        obj.localScale = Vector3.one * easeFunction(Mathf.PingPong(2 * Time.time, 1));
    }
    private float EaseInSine(float x)
    {
        return 1 - Mathf.Cos((x * Mathf.PI) / 2);
    }

    private float EaseInOutQuint(float x)
    {
        return x < 0.5f ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;
    }
}
