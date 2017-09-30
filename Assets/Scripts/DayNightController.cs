using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayNightController : MonoBehaviour {

    public Material mat;
    public Color lerpedColor;

    private Color dayColor = Color.white;
    private Color sunsetColor = new Vector4(0.957f, 0.643f, 0.376f, 1);
    private Color nightColor = new Vector4(0.275f, 0.510f, 0.706f, 1);

    // Current day cycle is four minutes long
    private float secondsInFullDay = 240f;
    private float timeMultiplier = 1f;
    private float currentTimeOfDay;

    // The "timeIn" variables control the span of the lerp, telling it how long to gradually change between the colors
    private float timeInSunset = 0;
    private float timeInDusk = 0;
    private float timeInDawn = 0;

    // Reset all counters used by the loop so that the 
    // lerp will work for every iteration
    private void resetCounters()
    {
        currentTimeOfDay = 0;
        timeInDawn = 0;
        timeInDusk = 0;
        timeInSunset = 0;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
        if (currentTimeOfDay >= .9375f)
            resetCounters();

        if (currentTimeOfDay >= 0 && currentTimeOfDay <= .50f)
        {
            lerpedColor = dayColor;
        }
        else if (currentTimeOfDay >= .51f && currentTimeOfDay <= .5625f)
        {
            timeInSunset += Time.deltaTime / 12;
            lerpedColor = Color.Lerp(dayColor, sunsetColor, timeInSunset);
        }
        else if (currentTimeOfDay >= .5626f && currentTimeOfDay <= .625f)
        {
            // Queue the nightime transition sequence
            if (GameManager.instance.isDaytime != false)
                GameManager.instance.setNighttimeSettings();

            timeInDusk += Time.deltaTime / 15;
            lerpedColor = Color.Lerp(sunsetColor, nightColor, timeInDusk);
        }
        else if (currentTimeOfDay >= .626f && currentTimeOfDay <= .875f)
        {
            lerpedColor = nightColor;
        }
        else if (currentTimeOfDay >= .876f && currentTimeOfDay <= .9375f)
        {
            // Queue the daytime transition sequence
            if (currentTimeOfDay >= .895f && GameManager.instance.isDaytime != true)
                GameManager.instance.setDaytimeSettings();

            timeInDawn += Time.deltaTime / 15;
            lerpedColor = Color.Lerp(nightColor , dayColor, timeInDawn);
        }

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "One Perfect Village House")
        {
            lerpedColor = Color.white;
        }

        mat.SetColor("_Color", lerpedColor);
        Graphics.Blit(src, dest, mat);
    }
}
