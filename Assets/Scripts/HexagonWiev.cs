using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonWiev : MonoBehaviour
{   
    public HexagonModel hexagon;
    private SpriteRenderer _spriteRenderer;

    void Start() => _spriteRenderer = GetComponent<SpriteRenderer>();

    void Update()
    {
        _spriteRenderer.color=hexagon.color;
        transform.position = hexagon.position;
    }
}
