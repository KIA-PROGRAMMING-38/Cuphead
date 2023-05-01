using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    
    [SerializeField] RectTransform _firstGauge;
    [SerializeField] RectTransform _secondGauge;
    [SerializeField] RectTransform _thirdGauge;
    [SerializeField] RectTransform _fourthGuauge;
    [SerializeField] RectTransform _fifthGauge;

    [SerializeField] RectTransform _firstGaugeCard;
    [SerializeField] RectTransform _secondGaugeCard;
    [SerializeField] RectTransform _thirdGaugeCard;
    [SerializeField] RectTransform _fourthGuaugeCard;
    [SerializeField] RectTransform _fifthGaugeCard;

    [SerializeField]
    float MIN_POSITION_Y;

    public static int ExCount;

    RectTransform[] GuageTransforms = new RectTransform[5];
    RectTransform[] CardsTransforms = new RectTransform[5];

    void Start()
    {
        GuageTransforms[0] = _firstGauge;
        GuageTransforms[1] = _secondGauge;
        GuageTransforms[2] = _thirdGauge;
        GuageTransforms[3] = _fourthGuauge;
        GuageTransforms[4] = _fifthGauge;


        CardsTransforms[0] = _firstGaugeCard;
        CardsTransforms[1] = _secondGaugeCard;
        CardsTransforms[2] = _thirdGaugeCard;
        CardsTransforms[3] = _fourthGuaugeCard;
        CardsTransforms[4] = _fifthGaugeCard;
    }





    void Update()
    {
        IncreaeGuage();
    }
   
    public void IncreaeGuage()

    {
        //EX Count가 0인경우
        ExCount = CupheadController.CurrentExMoveGauge / CupheadController.ExMoveGaugeCountPerOne;

        

        for (int i = 4; i >= ExCount; i--) // 아닌경우 카드위치를 밑으로 
        {
          
            CardsTransforms[i].anchoredPosition = new Vector2
                (CardsTransforms[i].anchoredPosition.x, MIN_POSITION_Y);

            
        }

        for(int i = 0; i < 4; i++)
        {
            if(ExCount != i)
            {
                GuageTransforms[i].anchoredPosition = new Vector2
                    (GuageTransforms[i].anchoredPosition.x, MIN_POSITION_Y);
            }
        }
        
        if (ExCount < 5) // EXCOunt MAX == 5 게이지를 올리기. 
        {
            for (int i = 0; i < ExCount; i++)  //ExCount 갯수만큼, 생산량 증가
            {
                
                CardsTransforms[i].anchoredPosition = new Vector2
                    (CardsTransforms[i].anchoredPosition.x, 0);

                
            }

            var anchoredPosition = GuageTransforms[ExCount].anchoredPosition;

            anchoredPosition.y = MIN_POSITION_Y - ((CupheadController.CurrentExMoveGauge % CupheadController.ExMoveGaugeCountPerOne)
                         * MIN_POSITION_Y / CupheadController.ExMoveGaugeCountPerOne);

            GuageTransforms[ExCount].anchoredPosition = anchoredPosition;
        }



    }
}
