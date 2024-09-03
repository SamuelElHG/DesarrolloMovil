using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [Header("Scores")]

    [SerializeField] private TMP_Text lastScore, HighScore, High2, High3;
    // Start is called before the first frame update
    void Start()
    {

        lastScore.text = PlayerPrefs.GetInt("LastScore").ToString();
        HighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        High2.text = PlayerPrefs.GetInt("HighScore2").ToString();
        High3.text = PlayerPrefs.GetInt("HighScore3").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
