using System;

/// <summary>
/// class to hold all the color constants for the game
/// </summary>
[Serializable]
public class Question
{
    public string questionText;

    public string[] answers = new string[4];

    public int correctAnswerIndex;
}
