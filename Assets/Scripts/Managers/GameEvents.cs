using System;

/// <summary>
/// A central, static class for managing game-wide events.
/// </summary>
public static class GameEvents
{
    // event that will be broadcast when an answer button is clicked.
    public static event Action OnStartGameRequested;

    public static event Action OnQuizStart;
    public static event Action<int> OnAnswerSelected;
    public static event Action<bool, int> OnAnswerProcessed;
    public static event Action<string> OnError;
    public static event Action<bool> OnQuizEnd;

    //public static event Action OnNextQuestionClicked;

    /// <summary>
    /// broadcast the start of the quiz (after parsing json file)
    /// </summary>
    public static void StartQuiz()
    {
        OnQuizStart?.Invoke();
    }

    /// <summary>
    /// broadcast the start of the quiz (after parsing json file)
    /// </summary>
    public static void ProcessAnswer(int Answerindex, bool isAnswerCorrect)
    {
        OnAnswerProcessed?.Invoke(isAnswerCorrect, Answerindex);
    }

    public static void ReportError(string message)
    {
        OnError?.Invoke(message);
    }

    /// <summary>
    /// broadcast the end of the quiz
    /// </summary>
    public static void EndQuiz(bool hasPassed)
    {
        OnQuizEnd?.Invoke(hasPassed);
    }

    /// <summary>
    /// Any AnswerButton can call this method to broadcast its index to all listeners.
    /// </summary>
    public static void SelectAnswer(int index)
    {
        OnAnswerSelected?.Invoke(index);
    }

    /// <summary>
    /// start button call this method to broadcast the quiz start
    /// </summary>
    public static void RequestStartGame()
    {
        OnStartGameRequested?.Invoke();
    }

    /// <summary>
    /// 
    /// </summary>
    


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