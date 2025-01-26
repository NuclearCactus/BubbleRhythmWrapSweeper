using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MetroGnome : MonoBehaviour
{
    [SerializeField] private float BPM;
    [SerializeField] private float offset;

    private float bpmInSeconds;
    private float nextTime;

    public event EventHandler<EventArgs> Beat;

    [SerializeField] private AudioSource _metronomeTick;
    [SerializeField] private AudioSource _music;



    void Awake()
    {
        bpmInSeconds = 60 / BPM;
        nextTime = (float)AudioSettings.dspTime + bpmInSeconds + offset + _music.time;

#if UNITY_EDITOR
        return;
#else
        nextTime+=bpmInSeconds/2;
        return;
#endif

    }

    void Update()
    {
        if (AudioSettings.dspTime >= nextTime)
        {
            //Debug.Log("Tick");
            nextTime += bpmInSeconds;
            Metronometick();
        }

    }

    private void Metronometick()
    {
        Beat?.Invoke(this, EventArgs.Empty);
        _metronomeTick.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        _metronomeTick.volume = UnityEngine.Random.Range(0.8f, 1.2f);

        _metronomeTick.Play();
    }

    public float ProgressInTick()
    {
        return ((float)AudioSettings.dspTime - nextTime + bpmInSeconds) / bpmInSeconds;

    }
}
