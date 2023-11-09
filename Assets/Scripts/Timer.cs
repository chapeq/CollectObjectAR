using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public static float time = 0f;
    private TimeSpan timePlaying;

    public void StartTimer()
    {
        time = 0f;
        StartCoroutine("IncreaseTime");
    }

    public void EndTimer()
    {
        StopCoroutine("IncreaseTime");
    }

    private IEnumerator IncreaseTime()
    {
        while (true)
        {
            time += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(time);
            timerText.text = timePlaying.ToString(@"mm\:ss\:ff");
            yield return null;
        }

    }

}
