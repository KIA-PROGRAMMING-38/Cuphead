using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static event Action OnPotatoDied;
   
    
    public bool IsGameover { get; private set;}
    public bool IsPotatoDead { get; private set; }


    [SerializeField] private GameObject _player;
    [SerializeField] private CupheadController CupheadController;
    [SerializeField] private GameObject _Potato;
    [SerializeField] private GameObject _Onion;
    [SerializeField] private GameObject _Carrot;

    private void Start()
    {
        _player.SetActive(true);
        _Potato.SetActive(true);
    }

    public void OnGameStart()
    {
        CupheadController.enabled= true;
    }


    WaitForSeconds _waitTimeForActivatingNextBoss;
    float _waitTimeForActivatingOnionFloat = 1.0f; 

    public void OnPotatoDead()
    {
        _waitTimeForActivatingNextBoss = new WaitForSeconds(_waitTimeForActivatingOnionFloat);
        OnPotatoDied?.Invoke();
        _Potato.SetActive(false);
        StartCoroutine(SetActiveOnionDelayed());
    }

    IEnumerator SetActiveOnionDelayed()
    {
        yield return _waitTimeForActivatingNextBoss;
        _Onion.SetActive(true);
    }



    public void OnOnionDead()
    {
        _Onion.SetActive(false);
        _Carrot.SetActive(true);
    }

    IEnumerator SetActiveCarrotDelayed()
    {
        yield return _waitTimeForActivatingNextBoss;
        _Carrot.SetActive(true);
    }



    public void SetPotatoInactive() => _Potato.SetActive(false);
    public void SetOnionACtive() { }

 
    public void OnCarrotDead()
    {

    }
}
