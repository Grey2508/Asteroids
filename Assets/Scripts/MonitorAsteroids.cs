using UnityEngine;

public class MonitorAsteroids : MonoBehaviour
{
    [SerializeField] private int StartCountAsteroids = 2;
    [SerializeField] private float CheckSphereRadius = 5;
    [SerializeField] private float DelaySpawn = 2;

    [SerializeField] private ObjectPool AsteroidsPool;
    public static int CountAsteroids;

    private int _defaultStartCountAsteroids;
    private bool _startSpawn = true;

    private void Start()
    {
        _defaultStartCountAsteroids = StartCountAsteroids;
    }

    private void Update()
    {
        if (CountAsteroids > 0 || _startSpawn)
            return;

        _startSpawn = true;

        Invoke(nameof(StartSpawnAsteroids), DelaySpawn);
    }

    private void SpawnNewAsteroid()
    {
        float x = GameBoundary.Width / 2;
        float y = GameBoundary.Height / 2;

        bool freePoint = false;

        Vector3 SpawnPoint = new Vector3();

        while (!freePoint)
        {
            SpawnPoint = new Vector3(Random.Range(x, -x), Random.Range(y, -y), 0);

            freePoint = Physics.CheckSphere(SpawnPoint.normalized, CheckSphereRadius);
        }

        BigAsteroid newAsteroid = AsteroidsPool.GetNextObject() as BigAsteroid;

        newAsteroid.Create(SpawnPoint, Vector3.zero);
    }

    private void StartSpawnAsteroids()
    {
        for (int i = 0; i < StartCountAsteroids; i++)
        {
            SpawnNewAsteroid();
        }

        StartCountAsteroids++;

        _startSpawn = false;
    }

    public void Restart()
    {
        CancelInvoke();

        _startSpawn = true;

        CountAsteroids = 0;
        StartCountAsteroids = _defaultStartCountAsteroids;

        StartSpawnAsteroids();
    }
}
