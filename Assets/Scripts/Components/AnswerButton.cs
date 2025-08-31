using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// component to hold the answer button data and events
/// </summary>
[RequireComponent(typeof(Button))]
public class AnswerButton : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private TMP_Text answerText;
    [SerializeField]
    private Image btnBackground;
    [SerializeField]
    private Image btnOutline;
    private int buttonIndex = -1;

    void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }
        if (answerText == null)
        {
            answerText = GetComponentInChildren<TMP_Text>();
        }
        if (btnBackground == null)
        {
            btnBackground = GetComponentInChildren<Image>();
        }
        if (btnOutline == null)
        {
            btnOutline = GetComponent<Image>();
        }
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

    /// <summary>
    /// set the answer background color
    /// </summary>
    /// <param name="bgColor"> background color <see cref="GameColors"/> </param>
    /// 
    public void SetButtonColor(Color bgColor, Color outlineColor)
    {
        if (btnOutline != null && btnBackground != null)
        {
            btnOutline.color = outlineColor;
            btnBackground.color = bgColor;
        }
        else
        {
            Debug.LogError("Button Image Cannot be found");
        }
    }
    /// <summary>
    /// set the answer text color
    /// </summary>
    /// <param name="color"> <see cref="GameColors"/> </param>
    public void SetTextColor(Color color)
    {
        if (answerText != null)
        {
            answerText.color = color;
        }
        else
        {
            Debug.LogError("Button Text Cannot be found");
        }
    }
    /// <summary>
    /// set if the answer button can be interacted with
    /// </summary>
    public void SetBtnInteractablity(bool canInteract)
    {
        button.interactable = canInteract;
    }
    void OnAnswerSelected()
    {
        GameEvents.BroadcastAnswerSelection(buttonIndex);
    }

}
