using UnityEngine;

/// <summary>
/// Static class to hold all the color constants for the game
/// </summary>
public static class GameColors
{
    // Primary UI Colors
    public static readonly Color PrimaryBlue = new Color32(0x08, 0x71, 0xCE, 0xFF); // #0871CE
    public static readonly Color PrimaryWhite = new Color32(0xF9, 0xFE, 0xFE, 0xFF); // #F9FEFE
    public static readonly Color DisabledGray = new Color32(0xAA, 0xAC, 0xAD, 0xFF); // #AAACAD

    // Text Colors
    public static readonly Color TextWhite = new Color32(0xFF, 0xFF, 0xFF, 0xFF); // #FFFFFF
    public static readonly Color TextGray = new Color32(0xAD, 0xAE, 0xAD, 0xFF); // #ADAEAD
    public static readonly Color TextBlue = new Color32(0x00, 0x71, 0xBD, 0xFF); // #0071BD

    // Feedback Colors 
    public static readonly Color CorrectAnswer = new Color32(0x00, 0x71, 0xB9, 0xFF); // #0071B9
    public static readonly Color WrongAnswer = new Color32(0xA5, 0x1C, 0x30, 0xFF); // #A51C30
}
