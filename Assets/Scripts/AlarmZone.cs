using System.Collections;
using UnityEngine;

public class AlarmZone : MonoBehaviour
{
    private AudioSource _alarm;    
    
    private float _volumeChangeStepTime = 1.0f;
    private float _volumeChangeStep = 0.1f;    

    private void Start()
    {        
        _alarm = GetComponent<AudioSource>();        
        _alarm.volume = 0;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Thief>() != null)
        {
            StopAllCoroutines();
            _alarm.Play();
            StartCoroutine(VolumeIncrease(_volumeChangeStepTime, _volumeChangeStep));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Thief>() != null)
        {
            StopAllCoroutines();
            StartCoroutine(VolumeDecrease(_volumeChangeStepTime, _volumeChangeStep));
        }
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
