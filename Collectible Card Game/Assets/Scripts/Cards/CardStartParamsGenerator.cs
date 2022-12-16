using System;
using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStartParamsGenerator : MonoBehaviour
{
    #region Variables
    //String with generated card name.
    private string cardNameString;
    //CardParams of the current card.
    private CardParams cardParams;
    //SpriteRenderer component of the card portrait.
    private SpriteRenderer cardPortrait;
    #endregion

    #region Methods
    /// <summary>
    /// At the start we get the map parameter component, generating random data.
    /// </summary>
    private void Start()
    {
        cardParams = GetComponent<CardParams>();
        GenerateRandomParamsOnStart();
    }

    /// <summary>
    /// The method generates random values for map parameters from 1 to 10 (inclusive).
    /// </summary>
    private void GenerateRandomParamsOnStart()
    {
        cardParams.ManaCost = UnityEngine.Random.Range(1, 11);
        cardParams.HealthPoints = UnityEngine.Random.Range(1, 11);
        cardParams.DamagePoints = UnityEngine.Random.Range(1, 11);

        //Uri url = new Uri("https://picsum.photos/200");

        //using (WebClient client = new WebClient())
        //{
        //    client.DownloadFileAsync(url, "portrait");
        //}
        //cardPortrait.sprite = Resources.LoadAsync<Sprite>()

        GetRandomCardName();
    }

    /// <summary>
    /// The method gets two random words from the resource in the query, 
    /// gets the text of the words themselves from the string and passes it into a variable.
    /// </summary>
    private void GetRandomCardName()
    {
        WebRequest request = WebRequest.Create("https://random-word-api.herokuapp.com/word?number=2");
        WebResponse response = request.GetResponse();
        Stream stream = response.GetResponseStream();
        StreamReader streamReader = new StreamReader(stream);
        string responseString = streamReader.ReadToEnd();
        string[] words = responseString.Split('"');

        cardNameString = $"{words[1]} {words[3]}";
        cardParams.CardName = cardNameString;
    }
    #endregion
}