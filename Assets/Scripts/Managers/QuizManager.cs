using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;

/// <summary>
///  class to control the quiz flow and results
/// </summary>
public class QuizManager : MonoBehaviour
{
    const string QUESTIONS_FILE_NAME = "questions"; 

    [SerializeField]
    private UIManager uiManager;

    private QuestionsList questionsList;
    private int score;
    private int currentQuestionIndex;

    void Awake()
    {
        if (uiManager == null)
        {
            Debug.LogWarning("UIManager is not assigned in the inspector");

            if(!TryGetComponent<UIManager>(out uiManager))
            {
                Debug.LogError("UIManager is not found");
                return;
            }
        }
    }

    /// <summary>
    /// Try to load the questions and start the game, printing an error if failed
    /// </summary>
    public void TryStartGame()
    {
        try
        {
            LoadQuestionsFromJsonFile();

            uiManager.ShowPanelOnly(PanelType.QuizPanel);

            Debug.Log($"Game started successfully with {questionsList.questions.Length} questions!");
            //TODO:
            // shuffle questions and show the first one
        }
        catch (System.Exception e)
        {
            uiManager.PrintError(e.Message);
        }
    }

    /// <summary>
    /// Load the questions from the json file in the Resources folder
    /// </summary>
    private void LoadQuestionsFromJsonFile()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(QUESTIONS_FILE_NAME);

        if (jsonFile == null)
        {
            throw new FileNotFoundException($"JSON file not found at 'Assets/Resources/{QUESTIONS_FILE_NAME}.json'");
        }

        questionsList = JsonUtility.FromJson<QuestionsList>(jsonFile.text);

        if (questionsList == null || questionsList.questions.Length == 0)
        {
            throw new System.Exception("JSON file is empty or does not contain a valid questions format");
        }
    }

}