using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboTracker : MonoBehaviour
{
    int _score = 0;
    [SerializeField] private TextMeshPro[] _scoreTrackers;
    [SerializeField] private TextMeshPro _streak;

    [SerializeField] private Animator[] _animator;

    int _currentStreak = 0;
    int _missInSequence = 0;
    [SerializeField] private Color[] _colorList;


    void Awake()
    {
        GameManager.Instance.metronome.Beat += Beat;

    }


    protected void Beat(object sender, EventArgs e)
    {
        if (_animator[0].GetCurrentAnimatorStateInfo(0).IsName("ComboTrackerGeneral"))
        {
            foreach (var animator in _animator)
            {
                animator.Play("ComboTrackerBeat");
            }
        }

        _missInSequence++;
        if (_missInSequence > 3)
        {
            _currentStreak = 0;
        }
        
        UpdateStreak();
        UpdateScore();

    }

    private void UpdateStreak()
    {
        int progress = Mathf.Clamp(_currentStreak/2, 0, _colorList.Length - 1);
        _streak.text = $"{_currentStreak}.00x";
        _streak.color = _colorList[progress];
        _streak.fontSize = Mathf.Lerp(4f,9f, (float)progress/(float)_colorList.Length);
    }

    public void GotPositiveScore(int score)
    {
        _missInSequence = 0;
        _currentStreak++;
        _score += score * _currentStreak;


    }

    public void GotNegative()
    {
        _currentStreak = 0;

    }

    private void UpdateScore()
    {
        foreach (var tracker in _scoreTrackers)
        {
            tracker.text = $"{_score}";
        }
    }
}
