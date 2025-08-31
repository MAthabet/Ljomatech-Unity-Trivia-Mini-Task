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
    private float timeToMoveToNextQuestion = 2f;
    [SerializeField]
    private int answersPerQuestion = 4;

    private List<Question> questionsList;
    private int score;
    private int maxScore;
    private int currentQuestionIndex;

    void OnEnable()
    {
        GameEvents.OnAnswerSelected += (answerindex) => HandleAnswerSelection(answerindex);
        GameEvents.OnStartQuizRequested += TryStartGame;
        //GameEvents.OnNextQuestionClicked += NextQuestion;
    }

    /// <summary>
    /// Try to load the questions and start the game, printing an error if failed
    /// </summary>
    private void TryStartGame()
    {
        try
        {
            LoadQuestionsFromJsonFile();
            PrepareQuiz();
            
            GameEvents.BroadcastQuizStart();
            Debug.Log($"Game started successfully with {maxScore} questions!");
        }
        catch (System.Exception e)
        {
            GameEvents.ReportError(e.Message);
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

        if (qArray == null || qArray.questions.Length == 0)
        {
            throw new System.Exception("JSON file is empty or does not contain a valid questions format");
        }

        questionsList = new List<Question>(qArray.questions);
    }

    /// <summary>
    ///  reset score and questions
    /// </summary>
    private void ResetQuiz()
    {
        PrepareQuiz();
        GameEvents.BroadcastQuizRestart();
    }
    private void PrepareQuiz()
    {
        score = 0;
        maxScore = questionsList.Count;
        ShuffleQuestions();
        ChangeCurrentQuestion(0);
    }
    private void ShuffleQuestions()
    {

    }
    private void NextQuestion()
    {
        ChangeCurrentQuestion(currentQuestionIndex + 1);
    }

    /// <summary>
    /// set the current question to the target index or end the quiz if the index is out of bounds
    /// </summary>
    /// <param name="targetQuestionIndex"></param>
    private void ChangeCurrentQuestion(int targetQuestionIndex)
    {
        currentQuestionIndex = targetQuestionIndex;

        if (currentQuestionIndex >= maxScore)
        {
            EndQuiz();
            return;
        }

        Question q = questionsList[currentQuestionIndex];
        GameEvents.BroadcastQuestionUpdate(q, currentQuestionIndex);
    }

    /// <summary>
    /// handle score and feedback when an answer is selected
    /// </summary>
    /// <param name="selectedIndex"></param>
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
            GameEvents.BroadcastAnswerFeedback(selectedIndex, true);
        }
        else
        {
            GameEvents.BroadcastAnswerFeedback(selectedIndex, false);
        }
    }
    private bool IsAnswerCorrect(int AnswerIndex)
    {
        Question q = questionsList[currentQuestionIndex];

        return q.correctAnswerIndex == AnswerIndex;
    }
    private void EndQuiz()
    {
        GameEvents.BroadcastQuizEnd(IsQuizPassed());
    }

    private bool IsQuizPassed()
    {
        if(maxScore <= 0)
        {
            Debug.LogError("error in maxScore");
            return false;
        }
        return (score / maxScore) > (percantageToPass / 100);
    }

    private void OnDisable()
    {
        GameEvents.OnAnswerSelected -= HandleAnswerSelection;
        GameEvents.OnQuizStart -= TryStartGame;
        //GameEvents.OnNextQuestionClicked -= NextQuestion;
    }
}