using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class Xp_manager : MonoBehaviour
{
    UnityEvent onProgressComplete;
    public TMP_Text currentXp;
    public Image levelBackground;
    float levelMaxValue = 10f, levelMinValue = 0f;
    float currentLevelValue;
    float levelValue;

    public float CurrentValue
    {
        get
        {
            return currentLevelValue;
        }
        set
        {
            // Ensure the passed value falls within min/max range
            currentLevelValue = Mathf.Clamp(value, levelMinValue, levelMaxValue);

            // Calculate the current fill percentage and display it
            float fillPercentage = currentLevelValue / levelMaxValue;
            levelBackground.fillAmount = fillPercentage;

            // If the value exceeds the max fill, invoke the completion function
            if (value >= levelMaxValue)
            {
                levelValue++;
                onProgressComplete.Invoke();
            }
            // Remove any overfill (i.e. 105% fill -> 5% fill)
            currentLevelValue = value % levelMaxValue;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Getting_exp_speed");
        currentLevelValue = 0;
        levelValue = 1;

        if (onProgressComplete == null)
            onProgressComplete = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        Show_xp_text();
    }

    // XP 경험치를 표시해주는 함수
    void Show_xp_text()
    {
        currentXp.text = levelMinValue.ToString() + " / " + levelMaxValue.ToString();
    }

    IEnumerator Getting_exp_speed()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            levelBackground.fillAmount += CurrentValue;
            CurrentValue+=0.25f;
        }
    }

   
}