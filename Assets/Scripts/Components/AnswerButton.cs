using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// component to hold the answer button data and events
/// </summary>
[RequireComponent(typeof(Button))]
public class AnswerButton : MonoBehaviour
{
    private Button button;
    private int buttonIndex = -1;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    /// <summary>
    /// intialize answer button with its index and add the click event listener
    /// </summary>
    /// <param name="index"> answer index to return when clicked </param>
    public void AnswerButtonInit(int index)
    {
        buttonIndex = index;
        button.onClick.AddListener(OnAnswerSelected);
    }

    void OnAnswerSelected()
    {
        GameEvents.AnswerSelected(buttonIndex);
    }

}
