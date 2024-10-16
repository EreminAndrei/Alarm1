using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AlarmZone : MonoBehaviour
{
    private AudioSource _alarm;    
    
    private float _volumeChangeStepTime = 1.0f;
    private float _volumeChangeStep = 0.1f;

    private Coroutine _changeVolume;    

    private void Awake()
    {        
        _alarm = GetComponent<AudioSource>();        
        _alarm.volume = 0;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Thief>() != null)
        {
            if (_changeVolume != null)
                StopCoroutine( _changeVolume );

            _changeVolume = StartCoroutine(IncreaseVolume(_volumeChangeStepTime, _volumeChangeStep));            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Thief>() != null)
        {
            if (_changeVolume != null)
                StopCoroutine( _changeVolume );

            _changeVolume = StartCoroutine(DecreaseVolume(_volumeChangeStepTime, _volumeChangeStep));
        }
    }    

    private IEnumerator IncreaseVolume(float timeStep, float volumeStep)
    {
        var wait = new WaitForSeconds(timeStep);

        _alarm.Play();

        while (_alarm.volume < 1)
        {
            yield return wait;
            
            _alarm.volume += volumeStep;            
        }
    }

    private IEnumerator DecreaseVolume(float timeStep, float volumeStep)
    {
        var wait = new WaitForSeconds(timeStep);

        while (_alarm.volume > 0)
        {
            yield return wait;
                      
            _alarm.volume -= volumeStep;               
        }

        _alarm.Stop();
    }
}
