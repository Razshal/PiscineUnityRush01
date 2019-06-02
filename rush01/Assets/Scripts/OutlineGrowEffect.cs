using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineGrowEffect : MonoBehaviour
{

    public float durationTransition = 2.0f;
    private float timer = 0.0f;
    private Outline _outline;

    private Color colorStart;
    private Color colorEnd;
    
    private void Start()
    {
        _outline = GetComponent<Outline>();
        colorStart = _outline.OutlineColor;
        colorEnd = colorStart;
        colorEnd.a = 0.0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > durationTransition)
        {
            Color swap = colorEnd;
            colorEnd = colorStart;
            colorStart = swap;
            timer = 0;
        }

        _outline.OutlineColor = Color.Lerp(colorStart, colorEnd, timer / durationTransition);
    }
}
