using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static event Action OnPotatoDied;
   
    
    public bool IsGameover { get; private set;}
    public bool IsPotatoDead { get; private set; }


    [SerializeField] private GameObject _player;
    [SerializeField] private CupheadController CupheadController;


    [SerializeField] private GameObject _Potato;
    [SerializeField] private Transform _Potatobody;

    [SerializeField] private GameObject _Onion;
    [SerializeField] private GameObject _OnionBackground;

    [SerializeField] private GameObject _Carrot;


    [SerializeField] private GameObject _bossDeathEffect;
    [SerializeField] private BossDeathEffectController bossDeathEffectController;

    public float startTime;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        startTime = Time.time;
        StartCoroutine(StartTimer());
        _player.SetActive(true);
        _Potato.SetActive(true);
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
       
    }




    public void OnGameStart()
    {
        CupheadController.enabled= true;
    }
    

    WaitForSeconds _waitTimeForActivatingNextBoss;
    float _waitTimeForActivatingOnionFloat = 2.0f;

    public void OnPotatoDeadStart()
    {
        _bossDeathEffect.transform.position = _Potatobody.transform.position;
        bossDeathEffectController.ActivateAnimation();
        _bossDeathEffect.SetActive(true);
    }

    public void OnPotatoDead()
    {

        _waitTimeForActivatingNextBoss = new WaitForSeconds(_waitTimeForActivatingOnionFloat);
        OnPotatoDied?.Invoke();
        
        StartCoroutine(SetActiveOnionDelayed());
    }

    IEnumerator SetActiveOnionDelayed()
    {
        yield return _waitTimeForActivatingNextBoss;
        _Potato.SetActive(false);
        _Onion.SetActive(true);
    }


    public void OnOnionDeadStart()
    {
        _bossDeathEffect.transform.position = _Onion.transform.position;
        bossDeathEffectController.ActivateAnimation();
        _bossDeathEffect.SetActive(true);
    }


    public void OnOnionDead()
    {
        StartCoroutine(SetActiveCarrotDelayed());

    }

    IEnumerator SetActiveCarrotDelayed()
    {
        yield return _waitTimeForActivatingNextBoss;
        _Onion.SetActive(false);
        _Carrot.SetActive(true);
    }



    public void OnCarrotDead()
    {
        _bossDeathEffect.transform.position = _Carrot.transform.position;
        bossDeathEffectController.ActivateAnimation();
        _bossDeathEffect.SetActive(true);
    }




    public static float playTime;

    IEnumerator StartTimer()
    {
        while (!IsGameover)
        {
            playTime = Time.time - startTime;

            if (IsGameover)
            {
                StopTimerAndLoadNewScene();
            }

            yield return null;
        }
       
        
    }

    public void StopTimerAndLoadNewScene()
    {
        IsGameover = true;
        string sceneName = "RESULT";
        Debug.Log(playTime);
        SceneManager.LoadScene(sceneName);
    }


    public void SetPotatoInactive() => _Potato.SetActive(false);
    public void SetOnionACtive() { }

 
   
}
