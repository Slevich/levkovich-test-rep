using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsListUpdater : MonoBehaviour
{
    #region Variables
    [Header("TableTriggerPositionUpdater component")]
    [SerializeField] private TableTriggerPositionUpdater tablePositionUpdater;
    //Lists with all the cards in hand or on the table.
    private List<GameObject> cardsInHandList = new List<GameObject>();
    private List<GameObject> cardsOnBoardList = new List<GameObject>();
    //PositionUpdater component
    private CardsHandPositionUpdater positionUpdater;
    //Timer of checking cards in hand list.
    private float checkHandListTimer = 0.5f;
    //Reload timer.
    private float reloadHandListTimer;
    //Timer of checking cards on board list..
    private float checkBoardListTimer = 0.5f;
    //Reload timer.
    private float reloadCheckBoardListTimer;

    //Properties for obtaining variable values.
    public List<GameObject> CardsInHandList { get { return cardsInHandList; }  }
    public List<GameObject> CardsOnBoardList { get { return cardsOnBoardList; } }
    #endregion

    #region Methods
    /// <summary>
    /// At the start we assign timers.
    /// Get the component.
    /// </summary>
    private void Start()
    {
        reloadHandListTimer = checkHandListTimer;
        reloadCheckBoardListTimer = checkBoardListTimer;
        positionUpdater = GetComponent<CardsHandPositionUpdater>();
    }

    /// <summary>
    /// In Update, call methods with sheet checks.
    /// </summary>
    private void Update()
    {
        CheckCardsInHandListByTimer();
        CheckCardsOnBoardListByTimer();
    }

    /// <summary>
    /// The methods add or remove a new object to the relevant list.
    /// </summary>
    /// <param name="addedCard"></param>
    public void AddNewCardInHandList(GameObject addedCard)
    {
        cardsInHandList.Add(addedCard);
        positionUpdater.UpdateCardsPositions(cardsInHandList);
    }
    public void AddNewCardOnBoardList(GameObject addedCard)
    {
        cardsOnBoardList.Add(addedCard); 
        tablePositionUpdater.UpdateBoardCardsPositions(cardsOnBoardList);
    }
    public void DeleteCardInHandList(GameObject deletedCard)
    {
        cardsInHandList.Remove(deletedCard);
        positionUpdater.UpdateCardsPositions(cardsInHandList);
    }

    public void DeleteCardOnBoardList(GameObject deletedCard)
    {
        cardsOnBoardList.Remove(deletedCard);
        tablePositionUpdater.UpdateBoardCardsPositions(cardsOnBoardList);
    }


    /// <summary>
    /// The methods check if one of the elements has become null. 
    /// If so, remove it.
    /// </summary>
    private void CheckCardsInHandListByTimer()
    {
        checkHandListTimer -= Time.deltaTime;

        if (checkHandListTimer <= 0)
        {
            for (int i = 0; i < cardsInHandList.Count; i++)
            {
                if (cardsInHandList[i] == null)
                {
                    DeleteCardInHandList(cardsInHandList[i]);
                    i--;
                }
            }
            checkHandListTimer = reloadHandListTimer;
        }
    }

    private void CheckCardsOnBoardListByTimer()
    {
        checkBoardListTimer -= Time.deltaTime;

        if (checkBoardListTimer <= 0)
        {
            tablePositionUpdater.UpdateBoardCardsPositions(cardsOnBoardList);

            for (int i = 0; i < cardsOnBoardList.Count; i++)
            {
                if (cardsOnBoardList[i] == null)
                {
                    DeleteCardOnBoardList(cardsOnBoardList[i]);
                    i--;
                }
            }
            checkBoardListTimer = reloadCheckBoardListTimer;
        }
    }
    #endregion
}
