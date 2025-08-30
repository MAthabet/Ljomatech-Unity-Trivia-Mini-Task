using UnityEngine;

/// <summary>
/// Static class to hold all the color constants for the game
/// </summary>
public static class GameColors
{
    // Primary UI Colors
    public static readonly Color PrimaryBlue = new Color (0x08, 0x71, 0xCE); // #0871CE
    public static readonly Color PrimaryWhite = new Color (0xF9, 0xFE, 0xFE); // #F9FEFE
    public static readonly Color DisabledGray = new Color (0xAA, 0xAC, 0xAD); // #AAACAD

    // Text Colors
    public static readonly Color TextWhite = new Color (0xFF, 0xFF, 0xFF); // #FFFFFF
    public static readonly Color TextGray = new Color (0xAD, 0xAE, 0xAD); // #ADAEAD
    public static readonly Color TextBlue = new Color (0x00, 0x71, 0xBD); // #0071BD

    // Feedback Colors 
    public static readonly Color CorrectAnswer = new Color (0x00, 0x71, 0xB9); // #0071B9
    public static readonly Color WrongAnswer = new Color (0xA5, 0x1C, 0x30); // #A51C30
}
