using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardParams : MonoBehaviour
{
    #region Variables
    //Amount of card health.
    private int healthPoints;
    //Amount of card damage to enemy's cards.
    private int damagePoints;
    //Amount of mana spend on drop card to the table.
    private int manaCost;
    //Name of charater on the card.
    private string cardName;
    private bool isAlive;
    //Properties for manipulating class variables.
    public int HealthPoints { get { return healthPoints; } set { healthPoints = value; } }
    public int DamagePoints { get { return damagePoints; } set { damagePoints = value; } }
    public int ManaCost { get { return manaCost; } set { manaCost = value; } }
    public string CardName { get { return cardName; } set { cardName = value; } }

    public bool IsAlive { get { return isAlive; } }
    #endregion

    #region Methods
    private void Start()
    {
        isAlive = true;
    }

    private void Update()
    {
        CheckIsAlive();
    }

    /// <summary>
    /// The method transfers parameters from the card from the hand to the card on the table (the attacking card).
    /// </summary>
    /// <param name="HealthPoints"></param>
    /// <param name="DamagePoints"></param>
    public void HandOverParams(int HealthPoints, int DamagePoints)
    {
        healthPoints = HealthPoints;
        damagePoints = DamagePoints;
    }

    private void CheckIsAlive()
    {
        if (healthPoints <= 0)
        {
            isAlive = false;
        }
    }

    public void GenerateRandomManaAmount() => manaCost = Random.Range(-2, 10);

    public void GenerateRandomHealthAmount() => healthPoints = Random.Range(-2, 10);

    public void GenerateRandomDamageAmount() => damagePoints = Random.Range(-2, 9);
    #endregion
}
