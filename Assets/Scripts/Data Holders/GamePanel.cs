using UnityEngine;

public enum PanelType
{
    StartPanel,
    QuizPanel,
    ResultPanel,
    ErrorPanel
}

[System.Serializable]
public class GamePanel
{
    [SerializeField]
    private PanelType type;
    [SerializeField]
    private CanvasGroup panelCanvasGroup;
    public PanelType Type { get { return type; } }

    /// <summary>
    /// make panel visable and interactable
    /// </summary>
    public void Show()
    {
        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.alpha = 1f;
            panelCanvasGroup.interactable = true;
            panelCanvasGroup.blocksRaycasts = true;
        }
    }
    /// <summary>
    /// make panel invisable and not interactable
    /// </summary>
    public void Hide()
    {
        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.alpha = 0f;
            panelCanvasGroup.interactable = false;
            panelCanvasGroup.blocksRaycasts = false;
        }
    }
}