using System.Collections;
using UnityEngine;

public class NumTileEffect : MonoBehaviour
{
    [Header("PopUpMerge")]
    [SerializeField] private float scaleValuePopUpMerge;
    [SerializeField] private float timedurPopUpMerge;

    [Header("PopUpSpawn")]
    [SerializeField] private float scaleValuePopUpSpawn;
    [SerializeField] private float timedurPopUpSpawn;

    private void Start()
    {
        StartPopUpSpawn();
    }

    public void StartPopUpMerge()
    {
        StartCoroutine(PopUpMerge());
    }
    private IEnumerator PopUpMerge()
    {
        float time = 0;
        Vector3 originScale = transform.localScale;
        Vector3 newScale = originScale * scaleValuePopUpMerge;
        while (time < timedurPopUpMerge)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, newScale, time / timedurPopUpMerge);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = newScale;

        time = 0;
        while (time < timedurPopUpMerge)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, originScale, time / timedurPopUpMerge);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originScale;
    }

    private void StartPopUpSpawn()
    {
        StartCoroutine(PopUpSpawn());
    }
    private IEnumerator PopUpSpawn()
    {
        float time = 0;
        Vector3 originScale = transform.localScale;
        transform.localScale = transform.localScale * scaleValuePopUpSpawn;
        while (time < timedurPopUpSpawn)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, originScale, time / timedurPopUpSpawn);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originScale;
    }
}
