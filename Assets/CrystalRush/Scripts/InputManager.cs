using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public TMP_Text text;
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody2D objectToDrag;
    private Vector3 initialPosition;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.GetComponent<Rigidbody2D>() != null)
            {
                objectToDrag = hit.collider.GetComponent<Rigidbody2D>();
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            objectToDrag = null;
        }

        
        
        if (objectToDrag != null)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            objectToDrag.MovePosition(newPosition);
            if (Mathf.Abs(objectToDrag.transform.position.y) > Mathf.Abs(objectToDrag.transform.position.x))
            {
                if (objectToDrag.transform.position.y<0)
                {
                    text.text = "Down";
                }
                else
                {
                    text.text = "Up";
                }
            }
            else
            {
                if (objectToDrag.transform.position.x < 0)
                {
                    text.text = "Left";
                }
                else
                {
                    text.text = "Right";
                }
            }
        }

        
    }
}
