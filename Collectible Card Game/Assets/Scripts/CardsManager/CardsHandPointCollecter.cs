using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsHandPointCollecter : MonoBehaviour
{
    #region Переменные
    [Header("Array of cards points in hand transforms.")]
    [SerializeField] private Transform[] handCardPoints;

    //Property to get an array.
    public Transform[] HandCardPoints { get { return handCardPoints; } }
    #endregion
}
