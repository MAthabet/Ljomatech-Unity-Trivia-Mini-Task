using System;
using UnityEngine;

/// <summary>
///  class to control all the sfx and music
/// </summary>
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [Header("Audio Clips")]
    [SerializeField]
    AudioClip correctAnswerClip;
    [SerializeField]
    AudioClip wrongAnswerClip;
    [SerializeField]
    AudioClip quizPassedClip;
    [SerializeField]
    AudioClip quizFailedClip;
    [SerializeField]
    AudioClip TimerTickClip;
    [SerializeField]
    AudioClip TimeUpClip;

    private void OnEnable()
    {
        GameEvents.OnQuizStart += HandleQuizStart;
        GameEvents.OnQuizEnd += HandleQuizEnd;
        GameEvents.OnAnswerProcessed += HandleProcessedAnswer;
        GameEvents.OnQuestionTimerExpired += OnQuestionTimerExpired;
        GameEvents.OnQuestionUpdated += HandleQuizUpdated;
        GameEvents.OnQuizReset += HandleQuizReset;
    }
    private void HandleQuizReset()
    {
        playSound(TimerTickClip, true);
    }

    private void HandleQuizStart(int foo)
    {
        playSound(TimerTickClip, true);
    }

    private void HandleQuizUpdated(Question arg1, int arg2)
    {
        playSound(TimerTickClip, true);
    }
    private void OnQuestionTimerExpired(int obj)
    {
        playSound(TimeUpClip);
    }

    
    private void HandleQuizEnd(bool hasPassed)
    {
        if(hasPassed)
        {
            playSound(quizPassedClip);
        }
        else
        {
            playSound(quizFailedClip);
        }
    }
    private void HandleProcessedAnswer(int arg1, int arg2, bool isCorrect)
    {
        if (isCorrect)
        {
            playSound(correctAnswerClip);
        }
        else
        {
            playSound(wrongAnswerClip);
        }
    }

    /// <summary>
    /// play audio clip (looping or not) on the audio source alone
    /// </summary>
    private void playSound(AudioClip clip, bool isLooping = false)
    {
        if (audioSource == null || clip == null) return;

        StopAllSounds();
        audioSource.clip = clip;
        audioSource.loop = isLooping;
        audioSource.Play();
    }

    private void StopAllSounds()
    {
        audioSource.Stop();
    }

    private void OnDisable()
    {
        GameEvents.OnQuizStart -= HandleQuizStart;
        GameEvents.OnQuizEnd -= HandleQuizEnd;
        GameEvents.OnAnswerProcessed -= HandleProcessedAnswer;
        GameEvents.OnQuestionTimerExpired -= OnQuestionTimerExpired;
        GameEvents.OnQuestionUpdated -= HandleQuizUpdated;
        GameEvents.OnQuizReset -= HandleQuizReset;
    }
}
