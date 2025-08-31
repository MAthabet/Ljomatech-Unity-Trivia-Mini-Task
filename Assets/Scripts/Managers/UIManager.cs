using System.Collections.Generic;
using TMPro;    
using UnityEngine;

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
    [Tooltip("List of answer buttons in ORDER")]
    private List<AnswerButton> answerButtons;

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

    public void DisplayError(string message)
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
    public void ShowPanel(PanelType panel)
    {
        foreach (var p in panelsList)
        {
            if (p.type == panel)
            {
                p.panelObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Activating the target panel and deactivating the other screen panels
    /// </summary>
    public void ShowPanelOnly(PanelType panel)
    {
        foreach (var p in panelsList)
        {
            if (p.type == panel)
            {
                p.panelObject.SetActive(true);
            }
            else
            {
                p.panelObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Uodate question text and answer buttons
    /// </summary>
    public void UpdateQuizUI(Question question)
    {
        questionText.SetText(question.questionText);
        int n = answerButtons.Count;
        for (int i = 0; i < n; i++)
        {
            answerButtons[i].SetButtonText(question.answers[i]);
        }
    }

    public void ShowCorrectFeedback(int buttonIndex)
    {
        throw new System.NotImplementedException();
    }

    public void ShowWrongFeedback(int buttonIndex)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// reset quiz ui and answer buttons to default state and hide any feedback
    /// </summary>
    public void ResetFeedback()
    {
    }

    private void InitializeAnwerButtons()
    {
        int n = answerButtons.Count;
        if (n == 0)
        {
            Debug.LogWarning("answerButtons list is not assigned in the inspector");
            return;
        }
        for (int i = 0; i < n; i++)
        {
            answerButtons[i].ButtonInit(i);
        }
    }
}
