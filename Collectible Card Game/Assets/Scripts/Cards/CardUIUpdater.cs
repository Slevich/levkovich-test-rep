using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardUIUpdater : MonoBehaviour
{
    #region Variables
    [Header("TextMeshPro component with health points text.")]
    [SerializeField] private TextMeshPro healthPointsText;
    [Header("TextMeshPro component with damage counter text.")]
    [SerializeField] private TextMeshPro damagePointsText;
    [Header("TextMeshPro component with mana cost text.")]
    [SerializeField] private TextMeshPro manaCostPointText;
    [Header("TextMeshPro component with card name text.")]
    [SerializeField] private TextMeshPro cardName;

    //CardParams component
    private CardParams cardParams;
    #endregion

    #region Methods
    /// <summary>
    /// On Start get CardParams component
    /// </summary>
    private void Start()
    {
        cardParams = GetComponent<CardParams>();
    }

    /// <summary>
    /// In Update call the method of update ui counts.
    /// </summary>
    private void Update()
    {
        UpdateUICounts();
    }

    /// <summary>
    /// The method updates the current parameter text on the UI.
    /// </summary>
    private void UpdateUICounts()
    {
        if (manaCostPointText != null)
        {
            healthPointsText.text = cardParams.HealthPoints.ToString();
            damagePointsText.text = cardParams.DamagePoints.ToString();
            manaCostPointText.text = cardParams.ManaCost.ToString();
            cardName.text = cardParams.CardName.ToString();
        }
        else
        {
            healthPointsText.text = cardParams.HealthPoints.ToString();
            damagePointsText.text = cardParams.DamagePoints.ToString();
        }
    }
    #endregion
}
