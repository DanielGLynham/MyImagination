using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private float dropSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * dropSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().LoseHealth();
        }
        if(other.gameObject.tag != "Projectile")
        {
            Destroy(this.gameObject);
        }
    }
}
