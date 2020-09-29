using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    private float delayTime = 120f;

    void Start()
    {
        Destroy(gameObject, delayTime);
    }
}