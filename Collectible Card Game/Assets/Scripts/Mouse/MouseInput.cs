using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    #region Variables
    //Bool switch, indicating whether the left-hand mouse button is pressed.
    private bool mouseButtonPressed;

    //Property.
    public bool MouseButtonPressed { get { return mouseButtonPressed; } }
    #endregion

    #region Methods
    /// <summary>
    /// Call out the method for checking the input.
    /// </summary>
    private void Update()
    {
        DetectMouseInput();
    }

    /// <summary>
    /// The method checks whether the left mouse button is pressed.
    /// </summary>
    private void DetectMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseButtonPressed = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseButtonPressed = false;
        }
    }
    #endregion
}
