using System;

/// <summary>
/// A central, static class for managing game-wide events.
/// </summary>
public static class GameEvents
{
    // event that will be broadcast when an answer button is clicked.
    public static event Action OnStartQuizRequested;

    public static event Action<int> OnQuizStart;
    public static event Action<int> OnAnswerSelected;
    public static event Action<int, int, bool> OnAnswerProcessed;
    public static event Action<Question, int> OnQuestionUpdated;
    public static event Action<string> OnError;
    public static event Action<int> OnQuestionTimerExpired;
    public static event Action<bool> OnQuizEnd;
    public static event Action OnQuizReset;

    //public static event Action OnNextQuestionClicked;

    /// <summary>
    /// broadcast the start of the quiz (after parsing json file)
    /// </summary>
    public static void BroadcastQuizStart(int totalQuestionsNumber)
    {
        OnQuizStart?.Invoke(totalQuestionsNumber);
    }

    /// <summary>
    /// broadcast the start of the quiz (after parsing json file)
    /// </summary>
    public static void BroadcastAnswerFeedback(int questionIndex,int Answerindex, bool isAnswerCorrect)
    {
        OnAnswerProcessed?.Invoke(questionIndex, Answerindex, isAnswerCorrect);
    }

    /// <summary>
    /// broadcast the error message
    /// </summary>
    public static void ReportError(string message)
    {
        OnError?.Invoke(message);
    }

    /// <summary>
    /// broadcast the end of the quiz
    /// </summary>
    public static void BroadcastQuizEnd(bool hasPassed)
    {
        OnQuizEnd?.Invoke(hasPassed);
    }
    /// <summary>
    /// broadcast restarting of the quiz without being ended
    /// </summary>
    public static void BroadcastQuizRestart()
    {
        OnQuizReset?.Invoke();
    }

    /// <summary>
    /// Any AnswerButton can call this method to broadcast its index to all listeners.
    /// </summary>
    public static void BroadcastAnswerSelection(int index)
    {
        OnAnswerSelected?.Invoke(index);
    }

    /// <summary>
    /// start button call this method to broadcast the quiz start
    /// </summary>
    public static void RequestStartQuiz()
    {
        OnStartQuizRequested?.Invoke();
    }

    /// <summary>
    /// proadcast the new question text and index
    /// </summary>
    public static void BroadcastQuestionUpdate(Question currentQuestion, int questionIndex )
    {
        OnQuestionUpdated?.Invoke(currentQuestion, questionIndex);
    }

    public static void BroadcastQuestionTimerExpired(int questionIndex)
    {
        OnQuestionTimerExpired?.Invoke(questionIndex);
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