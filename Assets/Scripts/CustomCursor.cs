using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Camera mainCamera;
    public SpriteRenderer cursorSprite;

    void Start()
    {
        Cursor.visible = false;
    }


    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        cursorSprite.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0f);


    }
}
