using System.Collections.Generic;
using TMPro;    
using UnityEngine;

/// <summary>
///  class to control all the game ui elemnts 
/// </summary>
public class UIManager : MonoBehaviour, IQuizUI
{
    [Header("UI Elemnts")]
    [SerializeField]
    private List<GamePanel> panelsList;
    [SerializeField]
    private TMP_Text errorText;

    private Dictionary<PanelType, GameObject> panelsDictionary;

    void Start()
    {
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

    public void UpdateQuizUI(Question question)
    {
        throw new System.NotImplementedException();
    }

    public void ShowCorrectFeedback(int buttonIndex)
    {
        throw new System.NotImplementedException();
    }

    public void ShowWrongFeedback(int buttonIndex)
    {
        throw new System.NotImplementedException();
    }

    public void ResetFeedback()
    {
        throw new System.NotImplementedException();
    }
}
