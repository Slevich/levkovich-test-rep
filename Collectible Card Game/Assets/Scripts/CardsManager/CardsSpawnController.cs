using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsSpawnController : MonoBehaviour
{
    #region Variables
    [Header("CardHandPointCollecter component.")]
    [SerializeField] private CardsHandPointCollecter pointsCollecter;
    [Header("Prefab with the usual card.")]
    [SerializeField] private GameObject cardPrefab;
    [Header("Transform of trigger, where spawned a new cards.")]
    [SerializeField] private Transform spawnTriggerTransform;
    //GameObject with new spawned card.
    private GameObject spawnedCard;
    //CardsListUpdater component.
    private CardsListUpdater listUpdater;
    //Variable, represents that, card reach hand point after it's spawn.
    private bool isCardInHand;
    //Number of cards, which spawned on start.
    private int startCardsAmount;
    //Current card number.
    private int cardNumber;
    //Property.
    public int StartCardsAmount { get { return startCardsAmount; } }
    #endregion

    #region Methods
    /// <summary>
    /// Get component at start.
    /// Generate random number of spawned cards.
    /// </summary>
    private void Start()
    {
        listUpdater = GetComponentInParent<CardsListUpdater>();
        startCardsAmount = Random.Range(4, 7);
    }

    /// <summary>
    /// Call for methods to spawn a new card and check if it has reached your hand.
    /// </summary>
    private void Update()
    {
        SpawnCardOnStart();
        CheckCardReachHandPoint();
    }

    /// <summary>
    /// The method spawns a new card until the number reaches zero.
    ///It then passes the number of the card to see which point in the array it should go to.
    /// </summary>
    private void SpawnCardOnStart()
    {
        if (isCardInHand == false && startCardsAmount > 0)
        {
            spawnedCard = Instantiate(cardPrefab, spawnTriggerTransform.position, Quaternion.Euler(-90,0,-90));
            listUpdater.AddNewCardInHandList(spawnedCard);
            spawnedCard.GetComponent<CardsAnimations>().CardNumber = listUpdater.CardsInHandList.IndexOf(spawnedCard);
            cardNumber = listUpdater.CardsInHandList.IndexOf(spawnedCard);
            isCardInHand = true;
            startCardsAmount--;
        }
    }

    /// <summary>
    /// The method checks if the card has reached a point in the hand, then switches to spawning a new card.
    /// </summary>
    private void CheckCardReachHandPoint()
    {
        if (spawnedCard != null)
        {
            if (Vector3.Distance(spawnedCard.transform.position, pointsCollecter.HandCardPoints[cardNumber].position) < 0.1f)
            {
                isCardInHand = false;
            }
        }
    }
    #endregion
}
