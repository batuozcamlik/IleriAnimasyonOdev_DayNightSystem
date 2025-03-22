using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LightManager lightManager;

    [Header("Temperature")]
    [SerializeField] private float currentTemperature;
    [SerializeField] private float temperatureChangeSpeed = 1f; // Sýcaklýk geçiþ hýzý
    [SerializeField] private float maxTemperature;
    
    [SerializeField] private Image barImage;
    [SerializeField] private TextMeshProUGUI temperatureText;

    private float targetTemperature;

    private void Update()
    {
        if (lightManager != null)
        {
            targetTemperature = CalculateTemperature(lightManager.timeOfDay);
            currentTemperature = Mathf.Lerp(currentTemperature, targetTemperature, Time.deltaTime * temperatureChangeSpeed);
        }

        barImage.fillAmount = currentTemperature / maxTemperature;
        temperatureText.text= ((int)currentTemperature).ToString()+" C";
    }

    private float CalculateTemperature(float timeOfDay)
    {
        float morningMinTemp = 15f;
        float morningMaxTemp = 20f;
        float afternoonMinTemp = 25f;
        float afternoonMaxTemp = 35f;
        float nightMinTemp = 10f;
        float nightMaxTemp = 15f;

        float t = 0f;

        // 06:00 - 12:00 (Sabah)
        if (timeOfDay >= 6f && timeOfDay < 12f)
        {
            t = Mathf.SmoothStep(0f, 1f, (timeOfDay - 6f) / 6f);
            return Mathf.Lerp(morningMinTemp, morningMaxTemp, t);
        }
        // 12:00 - 18:00 (Öðleden Sonra)
        else if (timeOfDay >= 12f && timeOfDay < 18f)
        {
            t = Mathf.SmoothStep(0f, 1f, (timeOfDay - 12f) / 6f);
            return Mathf.Lerp(afternoonMinTemp, afternoonMaxTemp, t);
        }
        // 18:00 - 06:00 (Geceye Geçiþ)
        else
        {
            if (timeOfDay >= 18f)
                t = Mathf.SmoothStep(0f, 1f, (timeOfDay - 18f) / 12f);
            else
                t = Mathf.SmoothStep(0f, 1f, (timeOfDay + 6f) / 12f);

            return Mathf.Lerp(nightMinTemp, nightMaxTemp, t);
        }
    }
}
