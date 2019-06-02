using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineGrowEffect : MonoBehaviour
{

    public float durationTransition = 2.0f;
    [SerializeField]
    private float timer = 0.0f;
    [SerializeField]
    private Outline _outline;

    [SerializeField]
    private Color colorStart;
    [SerializeField]
    private Color colorEnd;

    private bool first = false;
    
    private void LateStart()
    {
        _outline = GetComponent<Outline>();
        colorStart = _outline.OutlineColor;
        colorEnd = colorStart;
        colorEnd.a = 0.0f;
    }

    private void Update()
    {
        if (!first)
        {
            LateStart();
            first = true;
        }
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
