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



    private IQuizUI uiManager;

    private QuestionsList questionsList;
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
            Debug.Log($"Game started successfully with {questionsList.questions.Length} questions!");
            
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

        questionsList = JsonUtility.FromJson<QuestionsList>(jsonFile.text);

        if (questionsList == null || questionsList.questions.Length == 0)
        {
            throw new System.Exception("JSON file is empty or does not contain a valid questions format");
        }
    }
    private void HandleAnswerSelection(int selectedAnswerIndex)
    {

    }
    private void PrepareQuiz()
    {
        score = 0;
        currentQuestionIndex = 0;
        //TODO:
        // shuffle questions
    }
    void OnDisable()
    {
        GameEvents.OnAnswerSelected -= HandleAnswerSelection;
        GameEvents.OnQuizStart -= TryStartGame;
    }
}