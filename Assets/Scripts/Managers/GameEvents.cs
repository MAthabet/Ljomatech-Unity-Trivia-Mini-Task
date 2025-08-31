using System;

/// <summary>
/// A central, static class for managing game-wide events.
/// </summary>
public static class GameEvents
{
    // event that will be broadcast when an answer button is clicked.
    public static event Action OnStartClicked;

    public static event Action OnQuizStart;
    public static event Action<int> OnAnswerSelected;
    public static event Action<int> OnCorrectAnswer;
    public static event Action<int> OnWrongAnswer;
    public static event Action<string> OnError;
    public static event Action OnQuizEnd;

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
    public static void FeedbackAnswer(int Answerindex, bool isAnswerCorrect)
    {
        if(isAnswerCorrect == true)
        {
            OnCorrectAnswer?.Invoke(Answerindex);
        }
        else 
        { 
            OnWrongAnswer?.Invoke(Answerindex);
        }
    }

    public static void Error(string message)
    {
        OnError?.Invoke(message);
    }

    /// <summary>
    /// broadcast the end of the quiz
    /// </summary>
    public static void EndQuiz()
    {
        OnQuizEnd?.Invoke();
    }

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
    public static void StartClicked()
    {
        OnStartClicked?.Invoke();
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