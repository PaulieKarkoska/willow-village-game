using UnityEngine;

public class AmbientPlayerSingleton : MonoBehaviour
{
    private static GameObject instance;

    void Start()
    {
        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}