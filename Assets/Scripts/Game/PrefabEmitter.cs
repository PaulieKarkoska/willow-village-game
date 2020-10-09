using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabEmitter : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject[] prefabs;


    //private void Start()
    //{
    //    StartCoroutine(Test());
    //}

    //private IEnumerator Test()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(3);
    //        Emit(transform.position, Vector3.up * 150, new Vector3(0.1f, 0.1f, 0.1f), 0, 5, 10);
    //    }
    //}

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
