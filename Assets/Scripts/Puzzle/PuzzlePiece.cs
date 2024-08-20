using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private bool _dragging;
    private Vector2 _offset, _originalPositon;

    void Update()
    {
        if(!_dragging)
        {
            return;
        }
        var mousePosition = GetMousePostion();
        transform.position = mousePosition - _offset;
    }

    private void Awake()
    {
        _originalPositon = transform.position;
    }

    private void OnMouseDown()
    {
        _dragging = true;
        _offset = GetMousePostion() - (Vector2)transform.position;
    }

    private void OnMouseUp()
    {
        _dragging = false;
        transform.position = _originalPositon;
    }

    Vector2 GetMousePostion()
    {
       return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
