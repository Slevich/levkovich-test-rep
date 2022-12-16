using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTriggerPositionUpdater : MonoBehaviour
{
    #region Methods
    /// <summary>
    /// The method cycles through the list of cards on the table, 
    /// changing their anchor points depending on their multiplicity of numbers.
    /// If a multiple of 2 or 3, then to one side of the table, if not, to the other.
    ///  This allows for a more even distribution of cards,
    /// relative to the centre of the table.
    /// </summary>
    /// <param name="cardsList"></param>
    public void UpdateBoardCardsPositions(List<GameObject> cardsList)
    {
        for(int i = 0; i < cardsList.Count; i++)
        {
            if (cardsList.Count > 0 && cardsList[i] != null)
            {
                 if (i == 0) cardsList[i].GetComponent<CardsAnimations>().AttackCardPosition = transform.position;
                 else if (i % 2 == 0 || i % 3 == 0) cardsList[i].GetComponent<CardsAnimations>().AttackCardPosition = new Vector3(transform.position.x - i,
                                                                                                                                  transform.position.y,
                                                                                                                                  transform.position.z);
                 else cardsList[i].GetComponent<CardsAnimations>().AttackCardPosition = new Vector3(transform.position.x + i,
                                                                                                    transform.position.y,
                                                                                                    transform.position.z);
            }
            else break;
        }
    }
    #endregion
}
