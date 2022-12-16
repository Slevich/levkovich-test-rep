using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsHandPositionUpdater : MonoBehaviour
{
    #region Variables
    //CardsHandPointCollecter component.
    private CardsHandPointCollecter pointCollecter;
    #endregion

    #region Methods
    /// <summary>
    /// Receive component in children on start.
    /// </summary>
    private void Start()
    {
        pointCollecter = GetComponentInChildren<CardsHandPointCollecter>();
    }

    /// <summary>
    /// The method changes the anchor points of all cards in the hand.
    /// </summary>
    /// <param name="cardInHandList"></param>
    public void UpdateCardsPositions(List<GameObject> cardInHandList)
    {
        for (int i = 0; i < cardInHandList.Count; i++)
        {
            if (cardInHandList[i] != null)
            {
                cardInHandList[i].GetComponent<CardsAnimations>().CardHandPointTransform = pointCollecter.HandCardPoints[i];
            }
        }
    }
    #endregion
}
