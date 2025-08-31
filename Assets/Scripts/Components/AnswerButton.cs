using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// component to hold the answer button data and events
/// </summary>
[RequireComponent(typeof(Button))]
public class AnswerButton : MonoBehaviour
{
    private Button button;
    private TMP_Text answerText;
    private int buttonIndex = -1;

    void Awake()
    {
        button = GetComponent<Button>();
        answerText = GetComponentInChildren<TMP_Text>();
    }

    /// <summary>
    /// intialize answer button with its index and add the click event listener
    /// </summary>
    /// <param name="index"> answer index to return when clicked </param>
    public void ButtonInit(int index)
    {
        buttonIndex = index;
        button.onClick.AddListener(OnAnswerSelected);
    }
    public void SetButtonText(string text)
    {
        if (answerText != null)
        {
            answerText.SetText(text);
        }
        else
        {
            Debug.LogError("Button Text Cannot be found");
        }

    }

    void OnAnswerSelected()
    {
        GameEvents.SelectAnswer(buttonIndex);
    }

}
