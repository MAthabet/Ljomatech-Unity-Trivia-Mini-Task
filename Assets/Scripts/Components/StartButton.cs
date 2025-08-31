using UnityEngine;
using UnityEngine.UI;
public class StartButton : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(OnStartQuizClicked);
    }

    private void OnStartQuizClicked()
    {
        GameEvents.QuizStart();
    }
}
