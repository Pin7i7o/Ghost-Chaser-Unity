using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Locks and Hides the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

}
