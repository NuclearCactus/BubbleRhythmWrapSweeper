using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CustomGridScript CustomGridScript;

    [SerializeField, Header("Audio Settings")] private AudioSource _bubblePop;
    [SerializeField] private AudioSource _bubbleExplosion;
    [SerializeField] private AudioSource _mouseClick;
    [SerializeField] private AudioSource _bubbleBlow;
    [SerializeField] private AudioSource _bubbleFail;
    [SerializeField] private AudioSource _flagSound;





    private float _shakeDuration = 0f;
    private float _maxShakeDuration = 0f;

    [SerializeField, Header("Life Settings")] private int _maxLives = 3;
    private int _lives = 3;
    [SerializeField] private Volume _postprocessing;



    [SerializeField, Header("Camerashake Settings")] private float _shakeMagnitude = 0.7f;
    [SerializeField] private float _dampingSpeed = 1.0f;
    [SerializeField] private GameObject _camera;
    private Vector3 _initialCameraPosition;
    [SerializeField] private AnimationCurve _cameraYShake;
    [SerializeField] private AnimationCurve _cameraRotationShake;

    [SerializeField, Header("Juice")] private Animator _restartButton;
    public MetroGnome metronome;
    public ComboMeterScoreSetter ScoreSetter;
    public ComboTracker ComboTracker;

    private int _unflagAmount;


    [SerializeField] private GameObject _gameOverScreen;

    // update pls
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        _lives = _maxLives;
        _initialCameraPosition = _camera.transform.localPosition;
        Time.timeScale = 1;

    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PlayMouseClick();
        }
        if (_shakeDuration > 0)
        {
            _camera.transform.localPosition = _initialCameraPosition + _camera.transform.up * _shakeMagnitude * _cameraYShake.Evaluate(1 - (_shakeDuration / _maxShakeDuration)/5);
            _camera.transform.eulerAngles = new Vector3(0, 0, _shakeMagnitude * _cameraRotationShake.Evaluate(1 - (_shakeDuration / _maxShakeDuration) ) * 10);
            //yeah
            _shakeDuration -= Time.deltaTime;
        }
        else
        {
            _shakeDuration = 0f;
            _camera.transform.eulerAngles = new Vector3(0, 0, 0);

            _camera.transform.localPosition = _initialCameraPosition;
        }

        if (Time.timeScale < 1)
        {
            Time.timeScale += Time.deltaTime*0.1f;
        }
    }

    private void PlayMouseClick()
    {
        _mouseClick.pitch = UnityEngine.Random.Range(0.4f, 1.2f);
        _mouseClick.volume = UnityEngine.Random.Range(0.4f, 0.6f);

        _mouseClick.Play();
    }

    internal void PlayBubblePopSound()
    {
        _bubblePop.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        _bubblePop.volume = UnityEngine.Random.Range(0.8f, 1.2f);

        _bubblePop.Play();
    }

    internal void PlayExplosionSound()
    {
        _bubbleExplosion.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        _bubbleExplosion.volume = UnityEngine.Random.Range(1.4f, 2f);

        _bubbleExplosion.Play();
        _restartButton.Play("RestartButtonHit");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void TriggerShake(float duration, float magnitude)
    {
        _shakeDuration = duration;
        _maxShakeDuration = duration;
        _shakeMagnitude = magnitude;
    }

    internal void BubbleBlowSound()
    {
        _bubbleBlow.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        _bubbleBlow.volume = UnityEngine.Random.Range(0.8f, 1.2f);

        _bubbleBlow.Play();
    }

    public void RemoveLife()
    {
        _lives--;
        _postprocessing.weight= 1f - ((float)_lives/ (float)_maxLives);
        Debug.Log(1f - ((float)_lives / (float)_maxLives) + " " + _lives +" lives left");
        Time.timeScale = 0.6f;

        ScoreSetter.RemoveLifeScore();


        if ( _lives <= 0)
        {
            Time.timeScale = 0.15f;
            StartCoroutine(StartDeath());
        }
    }
    IEnumerator StartDeath()
    {
        ComboTracker.enabled = false;
 
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 4, 1 - alpha);
            yield return new WaitForSeconds(0.02f);
            ScoreSetter.GameOver();

        }
        _gameOverScreen.SetActive(true);
        
    }

    internal void tileClick()
    {
        ScoreSetter.AddActionscore();
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    internal void CheckIfEnd()
    {
        if (!CustomGridScript.AnythingLeft())
        {
            StartCoroutine(WaitASecondThenReset());
            ScoreSetter.Finished();

        }
    }

    IEnumerator WaitASecondThenReset()
    {
        yield return new WaitForSeconds(0.1f);
        CustomGridScript.Reset();

    }
    internal void tileFlag()
    {
        ScoreSetter.AddActionscore();
        PlayFlagSound();
    }

    internal void tileUnflag()
    {
        ScoreSetter.AddActionscore();
        _unflagAmount++;
        ScoreSetter.ScoreFine(_unflagAmount*2);
        CustomGridScript.Unflag();
        PlayUnflagSound();
    }

    public void ManualReset()
    {
        ScoreSetter.ManuelResetFine();
        CustomGridScript.Reset();
    }

    internal void PlayFailSound()
    {
        _bubbleFail.pitch = UnityEngine.Random.Range(0.4f, 1.2f);
        _bubbleFail.volume = UnityEngine.Random.Range(0.4f, 0.6f);

        _bubbleFail.Play();
    }

    internal void PlayBubblePopSound(float v)
    {
        _bubblePop.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        _bubblePop.volume = UnityEngine.Random.Range(0.8f, 1.2f)*v;

        _bubblePop.Play();
    }

    internal void PlayFlagSound()
    {
        _flagSound.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        _flagSound.volume = UnityEngine.Random.Range(0.8f, 1.2f);

        _flagSound.Play();
    }

    internal void PlayUnflagSound()
    {
        _flagSound.pitch = UnityEngine.Random.Range(0.2f, 0.4f);
        _flagSound.volume = UnityEngine.Random.Range(0.4f, 0.9f);

        _flagSound.Play();
    }
}
