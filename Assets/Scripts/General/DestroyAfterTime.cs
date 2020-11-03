using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    private float delayTime = 120f;
    [SerializeField]
    private bool startImmediately = false;

    private bool _countingDown = false;

    void Start()
    {
        if (startImmediately)
        {
            _countingDown = true;
            Destroy(gameObject, delayTime);
        }
    }

    public void DestroyObject(GameObject obj)
    {
        if (!_countingDown)
        {
            _countingDown = true;
            Destroy(gameObject, delayTime);
        }
    }
}