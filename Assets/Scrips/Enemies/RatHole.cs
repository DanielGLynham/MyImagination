using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHole : MonoBehaviour
{
    public GameObject ratPrefab;
    private List<GameObject> ratsSpwaned;
    private int maxRatsToSpawn = 5;
    private float maxXSpwnDist = 5f;
    private float maxYSpwnDist = 5f;
    private bool stopSpawning = false;
    private bool waitedToSpawn = true;
    public Sprite blockedUp;
    public GameObject PressEText;
    private GameObject ratHoleParent;
    // Start is called before the first frame update
    void Start()
    {
        ratsSpwaned = new List<GameObject>();
        SpawnRat();
        PressEText.SetActive(false);
    }
    public void InitialiseRatHole(GameObject da)
    {
        ratHoleParent = da;
    }
    // Update is called once per frame
    void Update()
    {
        if(!stopSpawning)
        {
            if(ratsSpwaned.Count - 1 < maxRatsToSpawn)
            {
                if(waitedToSpawn)
                {
                    SpawnRat();
                    waitedToSpawn = false;
                    StartCoroutine(WaitToSpawnRat(5f));
                }
            }
        }
        for (int i = 0; i < ratsSpwaned.Count; i++)
        {
            if(ratsSpwaned[i] == null)
            {
                ratsSpwaned.RemoveAt(i);
                break;
            }
        }
    }
    void SpawnRat()
    {
            float tempx = Random.Range(-maxXSpwnDist, maxXSpwnDist) + transform.position.x;
            float tempy = Random.Range(-maxYSpwnDist, maxYSpwnDist) + transform.position.y;
            float tempRot = Random.Range(-180, 180);
            GameObject tempRat = Instantiate(ratPrefab, new Vector3(tempx, tempy, 0), Quaternion.Euler(new Vector3(0, 90, tempRot)));
            ratsSpwaned.Add(tempRat);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!stopSpawning)
            {
                PressEText.SetActive(true);
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                stopSpawning = true;
                GetComponent<SpriteRenderer>().sprite = blockedUp;
                if(ratHoleParent != null)
                {
                    ratHoleParent.GetComponent<CodingBoss>().PreviousRatHoleBlocked();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PressEText.SetActive(false);
        }
    }
    IEnumerator WaitToSpawnRat(float time)
    {
        yield return new WaitForSeconds(time);
        waitedToSpawn = true;
    }
}
