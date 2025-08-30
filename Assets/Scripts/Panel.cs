using UnityEngine;

public enum PanelType
{
    StartPanel,
    QuizPanel,
    ResultPanel,
    ErrorPanel
}

[System.Serializable]
public class Panel
{
    public PanelType type;
    public GameObject panelObject;
}