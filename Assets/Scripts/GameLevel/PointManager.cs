using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    private int point;
    private int pointUpper;

    [SerializeField]
    private Text pointText;

    private void Start()
    {
        pointText.text = point.ToString();
    }

    public void UpPoint(string difficultyLevel)
    {
        switch (difficultyLevel)
        {
            case "kolay":
                pointUpper = 5;
                break;

            case "orta":
                pointUpper = 10;
                break;

            case "zor":
                pointUpper = 15;
                break;

            default:
                break;
        }
        point += pointUpper;
        pointText.text = point.ToString();
    }
}