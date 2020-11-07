using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [Tooltip("External light to flicker; you can leave this null if you attach script to a light")]
    public new Light light;
    [Tooltip("Minimum random light intensity")]
    public float minIntensity = 0f;
    [Tooltip("Maximum random light intensity")]
    public float maxIntensity = 1f;
    [Tooltip("How much to smooth out the randomness; lower values = sparks, higher = lantern")]
    [Range(1, 50)]
    public int smoothing = 5;

    private Queue<float> smoothQueue;
    private float lastSum = 0;

    private void Start()
    {
        smoothQueue = new Queue<float>(smoothing);
        if (light == null)
        {
            light = GetComponent<Light>();
        }
    }

    private void Update()
    {
        try
        {
            while (smoothQueue.Count >= smoothing)
            {
                lastSum -= smoothQueue.Dequeue();
            }

            float newVal = Random.Range(minIntensity, maxIntensity);
            smoothQueue.Enqueue(newVal);
            lastSum += newVal;

            light.intensity = lastSum / (float)smoothQueue.Count;
        }
        catch { }
    }
}