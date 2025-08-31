using System;
using System.Collections.Generic;
using TMPro;    
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  class to control all the game ui elemnts 
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("UI Elemnts")]
    [SerializeField]
    private List<GamePanel> panelsList;
    [SerializeField]
    private TMP_Text errorText;
    [SerializeField]
    private TMP_Text questionText;
    [SerializeField]
    private TMP_Text currentQuestionNumberText;
    [SerializeField]
    private Image questionBubble;
    [SerializeField]
    [Tooltip("List of answer buttons in ORDER")]
    private List<AnswerButton> answerButtons;

    [Header("Feedback Elemnts")]
    [SerializeField]
    private GameObject correctAnswerPanel;
    [SerializeField]
    private GameObject wrongAnswerPanel;
    [SerializeField]
    private GameObject winningPanel;
    [SerializeField]
    private GameObject losingPanel;

    [Header("Customization")]
    [SerializeField]
    [Tooltip("Curretn Question number text format (keep {0} as it the number placeholder)")]
    private string QuestionNumberTextformat = "Question\n#{0}";

    private int answerBtnsCount;
    //private Dictionary<PanelType, GameObject> panelsDictionary;

    void Start()
    {
        InitializeAnwerButtons();

        if (panelsList == null || panelsList.Count == 0)
        {
            Debug.LogError("panelsList not assigned in the inspector");
            return;
        }

        ShowPanelOnly(PanelType.StartPanel);
        HideFeedbackPanels();

        if (errorText == null)
        {
            Debug.LogWarning("errorText not assigned in the inspector");
        }
        
    }

    //private void InitializePanelsDictionary()
    //{
    //    panelsDictionary = new Dictionary<PanelType, GameObject>();
    //    foreach (var p in panelsList)
    //    {
    //        panelsDictionary.Add(p.type, p.panelObject);
    //    }
    //}
    private void OnEnable()
    {
        GameEvents.OnQuizStart += () => ShowPanelOnly(PanelType.QuizPanel);
        GameEvents.OnQuizReset += ResetQuizUI;
        GameEvents.OnQuizEnd += (hasPassed) => DisplayQuizResult(hasPassed);

        GameEvents.OnAnswerProcessed += (isCorrect, answerIndex) => ShowFeedback(answerIndex, isCorrect);
        GameEvents.OnQuestionUpdated += (currentQuestion, currntQuestionNumber) => OnQuestionUpdated(currentQuestion, currntQuestionNumber);

        GameEvents.OnError += (errorMsg) => DisplayError(errorMsg);
    }

    private void ResetQuizUI()
    {
        throw new NotImplementedException();
    }

    private void DisplayError(string message)
    {
        ShowPanel(PanelType.ErrorPanel);

        if (errorText != null)
        {
            errorText.SetText(message);
        }
    }

    /// <summary>
    /// Activating the target panel without deactivating any other panel
    /// </summary>
    private void ShowPanel(PanelType panel)
    {
        foreach (var p in panelsList)
        {
            if (p.Type == panel)
            {
                p.Show();
            }
        }
    }

    /// <summary>
    /// Activating the target panel and deactivating the other screen panels
    /// </summary>
    private void ShowPanelOnly(PanelType panel)
    {
        foreach (var p in panelsList)
        {
            if (p.Type == panel)
            {
                p.Show();
            }
            else
            {
                p.Hide();
            }
        }
    }

    /// <summary>
    /// Uodate question text and answer buttons
    /// </summary>
    /// <param name="questionNumber">0 indexed</param>
    private void UpdateQuizUI(Question question, int questionNumber)
    {
        questionText.SetText(question.questionText);
        for (int i = 0; i < answerBtnsCount; i++)
        {
            answerButtons[i].SetButtonText(question.answers[i]);
        }
        // questionNumber starts from 0
        currentQuestionNumberText.SetText(QuestionNumberTextformat, questionNumber + 1);
    }


    /// <summary>
    /// display answer feedback on the answer button and question bubble
    /// </summary>
    private void ShowFeedback(int answerIndex, bool isCorrect)
    {
        ChangeQuestionBubbleColor(GameColors.DisabledGray, GameColors.TextGray);
        ShowFeedbackOnAnswerButtons(answerIndex, isCorrect);
        ShowFeedbackMarks(isCorrect);
    }
    /// <summary>
    /// deactivating all answer buttons and showing feedback on the selected answer button
    /// </summary>
    private void ShowFeedbackOnAnswerButtons(int answerIndex, bool isCorrect)
    {
        for (int i = 0; i < answerBtnsCount; i++)
        {
            AnswerButton temp = answerButtons[i];
            temp.SetBtnInteractablity(false);

            if (i == answerIndex)
            {
                temp.SetTextColor(GameColors.TextWhite);
                if (isCorrect)
                {
                    temp.SetButtonColor(GameColors.CorrectAnswer);
                }
                else
                {
                    temp.SetButtonColor(GameColors.WrongAnswer);
                }
            }
            else
            {
                temp.SetTextColor(GameColors.TextGray);
                temp.SetButtonColor(GameColors.DisabledGray);
            }
        }
    }

    private void ShowFeedbackMarks(bool isCorrect)
    {
        if(isCorrect)
        {
            correctAnswerPanel.SetActive(true);
        }
        else
        {
            wrongAnswerPanel.SetActive(true);
        }
    }

    private void EnablingAllAnswerButtons()
    {
        foreach(var btn in answerButtons)
        {
            btn.SetBtnInteractablity(true);
            btn.SetButtonColor(GameColors.PrimaryBlue);
            btn.SetTextColor(GameColors.TextBlue);
        }
    }

    /// <summary>
    /// change question bubble background and text color
    /// </summary>
    /// <param name="bgColor"><see cref="GameColors"/></param>
    private void ChangeQuestionBubbleColor(Color bgColor, Color txtColor)
    {
        questionBubble.color = bgColor;
        questionText.color = txtColor;
    }

    /// <summary>
    /// reset quiz ui and answer buttons to default state and hide any feedback
    /// </summary>
    private void ResetFeedback()
    {
        ChangeQuestionBubbleColor(GameColors.PrimaryBlue, GameColors.TextBlue);
        HideFeedbackPanels();
        EnablingAllAnswerButtons();
    }

    private void HideFeedbackPanels()
    {
        correctAnswerPanel.SetActive(false);
        wrongAnswerPanel.SetActive(false);
    }

    private void OnQuestionUpdated(Question q, int currntQuestionNumber)
    {
        ResetFeedback();
        UpdateQuizUI(q, currntQuestionNumber);
    }
    private void InitializeAnwerButtons()
    {
        answerBtnsCount = answerButtons.Count;

        for (int i = 0; i < answerBtnsCount; i++)
        {
            answerButtons[i].ButtonInit(i);
        }
        if (answerBtnsCount == 0)
        {
            Debug.LogWarning("answerButtons list is not assigned in the inspector");
            return;
        }
    }
    private void DisplayQuizResult(bool hasPassed)
    {
        ShowPanelOnly(PanelType.ResultPanel);
        if(hasPassed)
        {
            winningPanel.SetActive(true); 
            losingPanel.SetActive(false);
        }
        else
        {
            winningPanel.SetActive(false); 
            losingPanel.SetActive(true);
        }
    }

    private void OnDisable()
    {
        GameEvents.OnQuizStart -= () => ShowPanelOnly(PanelType.QuizPanel);
        GameEvents.OnQuizReset -= ResetQuizUI;
        GameEvents.OnQuizEnd -= (hasPassed) => DisplayQuizResult(hasPassed);

        GameEvents.OnAnswerProcessed -= (isCorrect, answerIndex) => ShowFeedback(answerIndex, isCorrect);
        GameEvents.OnQuestionUpdated -= (currentQuestion, currntQuestionNumber) => OnQuestionUpdated(currentQuestion, currntQuestionNumber);

        GameEvents.OnError -= (errorMsg) => DisplayError(errorMsg);
    }
}
