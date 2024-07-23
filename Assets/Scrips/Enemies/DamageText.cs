using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public Text dmgTextUI;
    void Start()
    {
        StartCoroutine(DestroySelf());    
    }
    public void SetDamage(float amount)
    {
        dmgTextUI.text = "" + (int)amount;
    }
    private void Update()
    {
        transform.position += new Vector3(0, 2f, 0) * Time.deltaTime;
    }
    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
