using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemporaryFixForIntro : MonoBehaviour
{
    
    [SerializeField] float _playwaitingTime;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CupheadController>().enabled = false;
        StartCoroutine(AnimatorDelayer());

    }

    // Update is called once per frame

    IEnumerator AnimatorDelayer()
    {
        yield return new WaitForSeconds(_playwaitingTime);
        gameObject.GetComponent<CupheadController>().enabled = true;
    }
}
