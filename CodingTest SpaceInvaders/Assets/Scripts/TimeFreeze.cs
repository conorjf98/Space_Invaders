using System.Collections;
using UnityEngine;

public class TimeFreeze : MonoBehaviour
{
    bool waiting;
    public static TimeFreeze tf;
    private void Awake()
    {
        tf = this;
    }
    public void Stop(float duration)
    {
        if (waiting)
            return;
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }
}
