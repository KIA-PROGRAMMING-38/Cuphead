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
<<<<<<< HEAD


    [SerializeField] private GameObject _player;
    [SerializeField] private CupheadController CupheadController;


    [SerializeField] private GameObject _Potato;
    [SerializeField] private Transform _Potatobody;

    [SerializeField] private GameObject _Onion;
    [SerializeField] private GameObject _OnionBackground;

    [SerializeField] private GameObject _Carrot;


    [SerializeField] private GameObject _bossDeathEffect;
    [SerializeField] private BossDeathEffectController bossDeathEffectController;

    private float startTime;
    private void Awake()
    {
        _waitTimeForActivateRecord = new WaitForSeconds(_waitTimeForActivateRecordFloat);
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

    WaitForSeconds _waitTimeForActivateRecord;
    float _waitTimeForActivateRecordFloat = 8.0f;

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


    [SerializeField]
    GameObject _resultScene;
    [SerializeField]
    GameObject _inPlayerScene;
    [SerializeField]
    GameObject _cuphead;


    [SerializeField] AudioSource SoundManager;
    [SerializeField] AudioClip _knockout;

    public void OnCarrotDead()
    {
        SoundManager.clip = _knockout;
        SoundManager.loop = false;
        SoundManager.PlayOneShot(_knockout);
        _bossDeathEffect.transform.position = _Carrot.transform.position;
        bossDeathEffectController.ActivateAnimation();
        _bossDeathEffect.SetActive(true);

        StartCoroutine(SetActiveRecordDelayed());

        IsGameover = true;
       
    }

    IEnumerator SetActiveRecordDelayed()
    {
     
        yield return _waitTimeForActivateRecord;
        _Carrot.SetActive(false);
        _inPlayerScene.SetActive(false);
        _resultScene.SetActive(true);
        _cuphead.SetActive(false);
    }



    public static float playTime;

    IEnumerator StartTimer()
    {
        while (!IsGameover)
        {
            playTime = Time.time - startTime;
            yield return null;
        }
       
        
    }


    
  
=======
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
>>>>>>> 4a852836704d737aa2115b708427c05389db532a

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

    public void SetPotatoInactive() => _Potato.SetActive(false);
    public void SetOnionACtive() { }

 
   
}
