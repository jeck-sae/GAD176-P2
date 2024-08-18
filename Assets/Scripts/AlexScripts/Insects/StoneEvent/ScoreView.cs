using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{

    // private Text _scoreText;

    //[SerializeField] private string _template;

    public Text scoreNumbertext;

    private int number = 0;

    // public void Awake()
    //{

    //     _scoreText = GetComponent<Text>();

    //}

    private void OnEnable() 
    {

        Stone.TakeStone += IncreaseNumber;         //Subscribe to the event

    }


    private void OnDisable()
    {

        Stone.TakeStone -= IncreaseNumber;                 //unsubscribe to the event

    }

    public void IncreaseNumber()
    {

        number++;                              //increase number

        scoreNumbertext.text = "Number of stones " + number;          //update text on the screen

    }
}
