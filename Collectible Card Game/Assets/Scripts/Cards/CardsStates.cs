using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsStates : MonoBehaviour
{
    #region Variables
    //Variable of the card status enumeration type.
    CardStatesEnum cardState = new CardStatesEnum();
    //CardsAnimations component.
    private CardsAnimations cardsAnim;
    private CardParams cardsParams;
    //Number of current state.
    private int cardStateNumber;

    //The property with the card number.
    public int CardStateNumber { get { return cardStateNumber; } }
    #endregion

    #region Methods

    /// <summary>
    /// Enum containing an enumeration of all card states.
    /// </summary>
    enum CardStatesEnum : int
    {
        Moving = 1,
        Idle = 2,
        Selected = 3,
        Grabbed = 4,
        EmptyIdle = 5,
        ChangeToAttackState = 6,
        AttackIdle = 7,
        DropOnBoard = 8,
        Destroying = 9
    }

    /// <summary>
    /// Depending on the type of card, change the initial state.
    /// </summary>
    private void Start()
    {
        cardsAnim = GetComponent<CardsAnimations>();
        cardsParams = GetComponent<CardParams>();

        if (cardsAnim.IsCardOnBoard)
        {
            cardState = (CardStatesEnum)5;
        }
        else
        {
            cardState = (CardStatesEnum)1;
        }
    }


    private void Update()
    {
        CardStateSwitch();
        cardStateNumber = (int)cardState;

        if (cardsParams.IsAlive == false)
        {
            ChangeCardState(9);
        }
    }

    private void CardStateSwitch()
    {
        switch (cardState)
        {
            case CardStatesEnum.Moving:
                cardsAnim.MoveCardFromDeckToHand();
                break;

            case CardStatesEnum.Idle:
                cardsAnim.ShowHideGlowFrame(false);
                cardsAnim.PutBackwardCard();
                break;

            case CardStatesEnum.Selected:
                cardsAnim.ShowHideGlowFrame(true);
                cardsAnim.PutForwardCard();
                break;

            case CardStatesEnum.Grabbed:
                cardsAnim.MoveCardByCursor();
                break;

            case CardStatesEnum.EmptyIdle:
                cardsAnim.MoveAttackCardToPosition();
                cardsAnim.ShowHideGlowFrame(false);
                break;

            case CardStatesEnum.ChangeToAttackState:
                cardsAnim.ChangeCardToAttack();
                break;

            case CardStatesEnum.AttackIdle:
                cardsAnim.ShowHideGlowFrame(true);
                cardsAnim.StayInPlace();
                break;

            case CardStatesEnum.DropOnBoard:
                cardsAnim.DropCardOnBoard();
                break;

            case CardStatesEnum.Destroying:
                cardsAnim.CardDestroying();
                break;


        }
    }

    public void ChangeCardState(int cardStateNumber)
    {
        cardState = (CardStatesEnum)cardStateNumber;
    }
    #endregion
}
