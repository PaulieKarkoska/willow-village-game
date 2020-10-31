using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabEmitter : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject[] prefabs;

    [Header("Emission")]
    [SerializeField]
    private bool _emitOnStart = false;
    [SerializeField]
    private float _startEmissionDelay = 0;
    [SerializeField]
    private Vector3 _positionJitter = Vector3.zero;

    [Header("Continuous")]
    [SerializeField]
    private bool _isContinuous = false;
    [SerializeField]
    private float _continuousEmissionsPerSecond = 1;
    [SerializeField]
    private float _continuousEmissionJitter = 0;

    private void Start()
    {
        if (_emitOnStart)
            StartCoroutine(EmitOnStart());

        if (_isContinuous)
            StartCoroutine(EmitContinuously());
    }

    private IEnumerator EmitOnStart()
    {
        yield return new WaitForSeconds(_startEmissionDelay);
        Emit(transform.position, Vector3.zero, _positionJitter, 0, 1, 1);
    }

    private IEnumerator EmitContinuously()
    {
        while (_isContinuous)
        {
            var rate = 1 * _continuousEmissionsPerSecond;
            rate += Random.Range(rate - _continuousEmissionJitter, rate + _continuousEmissionJitter);
            yield return new WaitForSeconds(rate);
            Emit(transform.position, Vector3.zero, _positionJitter, 0, 1, 1);
        }
    }

    public GameObject[] Emit(Vector3 startPos, Vector3 force, Vector3 jitter, int prefabIndex, int emissionMinCount, int emissionMaxCount)
    {
        int count = Random.Range(emissionMinCount, emissionMaxCount + 1);
        jitter = new Vector3(Mathf.Abs(jitter.x),
                             Mathf.Abs(jitter.y),
                             Mathf.Abs(jitter.z));

        var objects = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            var pos = new Vector3(Random.Range(startPos.x - jitter.x, startPos.x + jitter.x),
                                  Random.Range(startPos.y - jitter.y, startPos.y + jitter.y),
                                  Random.Range(startPos.z - jitter.z, startPos.z + jitter.z));

            var obj = Instantiate(prefabs[prefabIndex], pos, Quaternion.identity);
            obj.GetComponent<Rigidbody>().AddForceAtPosition(force, obj.transform.position);
            objects.Add(obj);
        }
        return objects.ToArray();
    }

    public static GameObject[] Emit(GameObject prefab, Vector3 startPos, Vector3 force, Vector3 jitter, int emissionMinCount, int emissionMaxCount)
    {

        int count = Random.Range(emissionMinCount, emissionMaxCount + 1);
        jitter = new Vector3(Mathf.Abs(jitter.x),
                             Mathf.Abs(jitter.y),
                             Mathf.Abs(jitter.z));

        var objects = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            var pos = new Vector3(Random.Range(startPos.x - jitter.x, startPos.x + jitter.x),
                                  Random.Range(startPos.y - jitter.y, startPos.y + jitter.y),
                                  Random.Range(startPos.z - jitter.z, startPos.z + jitter.z));

            var obj = Instantiate(prefab, pos, Quaternion.identity);
            obj.GetComponent<Rigidbody>().AddForce(force);
            objects.Add(obj);
        }
        return objects.ToArray();
    }
}