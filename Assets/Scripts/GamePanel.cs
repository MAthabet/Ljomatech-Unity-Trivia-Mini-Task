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
    public PanelType type;
    public GameObject panelObject;
}