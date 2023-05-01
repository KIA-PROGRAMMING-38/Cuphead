using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{

    IEnumerator TurnUpBgV;




    [SerializeField]
    AudioSource BgmusicVeggie;

    [SerializeField]
    AudioSource musicNarrator;

    [SerializeField]
    AudioClip YouAreUp;




    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        BgmusicVeggie.volume = 0;
        StartBgVoluime();
        Invoke(nameof(PlayYouAreUp), 2.4f);

    }

    private void Update()
    {
        TurnUpBgVolume();
    }

    /// <summary>
    /// VeggieBackground를 설정하는 부분입니다.
    ///  볼륨을 인트로 이후 서서히 올립니다. 
    /// </summary> 
    [SerializeField]
    public float bgVolumeUpSpeed;

    public float targetVolume;
    public float volumeThreshold;

    AudioSource audioSource;

    public void StartBgVoluime()
    {

        BgmusicVeggie.SetScheduledStartTime(3f);

    }

    void  TurnUpBgVolume()
    {
           
            BgmusicVeggie.volume = Mathf.MoveTowards(BgmusicVeggie.volume, targetVolume, bgVolumeUpSpeed * Time.deltaTime);
     
    }

    void PlayYouAreUp()
    {
        audioSource.PlayOneShot(YouAreUp);
    }

   
}
