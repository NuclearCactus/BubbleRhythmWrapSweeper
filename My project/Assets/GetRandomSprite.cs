using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandomSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Sprite[] _spritesPopped;


    [SerializeField] private SpriteRenderer _renderer;

    void Start()
    {
        _renderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
    }

    public void Pop()
    {
        _renderer.sprite = _spritesPopped[Random.Range(0, _spritesPopped.Length)];

    }
}
