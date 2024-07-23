using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    private bool shotByPlayer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RemoveSelf());
    }
    private IEnumerator RemoveSelf()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
    public void SetStats(bool sbp)
    {
        shotByPlayer = sbp;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (shotByPlayer)
        {
            if (other.gameObject.tag != "Player")
            {
                // avoids player hit
                other.gameObject.GetComponent<BasicGoblin>().TakeDamage(0.5f);
            }
        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Health>().LoseHealth();
            }
        }
    }
}
