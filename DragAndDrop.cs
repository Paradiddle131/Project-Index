using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    bool moveAllowed;
    Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) // fingers on screen
        {
            Touch touch = Input.GetTouch(0);
            // pixel coordinates to unity game coordinates 
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began) // first touch on screen
            {
                // store whatever we touch on
                Collider2D touchCollider = Physics2D.OverlapPoint(touchPosition);
                if (col == touchCollider) // touched the planet
                {
                    moveAllowed = true;
                }
            }
            if (touch.phase == TouchPhase.Moved) // still on the screen
            {
                if (moveAllowed)
                {
                    transform.position = new Vector2(touchPosition.x, touchPosition.y);
                }
            }
            if (touch.phase == TouchPhase.Ended) // off on the screen
            {
                moveAllowed = false;
            }
        }
    }
}
