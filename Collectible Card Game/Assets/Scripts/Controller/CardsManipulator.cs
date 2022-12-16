using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManipulator : MonoBehaviour
{
    #region Variables
    //CursorRaycast component.
    private CursorRaycast cursorRaycast;
    //MouseInput component.
    private MouseInput mouseInput;
    //GameObject, which represents selected card.
    private GameObject selectedCard;
    //Bool, which represents, that card is taken and grabbed.
    private bool cardGrabbed;
    //Bool, which represents that in board trigger card or not.
    private bool inBoardTrigger;

    //Property.
    public bool InBoardTrigger { get { return inBoardTrigger; }  set { inBoardTrigger = value; } }
    #endregion

    #region Methods
    /// <summary>
    /// Get the components at the start, toggle the variable to false.
    /// </summary>
    private void Start()
    {
        inBoardTrigger = false;
        cursorRaycast = GetComponent<CursorRaycast>();
        mouseInput = GetComponent<MouseInput>();
    }

    /// <summary>
    /// In the update, call up the cards manipulation methods.
    /// </summary>
    private void Update()
    {
        SelectCard();
        DragAndDropCard();
    }

    /// <summary>
    /// The method checks the card's tag, changing its state to the selected one or not.
    /// </summary>
    private void SelectCard()
    {
        if (cursorRaycast.RaycastedObject != null && cursorRaycast.RaycastedObject.tag == "SelectedCard")
        {
            cursorRaycast.RaycastedObject.GetComponent<CardsStates>().ChangeCardState(3);
        }
        else if (cursorRaycast.RaycastedObject != null && cursorRaycast.PreviousObject != null && cursorRaycast.PreviousObject.tag == "CardInHand")
        {
            cursorRaycast.PreviousObject.GetComponent<CardsStates>().ChangeCardState(2);
        }
    }

    /// <summary>
    /// The method changes the state of the map if it is selected and the left mouse button is pressed.
    /// </summary>
    private void DragAndDropCard()
    {
        if (cursorRaycast.RaycastedObject != null && cursorRaycast.RaycastedObject.tag == "SelectedCard" && mouseInput.MouseButtonPressed)
        {
            selectedCard = cursorRaycast.RaycastedObject;
            selectedCard.GetComponent<CardsAnimations>().RaycastHitPosition = cursorRaycast.HitColliderPosition;
            selectedCard.GetComponent<CardsStates>().ChangeCardState(4);
            cardGrabbed = true;
        }
        else if (cardGrabbed && mouseInput.MouseButtonPressed == false)
        {
            if (inBoardTrigger == false)
            {
                selectedCard.GetComponent<CardsStates>().ChangeCardState(2);
            }
            cardGrabbed = false;
        }
    }
    #endregion
}
