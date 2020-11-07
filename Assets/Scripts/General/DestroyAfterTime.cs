using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Use a negative number to never destroy")]
    private float delayTime = 120f;
    [SerializeField]
    private bool startImmediately = false;

    private bool _countingDown = false;

    void Start()
    {
        if (delayTime > 0)
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