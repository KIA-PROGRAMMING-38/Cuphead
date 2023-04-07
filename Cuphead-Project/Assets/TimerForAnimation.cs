using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerForAnimation : MonoBehaviour
{
    [SerializeField] float _timeConditionToDelay;
    public Animator _animator;
    public GameObject _gameObject;
    public readonly float BOSSINTROBEGIN = 0.6f;

   
    void Start()
    {
        StartCoroutine(AnimatorDelayer());
       
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    IEnumerator AnimatorDelayer()
    {
        yield return new WaitForSeconds(_timeConditionToDelay);
        _animator.enabled = true;
    }
}

