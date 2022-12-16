using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardsAnimations : MonoBehaviour
{
    #region Variables
    [Header("CardsSpawnController component.")]
    [SerializeField] private CardsSpawnController cardsSpawner;
    [Header("GameObject with glow frame sprite of the hand card.")]
    [SerializeField] private GameObject cardGlowFrame;
    [Header("GameObject with glow frame sprite of the attack card on the table.")]
    [SerializeField] private GameObject attackCardGlowFrame;
    [Header("Prefab with smoke burst particle system.")]
    [SerializeField] private GameObject smokeBurstVFXprefab;
    [Header("Prefab with attack card.")]
    [SerializeField] private GameObject attackCardPrefab;
    [Header("The value by which the card is shifted when it is selected.")]
    [SerializeField] private float cardSelectOffset;
    [Header("The switch for prefab, indicating this is the card in hand or on the table.")]
    [SerializeField] private bool isCardOnBoard;
    //CardsHandPointCollecter component.
    private CardsHandPointCollecter pointsCollecter;
    //CardsListUpdater component.
    private CardsListUpdater cardsUpdater;
    //Transform of the card anchor.
    private Transform cardHandPointTransform;
    //Point of the attack card anchor.
    private Vector3 attackCardPosition;
    //Vector3 position of the point of raycast hit.
    private Vector3 raycastHitPosition;
    //Card number.
    private int cardNumber;

    //Properties of this class.
    public bool IsCardOnBoard { get { return isCardOnBoard; } }
    public int CardNumber { set { cardNumber = value; } }
    public Vector3 RaycastHitPosition { set { raycastHitPosition = value; } }
    public Transform CardHandPointTransform { set { cardHandPointTransform = value; } }
    public Vector3 AttackCardPosition { set { attackCardPosition = value; } }
    #endregion

    #region Methods
    private void Start()
    {
        pointsCollecter = GameObject.Find("CardsHandPointsCollecter").GetComponent<CardsHandPointCollecter>();
        cardsUpdater = GameObject.Find("CardsManager").GetComponent<CardsListUpdater>();
        cardHandPointTransform = pointsCollecter.HandCardPoints[cardNumber];
        attackCardPosition = transform.position;
    }

    /// <summary>
    /// The method puts the card forward and up.
    /// </summary>
    public void PutForwardCard()
    {
        if (transform.position.y <= cardHandPointTransform.position.y + cardSelectOffset)
        {
            transform.DOMove(new Vector3(transform.position.x - (cardSelectOffset/2),
                                         transform.position.y + cardSelectOffset,
                                         transform.position.z + cardSelectOffset),
                                         0.5f);
        }
        else
        {
            transform.DOMove(new Vector3(transform.position.x,
                                         transform.position.y,
                                         transform.position.z),
                                         0.5f);
        }
    }

    /// <summary>
    /// The method returns the map back to the anchor point.
    /// </summary>
    public void PutBackwardCard()
    {
        transform.DOMove(cardHandPointTransform.position, 0.5f);
    }

    /// <summary>
    /// The method moves a card from the deck to the anchor point in the player's hand.
    /// </summary>
    public void MoveCardFromDeckToHand()
    {
        if (Vector3.Distance(transform.position, cardHandPointTransform.position) > 0.1f)
        {
            transform.DOMove(cardHandPointTransform.position, 2f);
            transform.DOLocalRotate((cardHandPointTransform.rotation.eulerAngles), 2f);
        }
    }

    /// <summary>
    /// The method attaches a card to its current location.
    /// </summary>
    public void StayInPlace()
    {
        transform.DOMove(transform.position, 0f);
    }

    /// <summary>
    /// The method constantly moves the card to the point where the beam hits it.
    /// </summary>
    public void MoveCardByCursor()
    {
        transform.DOMove(new Vector3(raycastHitPosition.x,
                                     transform.position.y,
                                     raycastHitPosition.z),
                                     -1f);
    }

    /// <summary>
    /// The method moves and rotates the card relative to the anchor point on the board.
    /// </summary>
    public void DropCardOnBoard()
    {
        transform.DOMove(cardHandPointTransform.position, 0.2f);
        transform.DORotate(cardHandPointTransform.rotation.eulerAngles, 0.2f);
    }

    /// <summary>
    /// Method shows one of two frames, depending on the selected object instance 
    /// (normal card in hand or attacking card on the board).
    /// </summary>
    /// <param name="isShowGlowFrame"></param>
    public void ShowHideGlowFrame(bool isShowGlowFrame)
    {
        if (isCardOnBoard)
        {
            attackCardGlowFrame.SetActive(isShowGlowFrame);
        }
        else
        {
            cardGlowFrame.SetActive(isShowGlowFrame);
        }
    }

    /// <summary>
    /// The method moves the attacking card to the anchor point.
    /// </summary>
    public void MoveAttackCardToPosition()
    {
        transform.DOMove(attackCardPosition, 0.5f);
    }


    /// <summary>
    /// The method spawns a VFX object and then destroys the game object itself.
    /// </summary>
    public void CardDestroying()
    {
        GameObject smokeVFXObject = Instantiate(smokeBurstVFXprefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    /// <summary>
    /// The method spawns a VFX object, prefabs the attacking card, 
    /// passes the spawned card to the card list on the board, 
    /// passes its parameters to it, and then destroys itself.
    /// </summary>
    public void ChangeCardToAttack()
    {
        GameObject smokeVFXObject = Instantiate(smokeBurstVFXprefab, transform.position, Quaternion.identity);
        GameObject attackCard = Instantiate(attackCardPrefab, transform.position, Quaternion.identity);
        cardsUpdater.AddNewCardOnBoardList(attackCard);
        CardParams cardParams = GetComponent<CardParams>();
        attackCard.GetComponent<CardParams>().HandOverParams(cardParams.HealthPoints, cardParams.DamagePoints);
        Destroy(gameObject);
    }
    #endregion
}
