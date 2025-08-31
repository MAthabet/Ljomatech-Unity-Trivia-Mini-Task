using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// component to hold the question tab data
/// </summary>
public class QuestionTab : MonoBehaviour
{
    [SerializeField]
    private TMP_Text tabNumberText;
    [SerializeField]
    private Image tabImage;
    [SerializeField]
    [Tooltip("use {0} to represent the question number")]
    private string tabTextFormat = "Questoin #{0}";

    private void Awake()
    {
        if(tabImage == null)
        {
            tabImage = GetComponent<Image>();
        }
        if (tabNumberText == null)
        {
            tabNumberText = GetComponentInChildren<TMP_Text>();
        }
    }
    /// <summary>
    /// set the tab background color
    /// </summary>
    /// <param name="bgColor"> background color <see cref="GameColors"/> </param>
    public void ChangeTabColor(Color bgColor)
    {
        tabImage.color = bgColor;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="color">select from <see cref="GameColors"/></param>
    public void ChangeTextColor(Color color)
    {
        tabNumberText.color = color;
    }
    /// <summary>
    /// set tab number in same text format <see cref="tabTextFormat"/>
    /// </summary>
    public void SetTabNumber(int number)
    {
        if (tabNumberText != null)
        {
            tabNumberText.SetText(tabTextFormat, number);
        }
        else
        {
            Debug.LogError("Tab Number Text Cannot be found");
        }
    }

    //TODO
    //Make tabs button to switch between questions
}
