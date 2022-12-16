using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCardTrigger : MonoBehaviour
{
    #region Variables
    [Header("Mouse input component")]
    [SerializeField] private MouseInput mouseInput;
    [Header("Cards manipulator component")]
    [SerializeField] private CardsManipulator cardsManipulator;
    [Header("List updater component")]
    [SerializeField] private CardsListUpdater listUpdater;
    [Header("ManaUiCount component")]
    [SerializeField] private ManaUiCount manaCount;
    [Header("Material of board trigger.")]
    [SerializeField] private Material greenTransparentMat;
    [Header("Max distance delta")]

    //Color of material, when card is out the trigger.
    private Color outTriggerColor = new Color(0, 255, 0, 0);
    //Color of material, when card is comes in the trigger.
    private Color inTriggerColor = new Color(0, 255, 0, 0.05f);
    #endregion

    #region Methods
    /// <summary>
    /// Set the start color of board trigger.
    /// </summary>
    private void Start()
    {
        greenTransparentMat.SetColor("_Color", outTriggerColor);
    }

    /// <summary>
    /// On trigger enter, change color trigger.
    /// Set the value of bool = true.
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter(Collider collider)
    {
        if ((collider.CompareTag("SelectedCard") || collider.CompareTag("Card")) && mouseInput.MouseButtonPressed)
        {
            greenTransparentMat.SetColor("_Color", inTriggerColor);
            cardsManipulator.InBoardTrigger = true;
        }
    }

    /// <summary>
    /// On trigger stay, Change the map layer to nontraycasted.
    /// Drop the card on the board, then switch its state to change to attack phase.
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerStay(Collider collider)
    {
        if ((collider.CompareTag("SelectedCard") || collider.CompareTag("Card")) && mouseInput.MouseButtonPressed == false && collider.GetComponent<CardsStates>().CardStateNumber != 1 && manaCount.CurrentManaAmount > 0)
        {
            collider.gameObject.layer = 8;
            collider.gameObject.GetComponent<CardsStates>().ChangeCardState(8);
            collider.GetComponent<CardsAnimations>().CardHandPointTransform = transform;
            greenTransparentMat.SetColor("_Color", outTriggerColor);

            if (Vector3.Distance(collider.gameObject.transform.position, transform.position) < 0.1f)
            {
                manaCount.TakeAwayMana(collider.GetComponent<CardParams>().ManaCost);
                collider.gameObject.GetComponent<CardsStates>().ChangeCardState(6);
            }
        }
    }

    /// <summary>
    /// OnTriggerExit, change board trugger color.
    /// Change Card state to default.
    /// Set the value of bool = false.
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerExit(Collider collider)
    {
        if ((collider.CompareTag("SelectedCard") || collider.CompareTag("Card")) && mouseInput.MouseButtonPressed)
        {
            greenTransparentMat.SetColor("_Color", outTriggerColor);
            collider.gameObject.GetComponent<CardsStates>().ChangeCardState(2);
            cardsManipulator.InBoardTrigger = false;
        }
    }
    #endregion
}
