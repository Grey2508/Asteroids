public interface IPoolable
{
    bool Free { get; set; }

    ObjectPool NextPool { get;}

    void SetNextPool(ObjectPool pool);

    void SetActive(bool value);
}
