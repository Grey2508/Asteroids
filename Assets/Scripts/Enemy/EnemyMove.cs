using System.Collections;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float MinDelay = 20;
    [SerializeField] private float MaxDelay = 40;

    [SerializeField] private float TimeOnScreen = 10;

    [Range (0,1)] [SerializeField] private float Borders = 0.2f;

    private float _bordersValues;
    private int _sign;

    void Start()
    {
        _bordersValues = GameBoundary.Height / 2 - GameBoundary.Height * Borders;

        Invoke(nameof(StartMove), Random.Range(MinDelay, MaxDelay + 1));
        gameObject.SetActive(false);
    }

    void StartMove ()
    {
        gameObject.SetActive(true);

        float y = Random.Range(-_bordersValues, _bordersValues);

        _sign = (Random.Range(0, 2) == 1 ? -1 : 1);

        float x = -_sign * GameBoundary.Width / 2;

        transform.position = new Vector3(x, y, 0);

        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        for (float t = 0; t < TimeOnScreen; t += Time.deltaTime)
        {
            transform.position += new Vector3(Time.deltaTime * (GameBoundary.Width / TimeOnScreen), 0, 0) * _sign;

            yield return null;
        }

        Respawn();
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        Invoke(nameof(StartMove), Random.Range(MinDelay, MaxDelay));
    }

    public void Restart()
    {
        Respawn();

        gameObject.SetActive(false);
    }
}
