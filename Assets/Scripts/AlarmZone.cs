using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AlarmZone : MonoBehaviour
{
    private AudioSource _alarm;    
    
    private float _volumeChangeStepTime = 1.0f;
    private float _volumeChangeStep = 0.1f;

    private Coroutine _increaseCoroutine;
    private Coroutine _decreaseCoroutine;

    private void Awake()
    {        
        _alarm = GetComponent<AudioSource>();        
        _alarm.volume = 0;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Thief>() != null)
        {
            _alarm.Play();
            StopDecrease();
            StartIncrease();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Thief>() != null)
        {
            StopIncrease();
            StartDecrease();
        }
    }

    private void StartIncrease()
    {
        _increaseCoroutine = StartCoroutine(VolumeIncrease(_volumeChangeStepTime, _volumeChangeStep));        
    }

    private void StopIncrease()
    {
        if (_increaseCoroutine != null)
            StopCoroutine(_increaseCoroutine);
    }

    private void StartDecrease()
    {
        _decreaseCoroutine = StartCoroutine(VolumeDecrease(_volumeChangeStepTime, _volumeChangeStep));
    }

    private void StopDecrease()
    {
        if ( _decreaseCoroutine != null)
            StopCoroutine(_decreaseCoroutine);
    }

    private IEnumerator VolumeIncrease (float timeStep, float volumeStep)
    {
        var wait = new WaitForSeconds(timeStep);

        while (true)
        {
            yield return wait;

            if (_alarm.volume < 1)
                _alarm.volume += volumeStep;            
        }
    }

    private IEnumerator VolumeDecrease(float timeStep, float volumeStep)
    {
        var wait = new WaitForSeconds(timeStep);

        while (true)
        {
            yield return wait;

            if (_alarm.volume > 0)            
                _alarm.volume -= volumeStep;   
            else
                _alarm.Stop();
        }
    }
}
