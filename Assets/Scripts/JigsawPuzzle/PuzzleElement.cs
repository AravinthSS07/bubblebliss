using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleElement : MonoBehaviour
{
    private Vector2 originalPosition, _offset;
    private bool _dragging;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_dragging) return;
        transform.position = (Vector2)Input.mousePosition - _offset;
    }

    void OnMouseDown()//PointerEventData eventData
    {
        _dragging = true;
        _offset = GetMousePos() - (Vector2)transform.position;
    }

    void OnMouseUp()//PointerEventData eventData
    {
        transform.position = originalPosition;
        _dragging = false;
    }

    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
