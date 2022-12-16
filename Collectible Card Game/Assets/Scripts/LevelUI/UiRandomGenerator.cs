using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiRandomGenerator : MonoBehaviour
{
    #region Variables
    [Header("CardsListUpdater")]
    [SerializeField] private CardsListUpdater cardsListUpdater;
    #endregion

    #region Methods
    public void GenerateRandomMana()
    {
        for (int i = 0; i < cardsListUpdater.CardsInHandList.Count; i++)
        {
            cardsListUpdater.CardsInHandList[i].GetComponent<CardParams>().GenerateRandomManaAmount();
        }

        for (int i = 0; i < cardsListUpdater.CardsOnBoardList.Count; i++)
        {
            cardsListUpdater.CardsOnBoardList[i].GetComponent<CardParams>().GenerateRandomManaAmount();
        }
    }

    public void GenerateRandomHealth()
    {
        for (int i = 0; i < cardsListUpdater.CardsInHandList.Count; i++)
        {
            cardsListUpdater.CardsInHandList[i].GetComponent<CardParams>().GenerateRandomHealthAmount();
        }

        for (int i = 0; i < cardsListUpdater.CardsOnBoardList.Count; i++)
        {
            cardsListUpdater.CardsOnBoardList[i].GetComponent<CardParams>().GenerateRandomHealthAmount();
        }
    }

    public void GenerateRandomDamage()
    {
        for (int i = 0; i < cardsListUpdater.CardsInHandList.Count; i++)
        {
            cardsListUpdater.CardsInHandList[i].GetComponent<CardParams>().GenerateRandomDamageAmount();
        }

        for (int i = 0; i < cardsListUpdater.CardsOnBoardList.Count; i++)
        {
            cardsListUpdater.CardsOnBoardList[i].GetComponent<CardParams>().GenerateRandomDamageAmount();
        }
    }
    #endregion
}
