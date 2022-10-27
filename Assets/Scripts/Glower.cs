using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Glower : MonoBehaviour
{
    
    public SpriteRenderer spriteRenderer;
    public Color regColor = Color.white;
    public Color changeColor;
    public float duration;

    Color lerpedColor;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lerpedColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        lerpedColor = Color.Lerp(regColor, changeColor, Mathf.PingPong(Time.time, 1));
        spriteRenderer.color = lerpedColor;
    }
}
