using UnityEngine;

public class AsteroidEffect : MonoBehaviour, IPoolable
{
    public bool Free { get; set; }

    private void Start()
    {
        //SetActive(true);
    }
    public void Play()
    {
        GetComponent<ParticleSystem>().Play();
    }


    public ObjectPool NextPool => throw new System.NotImplementedException();

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public void SetNextPool(ObjectPool pool)
    {
    }
}
