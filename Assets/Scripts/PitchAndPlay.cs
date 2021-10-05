using UnityEngine;

public class PitchAndPlay : MonoBehaviour
{
    [SerializeField] private AudioSource TargetSound;

    [SerializeField] private float MinPitch = 0.8f;
    [SerializeField] private float MaxPitch = 1.2f;

    [SerializeField] private bool PlayOnAwake = false;
    [SerializeField] private float Lifetime = 0;

    private void Start()
    {
        if (PlayOnAwake)
            Play();

        if (Lifetime > 0)
            Destroy(gameObject, Lifetime);
    }

    public void Play()
    {
        TargetSound.pitch = Random.Range(MinPitch, MaxPitch);
        TargetSound.Play();
    }

    public void Stop()
    {
        TargetSound.Stop();
    }
}
