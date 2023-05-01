using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float time;

    [SerializeField]
    private float timerSpeed;


    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        showPlayime();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void showPlayime()
    {
        if(time == GameManager.playTime)
        {
           timeText.color = Color.yellow; 
        }
        time = Mathf.Lerp(time, GameManager.playTime, Time.deltaTime * timerSpeed);
        timeText.text = string.Format("{0:00}:{1:00}",
        Mathf.FloorToInt(GameManager.playTime / 60.0f),
        Mathf.FloorToInt(GameManager.playTime % 60.0f));
    }
}
