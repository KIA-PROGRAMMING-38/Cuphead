using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    TextMeshProUGUI hpText;

    [SerializeField]
    TextMeshProUGUI _hpBonus;

    [SerializeField]
    TextMeshProUGUI _parrySucceedCount;

    [SerializeField]
    TextMeshProUGUI _superMeter;

    [SerializeField]
    TextMeshProUGUI _skillLevelStar;

    [SerializeField]
    TextMeshProUGUI _grade;

    [SerializeField]
    TextMeshProUGUI _previousGrade;
    void Start()
    {
        hpText = GetComponent<TextMeshProUGUI>();
        hpText.text = $"HP.{CupheadController.playerHP}";
    }

    private void OnEnable()
    {
        CupheadController.HpControllEffect -= decreaseHP;
        CupheadController.HpControllEffect += decreaseHP;

    }

    private void OnDisable()
    {
        CupheadController.HpControllEffect -= decreaseHP;
    }

    private void Update()
    {
        decreaseHP();
    }
    private void ShowHpBonus()
    {
        _hpBonus.text = $"{CupheadController.HpBonus}/3";
    }

    private void ShowParrySucceedCount()
    {
        if (CupheadController.ParrySucceedCount < 4)
            _parrySucceedCount.text = $"{CupheadController.ParrySucceedCount} / 3";

        else if (CupheadController.ParrySucceedCount > 4)
        {
            _parrySucceedCount.text = "{3/3}";
        }

        else
        {
            Debug.Log($"there's error in ParryCountNumber ParrycountNumber = {CupheadController.ParrySucceedCount}");

        }
    }


    private void ShowSuperMeter()
    {
        _parrySucceedCount.text = $"{CupheadController.SuperMeter}/6";
    }


    /// <summary>
    /// showskillLevel은 성취도에 따라서 별애니메이션을 재생하는 갯수를정해줍니다
    /// skill레벨은 임의로 기준을 정해줘야합니다. 
    /// </summary>
    /// 
    ///
    private void ShowSkillLevelStart()
    {

    }


    /// <summary>
    /// ABCDE순으로 높은 등급입니다
    /// 기준을 임의로 설정합니다.
    /// </summary>
    private void ShowGrade()
    {

    }


    [SerializeField] Image HpTextBg;
    public void decreaseHP()
    {
       

        if (CupheadController.playerHP >= 2)
        {
           
            hpText.text = $"HP.{CupheadController.playerHP}";
        }

        if (CupheadController.playerHP == 1)
        {
            hpText.maskable = false;
            hpText.color = Color.white;
            HpTextBg.color = Color.red;
            hpText.text = $"HP.{CupheadController.playerHP}";

        }

        if (CupheadController.playerHP <= 0)
        {
            hpText.text = "DEAD";
        }




    }




}
