using System.Security.Cryptography;
using UnityEngine;

public interface IQuizUI
{
    void ShowPanel(PanelType panel);
    void ShowPanelOnly(PanelType panel);
    void DisplayError(string message);
}
