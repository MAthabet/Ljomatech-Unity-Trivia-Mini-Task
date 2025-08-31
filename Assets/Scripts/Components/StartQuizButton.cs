using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// component to handle start quiz button events
/// </summary>
[RequireComponent(typeof(Button))]
public class StartQuizButton : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(OnStartQuizClicked);
    }

    private void OnStartQuizClicked()
    {
        GameEvents.RequestStartQuiz();
    }
}
