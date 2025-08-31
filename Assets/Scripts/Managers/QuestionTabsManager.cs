using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  class to control question progress tabs
/// </summary>
public class QuestionTabsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject questionTabPrefab;
    [SerializeField]
    private GameObject tabsContent;

    private List<QuestionTab> tabs = new List<QuestionTab>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void OnEnable()
    {
        GameEvents.OnQuizStart += OnQuizLoaded;
        GameEvents.OnAnswerProcessed += HandleProcessedAnswer;
        GameEvents.OnQuestionTimerExpired += HandleTimeUp;
        GameEvents.OnQuizReset += ResetTabs;
    }

    private void HandleTimeUp(int questionIndex)
    {
        FeedbackTab(questionIndex, false);
    }
    private void HandleProcessedAnswer(int questionIndex, int foo, bool isCorrect)
    {
        FeedbackTab(questionIndex, isCorrect);
    }
    /// <summary>
    /// mark the tab as wrong or correct
    /// </summary>
    private void FeedbackTab(int questionIndex, bool isCorrect)
    {
        if (questionIndex < 0 || questionIndex >= tabs.Count)
        {
            Debug.LogError("questionIndex out of range in OnAnswerProcessed event");
            return;
        }
        //tabs list is enverted
        int tabIndex = tabs.Count - 1 - questionIndex;
        if (isCorrect)
        {
            tabs[tabIndex].ChangeTabColor(GameColors.CorrectAnswer);
        }
        else
        {
            tabs[tabIndex].ChangeTabColor(GameColors.WrongAnswer);
        }
        tabs[tabIndex].ChangeTextColor(GameColors.TextWhite);
    }

    /// <summary>
    /// populate tabs scroll content
    /// </summary>
    /// <param name="totalQuestionNumber">for tabs init</param>
    private void OnQuizLoaded(int totalQuestionNumber)
    {
        if (tabsContent == null)
        {
            Debug.LogError("assign tabs content in the inspector");
            return;
        }
        DestroyAllChildren();
        // to make the first question tab in the end of the scroll view
        for (int i = totalQuestionNumber - 1; i >= 0; i--)
        {
            GameObject tab = Instantiate(questionTabPrefab, tabsContent.transform);
            QuestionTab questionTab = tab.GetComponent<QuestionTab>();
            questionTab.SetTabNumber(i + 1);
            tabs.Add(questionTab);
        }
    }
    /// <summary>
    /// reset tabs to original grayed out form
    /// </summary>
    private void ResetTabs()
    {
        foreach(QuestionTab tab in tabs)
        {
            tab.ChangeTextColor(GameColors.TextGray);
            tab.ChangeTabColor(GameColors.PrimaryWhite);
        }
    }
    public void DestroyAllChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = tabsContent.transform.GetChild(i);
            
            Destroy(child.gameObject);
        }
        tabs.Clear();
    }

    private void OnDisable()
    {
        GameEvents.OnQuizStart -= OnQuizLoaded;
        GameEvents.OnAnswerProcessed -= HandleProcessedAnswer;
        GameEvents.OnQuestionTimerExpired -= HandleTimeUp;
        GameEvents.OnQuizReset -= ResetTabs;
    }
}
