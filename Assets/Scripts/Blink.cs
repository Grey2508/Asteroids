using System.Collections;
using UnityEngine;

public class Blink : MonoBehaviour
{
    private IBlinked _blinkedObject;

    public void StartEffect(float blinkTime, IBlinked blinked = null)
    {
        if (blinked == null)
            return;

        _blinkedObject = blinked;

        StartCoroutine(BlinkEffect(blinkTime));
    }

    private IEnumerator BlinkEffect(float blinkTime)
    {
        for (float t = 0; t < blinkTime; t += Time.deltaTime)
        {
            float value = (float)System.Math.Round(-Mathf.Sin(t * 11) * 0.5f + 0.5f, 2);

            if (value > 0.98)
                _blinkedObject.Hide();
            else if (value < 0.02)
                _blinkedObject.Show();

            yield return null;
        }

        _blinkedObject.Show();
    }
}
