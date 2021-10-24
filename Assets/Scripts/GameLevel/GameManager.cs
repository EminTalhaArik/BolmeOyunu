using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Square")]
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private Transform panelOfSquares;

    [Header("Question")]
    [SerializeField] private Transform questionPanel;
    [SerializeField] private Text questionText;

    private GameObject[] squares = new GameObject[25];

    private List<int> squareValues = new List<int>();
    private int divideNumber;
    private int dividedNumber;

    private int buttonValue;
    private int whichQuestion;
    private int trueAnswer;

    private bool isPressedButton = false;
    private GameObject nowSquare;

    [SerializeField] private GameObject endPanel;

    [SerializeField] private AudioSource audioSource;
    public AudioClip buttonSound;

    private HeartManager heartManager;
    private int health = 3;

    private PointManager pointManager;
    private string questionDifficultyLevel;

    private void Awake()
    {
        pointManager = Object.FindObjectOfType<PointManager>();

        endPanel.GetComponent<RectTransform>().localScale = Vector3.zero;

        audioSource = GetComponent<AudioSource>();

        heartManager = Object.FindObjectOfType<HeartManager>();
        heartManager.HeartControl(health);
    }

    private void Start()
    {
        questionPanel.GetComponent<RectTransform>().localScale = Vector3.zero;

        CreateSquares();
    }

    private void CreateSquares()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject square = Instantiate(squarePrefab, panelOfSquares);
            square.transform.GetComponent<Button>().onClick.AddListener(() => PressButton());
            squares[i] = square;
        }
        WriteSquareValues();
        StartCoroutine(DoFadeRoutine());
        Invoke("OpenQuestionPanel", 3f);
    }

    private void PressButton()
    {
        if (isPressedButton)
        {
            audioSource.PlayOneShot(buttonSound);

            buttonValue = int.Parse(
            UnityEngine.
            EventSystems.EventSystem.
            current.currentSelectedGameObject.
            transform.GetChild(0).
            GetComponent<Text>().text
            );

            nowSquare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            ControlAnswer();
        }
    }

    private void ControlAnswer()
    {
        if (buttonValue == trueAnswer)
        {
            nowSquare.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
            pointManager.UpPoint(questionDifficultyLevel);

            squareValues.RemoveAt(whichQuestion);

            if (squareValues.Count > 0)
            {
                OpenQuestionPanel();
            }
            else
            {
                EndGame();
            }

            Debug.Log(squareValues.Count);
        }
        else
        {
            Debug.Log("Yanlýþ Sonuç!!");
            health--;
            Debug.Log(health);
            heartManager.HeartControl(health);
        }

        if (health <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Oyun Bitti");
        isPressedButton = false;
        endPanel.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    private IEnumerator DoFadeRoutine()
    {
        foreach (var squareItem in squares)
        {
            squareItem.GetComponent<CanvasGroup>().DOFade(1, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void WriteSquareValues()
    {
        foreach (var item in squares)
        {
            int randomValue = Random.Range(1, 13);
            squareValues.Add(randomValue);
            item.transform.GetChild(0).GetComponent<Text>().text = randomValue.ToString();
        }
    }

    private void OpenQuestionPanel()
    {
        SetQuestion();
        questionPanel.GetComponent<RectTransform>().DOScale(1, 0.35f).SetEase(Ease.OutBack);
        isPressedButton = true;
    }

    private void SetQuestion()
    {
        divideNumber = Random.Range(2, 11);
        whichQuestion = Random.Range(0, squareValues.Count);
        trueAnswer = squareValues[whichQuestion];
        dividedNumber = divideNumber * trueAnswer;

        if (dividedNumber <= 40)
        {
            questionDifficultyLevel = "kolay";
        }
        else if (dividedNumber > 40 && dividedNumber <= 80)
        {
            questionDifficultyLevel = "orta";
        }
        else
        {
            questionDifficultyLevel = "zor";
        }

        questionText.text = dividedNumber + " : " + divideNumber;
    }
}
