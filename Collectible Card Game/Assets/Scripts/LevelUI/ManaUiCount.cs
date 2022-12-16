using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaUiCount : MonoBehaviour
{
    #region Variables
    [Header("Amount of start mana.")]
    [SerializeField] private int startManaAmount;
    [Header("Array of objects with black circles sprites.")]
    [SerializeField] private GameObject[] emptyManaSlotSprite;
    [Header("Array of objects with mana crystals sprites.")]
    [SerializeField] private GameObject[] manaSprites;
    [Header("TextMeshPro object with text of current mana amount.")]
    [SerializeField] private TextMeshPro currentManaCount;
    [Header("TextMeshPro object with text of all mana amount.")]
    [SerializeField] private TextMeshPro allManaCount;

    //Int current mana amount.
    private int currentManaAmount;

    public int CurrentManaAmount { get { return currentManaAmount; } }
    #endregion

    #region Methods

    /// <summary>
    /// The starting mana value must be at least 1 and no more than 11.
    /// </summary>
    private void OnValidate()
    {
        if (startManaAmount > 11)
        {
            startManaAmount = 11;
        }
        else if (startManaAmount < 1)
        {
            startManaAmount = 1;
        }
    }

    /// <summary>
    /// At the start, assign the current mana value to the maximum mana value.
    /// Call the methods for updating the UI counters.
    /// </summary>
    private void Start()
    {
        currentManaAmount = startManaAmount;
        UpdateManaSlotSprites();
        UpdateCurrentManaSprites();
    }

    /// <summary>
    /// The method runs through an array of empty mana crystal slots, 
    /// and includes them in an amount equal to the maximum mana value.
    /// </summary>
    private void UpdateManaSlotSprites()
    {
        allManaCount.text = startManaAmount.ToString();
        currentManaCount.text = currentManaAmount.ToString();

        for (int i = 0; i < startManaAmount; i++)
        {
            emptyManaSlotSprite[i].SetActive(true);
        }
    }

    /// <summary>
    /// The method runs through an array of mana crystal sprites objects, 
    /// and includes them in an amount equal to the current mana value.
    /// </summary>
    private void UpdateCurrentManaSprites()
    {
        currentManaCount.text = currentManaAmount.ToString();

        for (int i = 0; i < startManaAmount; i++)
        {
            if (i < currentManaAmount)
            {
                manaSprites[i].SetActive(true);
            }
            else
            {
                manaSprites[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// The method takes away an amount of mana.
    /// </summary>
    /// <param name="manaRecession"></param>
    public void TakeAwayMana(int manaRecession)
    {
        currentManaAmount -= manaRecession;
        UpdateCurrentManaSprites();
    }
    #endregion
}
