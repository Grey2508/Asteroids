public interface IPoolable
{
    bool Free { get; set; }

    ObjectPool NextPool { get;}

    void SetAsteroidPool(ObjectPool pool);
}
