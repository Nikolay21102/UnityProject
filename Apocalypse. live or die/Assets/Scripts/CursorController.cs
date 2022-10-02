using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private SpriteRenderer cursorSprite;
    void Start()
    {
        cursorSprite = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 cursor = Input.mousePosition;
        cursorSprite.transform.position = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(cursor.x, cursor.y, UnityEngine.Camera.main.nearClipPlane));
    }
}
