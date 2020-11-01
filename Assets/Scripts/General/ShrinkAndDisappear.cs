using System.Collections;
using UnityEngine;

public class ShrinkAndDisappear : MonoBehaviour
{
    [SerializeField]
    private float startDelay = 30;
    [SerializeField]
    private float shrinkTime = 0.1f;

    public void StartToShrink(GameObject objectToDestroy = null)
    {
        StartCoroutine(Shrinking());
    }

    private IEnumerator Shrinking()
    {

        yield return new WaitForSeconds(startDelay);

        var rb = gameObject.GetComponent<Rigidbody>();
        if (rb)
            rb.isKinematic = true;

        var origScale = transform.localScale;
        var totalTime = shrinkTime;

        var currentTime = 0f;

        do
        {
            transform.localScale = Vector3.Lerp(origScale, Vector3.zero, currentTime / totalTime);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= totalTime);

        Destroy(gameObject);
    }
}
