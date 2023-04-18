using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject _player;
    public delegate void PlayerDied();
    public static event PlayerDied playerDied;

    public static event Action OnGameEnded;

    private bool _isGameOver = false;
    void OnEnable()
    {
        playerDied?.Invoke();
    }

   
    void Update()
    {
        if(_isGameOver == false && _player.activeSelf == false)
        {
            SetGameOver();
        }
    }

    public void SetGameOver()
    {
        _isGameOver = true;
        OnGameEnded?.Invoke();


    }
}
