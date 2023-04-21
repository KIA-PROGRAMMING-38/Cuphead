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
    [SerializeField] private CupheadController _cupheadController;
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
        _cupheadController.enabled= true;
    }

    public void OnPotatoDead()
    {
        OnPotatoDied?.Invoke();
        _Potato.SetActive(false);
        _Onion.SetActive(true);
    }
    public void OnOnionDead()
    {
        _Onion.SetActive(false);
        _Carrot.SetActive(true);
    }

    public void SetPotatoInactive() => _Potato.SetActive(false);
    public void SetOnionACtive() { }

 
    public void OnCarrotDead()
    {

    }
}
