using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOnGorund : MonoBehaviour
{
    public float waitToRemoveTime = 15f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RemoveSelf());
    }
    private IEnumerator RemoveSelf()
    {
        yield return new WaitForSeconds(waitToRemoveTime);
        Destroy(this.gameObject);
    }
}
