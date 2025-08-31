using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System;

/// <summary>
///  class to control the quiz flow and results
/// </summary>
public class QuizManager : MonoBehaviour
{


    [SerializeField]
    [Tooltip("Component that implemnts IQuizUI")]
    private Component UiManagerComponent;

    [Header("Quiz Settings")]
    [SerializeField]
    private string questionsJSONFileName = "questions";
    [SerializeField]
    [Range(0f, 100f)]
    [Tooltip("% of correct answers required to win")]
    private float percantageToPass = 50;
    [SerializeField]
    [Tooltip("Time to answer each question (in seconds)")]
    private float timePerQuestion = 10f;
    [SerializeField]
    private float timeToRestartQuiz = 10f;
    [SerializeField]
    private int answersPerQuestion = 4;



    private IQuizUI uiManager;

    private List<Question> questionsList;
    private int score;
    private int currentQuestionIndex;

    void Awake()
    {
        if (UiManagerComponent == null)
        {
            Debug.LogWarning("UIManager component is not assigned");
        }
        else if (!UiManagerComponent.gameObject.TryGetComponent<IQuizUI>(out uiManager))
        {
            Debug.LogError("UIManager is not found");
            return;
        }
    }
    void OnEnable()
    {
        GameEvents.OnAnswerSelected += HandleAnswerSelection;
        GameEvents.OnQuizStart += TryStartGame;
    }

    /// <summary>
    /// Try to load the questions and start the game, printing an error if failed
    /// </summary>
    private void TryStartGame()
    {
        try
        {
            LoadQuestionsFromJsonFile();

            uiManager.ShowPanelOnly(PanelType.QuizPanel);

            PrepareQuiz();
            Debug.Log($"Game started successfully with {questionsList.Count} questions!");
            
        }
        catch (System.Exception e)
        {
            uiManager.DisplayError(e.Message);
        }
    }

    /// <summary>
    /// Load the questions from the json file in the Resources folder
    /// </summary>
    private void LoadQuestionsFromJsonFile()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(questionsJSONFileName);

        if (jsonFile == null)
        {
            throw new FileNotFoundException($"JSON file not found at 'Assets/Resources/{questionsJSONFileName}.json'");
        }

        QuestionsList qArray = JsonUtility.FromJson<QuestionsList>(jsonFile.text);
        questionsList = new List<Question>(qArray.questions);

        if (questionsList == null || questionsList.Count == 0)
        {
            throw new System.Exception("JSON file is empty or does not contain a valid questions format");
        }
    }

    /// <summary>
    ///  reset quiz and reshuffle questions
    /// </summary>
    private void PrepareQuiz()
    {
        score = 0;
        currentQuestionIndex = 0;
        //TODO:
        // shuffle questions
    }
    private void HandleAnswerSelection(int selectedIndex)
    {
        if (selectedIndex >= answersPerQuestion || selectedIndex < 0)
        {
            Debug.LogError("Answer Out Of Bounds");
            return;
        }
        if (IsAnswerCorrect(selectedIndex))
        {
            score++;
            uiManager.ShowCorrectFeedback(selectedIndex);
        }
        else
        {
            uiManager.ShowWrongFeedback(selectedIndex);
        }
    }
    private bool IsAnswerCorrect(int AnswerIndex)
    {
        Question q = questionsList[currentQuestionIndex];

        return q.correctAnswerIndex == AnswerIndex;
    }
    void OnDisable()
    {
        GameEvents.OnAnswerSelected -= HandleAnswerSelection;
        GameEvents.OnQuizStart -= TryStartGame;
    }
}