using System;

/// <summary>
/// A central, static class for managing game-wide events.
/// </summary>
public static class GameEvents
{
    // event that will be broadcast when an answer button is clicked.
    public static event Action<int> OnAnswerSelected;
    public static event Action OnQuizStart;
    //public static event Action OnNextQuestionClicked;

    /// <summary>
    /// Any AnswerButton can call this method to broadcast its index to all listeners.
    /// </summary>
    public static void AnswerSelected(int index)
    {
        OnAnswerSelected?.Invoke(index);
    }
    /// <summary>
    /// start button call this method to broadcast the quiz start
    /// </summary>
    public static void QuizStart()
    {
        OnQuizStart?.Invoke();
    }

    /*
    /// <summary>
    /// Next Question Button Call This methosd to broadcast the next question event
    /// </summary>
    public static void NextQuestion()
    {
        OnNextQuestionClicked?.Invoke();
    }
    */
}