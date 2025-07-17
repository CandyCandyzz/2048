using System.Collections;
using UnityEngine;

public class SlideTileNum : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool isDone = false;

    public void Slide(Vector3 target)
    {
        StartCoroutine(Animate(target));
    }

    private IEnumerator Animate(Vector3 target)
    {
        isDone = false;
        while (Vector2.Distance(transform.position, target) >= 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
        isDone = true;
    }
}
