using System.Collections.Generic;
using TMPro;    
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Elemnts")]
    [SerializeField]
    private List<Panel> panelsList;
    [SerializeField]
    private TMP_Text errorText;

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

    public void PrintError(string message)
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


}
