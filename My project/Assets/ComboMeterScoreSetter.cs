using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class ComboMeterScoreSetter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _beatScoreMeters;
    [SerializeField] private Animator _animator;
    private float _standardWidth;
    private float _currentScore;

    private float Miniscore;

    public List<ErrorMargins> ErrorMargins;

    [SerializeField] private TextPopUp _textPopUp;
    [SerializeField] private GameObject _spawnradius;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _standardWidth = _beatScoreMeters[0].size.x;
        _currentScore = 0.5f;
        SetScore(_currentScore);
        GameManager.Instance.metronome.Beat += Beat;

    }

    private void SetScore(float percentage)
    {

        foreach (var meter in _beatScoreMeters)
        {
            meter.size = new Vector2(_standardWidth * percentage, meter.size.y);
        }
    }

     public float GetPointsForTiming()
    {
        float progress = GameManager.Instance.metronome.ProgressInTick();
        Debug.Log("Progress");
        progress = 1-Mathf.Abs(progress-0.5f)*2;
        float margin = 0;
        foreach(var marginBrackets in ErrorMargins)
        {
            margin += marginBrackets.Margin;
            if (progress <= margin)
            {
                return marginBrackets.Points;
            }
        }
   
        return -5;
   }    

    public void AnimationLowerScore()
    {
        _currentScore -= 0.05f;
        SetScore(_currentScore);
    }

    public void LowerScoreAnticipation()
    {
        _animator.Play("ComboMeterHit");
    }

    public void HigherScore()
    {
        _currentScore += 0.1f;
        SetScore(_currentScore);
    }

    protected void Beat(object sender, EventArgs e)
    {
        if (enabled)
        {
            ScoreDecay();

            if(_currentScore<0)
            GameManager.Instance.RemoveLife();
        }
    }

    private void ScoreDecay()
    {
        Miniscore -= 5;
        SortMiniScore();
    }

    internal void AddActionscore()
    {
        float points = GetPointsForTiming();
        Debug.Log(points);
        Miniscore += points;
        SortMiniScore();


        Vector3 position = _spawnradius.gameObject.transform.position + UnityEngine.Random.Range(-0.5f,0.5f)*_spawnradius.transform.localScale.x *new Vector3(1,0)
            + UnityEngine.Random.Range(-0.5f, 0.5f) * _spawnradius.transform.localScale.y * new Vector3(0, 1);

        GameObject text = GameObject.Instantiate(_textPopUp.gameObject, position, Quaternion.identity);
        //text.transform.SetParent(_spawnradius.transform,true);
        //text.transform.localScale = new Vector3(1f/text.transform.parent.localScale.x,
        //    1f / text.transform.parent.localScale.y, 1f / text.transform.parent.localScale.z);
        //
        TextPopupMood textPopupMood = TextPopupMood.Neutral;
        if (points > 0)
        {
            textPopupMood = TextPopupMood.Positive;
            if (points > 10)
                textPopupMood = TextPopupMood.VeryPositive;

        }
        else
        {
            if (points < 0)
            {
                textPopupMood = TextPopupMood.Negative;

            }
        }

        text.GetComponent<TextPopUp>().SetPopup($"{points}", textPopupMood);
    }

    internal void SortMiniScore()
    {
        if (Miniscore > 10)
        {
            HigherScore();
            Miniscore -= 10;
        }
        if (Miniscore < -10)
        {
            LowerScoreAnticipation();
            Miniscore += 10;
        }
    }

    internal void RemoveLifeScore()
    {
        AnimationLowerScore();
        LowerScoreAnticipation();
    }

    public void ScoreFine(int fine)
    {
        Miniscore -= fine;
        SortMiniScore();
    }

    internal void Finished()
    {
        Miniscore += 30;
        SortMiniScore();


        Vector3 position = _spawnradius.gameObject.transform.position + UnityEngine.Random.Range(-0.5f, 0.5f) * _spawnradius.transform.localScale.x * new Vector3(1, 0)
            + UnityEngine.Random.Range(-0.5f, 0.5f) * _spawnradius.transform.localScale.y * new Vector3(0, 1);

        GameObject text = GameObject.Instantiate(_textPopUp.gameObject, position, Quaternion.identity);
        //text.transform.SetParent(_spawnradius.transform,true);
        //text.transform.localScale = new Vector3(1f/text.transform.parent.localScale.x,
        //    1f / text.transform.parent.localScale.y, 1f / text.transform.parent.localScale.z);
        //
        TextPopupMood textPopupMood = TextPopupMood.VeryPositive;

        

        text.GetComponent<TextPopUp>().SetPopup($"Finished +{30}", textPopupMood);
    }

    internal void ManuelResetFine()
    {
        Miniscore -= 15;
        SortMiniScore();


        Vector3 position = _spawnradius.gameObject.transform.position + UnityEngine.Random.Range(-0.5f, 0.5f) * _spawnradius.transform.localScale.x * new Vector3(1, 0)
            + UnityEngine.Random.Range(-0.5f, 0.5f) * _spawnradius.transform.localScale.y * new Vector3(0, 1);

        GameObject text = GameObject.Instantiate(_textPopUp.gameObject, position, Quaternion.identity);
        //text.transform.SetParent(_spawnradius.transform,true);
        //text.transform.localScale = new Vector3(1f/text.transform.parent.localScale.x,
        //    1f / text.transform.parent.localScale.y, 1f / text.transform.parent.localScale.z);
        //
        TextPopupMood textPopupMood = TextPopupMood.Negative;



        text.GetComponent<TextPopUp>().SetPopup($"Reset fine! -{15}", textPopupMood);
    }

    internal void GameOver()
    {
       


        Vector3 position = _spawnradius.gameObject.transform.position + UnityEngine.Random.Range(-0.5f, 0.5f) * _spawnradius.transform.localScale.x * new Vector3(1, 0)
            + UnityEngine.Random.Range(-0.5f, 0.5f) * _spawnradius.transform.localScale.y * new Vector3(0, 1);

        GameObject text = GameObject.Instantiate(_textPopUp.gameObject, position, Quaternion.identity);
        //text.transform.SetParent(_spawnradius.transform,true);
        //text.transform.localScale = new Vector3(1f/text.transform.parent.localScale.x,
        //    1f / text.transform.parent.localScale.y, 1f / text.transform.parent.localScale.z);
        //
        TextPopupMood textPopupMood = TextPopupMood.Negative;



        text.GetComponent<TextPopUp>().SetPopup($"Game Over!", textPopupMood);
    }
}


[Serializable]
public class ErrorMargins
{
    public float Margin;
    public float Points;
}