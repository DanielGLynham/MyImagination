using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodingBoss : MonoBehaviour
{
    public List<GameObject> PipeShootingEffects;
    public List<GameObject> PipeShootingLocaiton;
    public GameObject projcetile;
    public Sprite boxLandedSprite;
    private int numOfPipes, shootchoice;
    private bool canShoot = true;
    private int numOfOptions = 6;
    private int numOfRatHoles = 0;
    private bool fightStarted = false;
    public GameObject topLeftConstraint, bottomRightConstraint;
    public GameObject ratHole;
    public GameObject areanaBlocker;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Elf");
        numOfPipes = PipeShootingLocaiton.Count;
        shootchoice = 0;
        for (int i = 0; i < numOfPipes; i++)
        {
            PipeShootingEffects[i].SetActive(false);
        }

    }
    private void Update()
    {
        if(player.transform.position.x > topLeftConstraint.transform.position.x && player.transform.position.x < bottomRightConstraint.transform.position.x && player.transform.position.y > bottomRightConstraint.transform.position.y && player.transform.position.y < topLeftConstraint.transform.position.y)
        {
            if(!fightStarted)
            {
                StartFight();            
            }
        }
        if(canShoot && fightStarted)
        {
            StartCoroutine(WaitToShoot(1));
        }
    }
    IEnumerator Shoot(int pipeNum)
    {
        PipeShootingEffects[pipeNum].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        GameObject FallingBox = Instantiate(projcetile, PipeShootingLocaiton[pipeNum].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        PipeShootingEffects[pipeNum].SetActive(false);

    }
    private void ShootAllBarrelsFromStart()
    {
        for (int i = 0; i < numOfPipes / 2; i++)
        {
            StartCoroutine(Shoot(i * 2));
        }
        canShoot = true;
    }
    private void ShootAllBarrelsFromOne()
    {
        for (int i = 0; i < numOfPipes / 2; i++)
        {
            StartCoroutine(Shoot(1 + (i * 2)));
        }
        canShoot = true;
    }
    private void ShootFromRandomTube()
    {
        int rand = Random.Range(0, numOfPipes);
        StartCoroutine(Shoot(rand));
        canShoot = true;
    }
    IEnumerator ShowerLeftToRight(int currentNum, float waitTime)
    {
        int current = currentNum;
        StartCoroutine(Shoot(current));
        yield return new WaitForSeconds(waitTime);
        current++;
        if(current < numOfPipes)
        {
            StartCoroutine(ShowerLeftToRight(current, waitTime));
        }
        else
        {
            canShoot = true;
        }
    }
    IEnumerator ShowerRightToLeft(int currentNum, float waitTime)
    {
        int current = currentNum;
        StartCoroutine(Shoot(current));
        yield return new WaitForSeconds(waitTime);
        current--;
        if (current >= 0)
        {
            StartCoroutine(ShowerRightToLeft(current, waitTime));
        }
        else
        {
            canShoot = true;
        }
    }
    public void StartFight()
    {
        areanaBlocker.SetActive(true);
        fightStarted = true;
        SpawnRatHole();
    }
    private void SpawnRatHole()
    {
        if(numOfRatHoles < 5)
        {
            numOfRatHoles++;
            float x = Random.Range(topLeftConstraint.transform.position.x, bottomRightConstraint.transform.position.x);
            float y = Random.Range(bottomRightConstraint.transform.position.y, topLeftConstraint.transform.position.y);
            GameObject ratHoleSpawned = Instantiate(ratHole, new Vector3(x, y, 0), Quaternion.identity);
            ratHoleSpawned.GetComponent<RatHole>().InitialiseRatHole(this.gameObject);
        }
        else
        {
            fightStarted = false;
            Debug.Log("boss defeated");
        }
    }
    public void PreviousRatHoleBlocked()
    {
        SpawnRatHole();
    }



    IEnumerator WaitToShoot(float waitTime)
    {
        canShoot = false;
        yield return new WaitForSeconds(waitTime);
        switch (shootchoice)
        {
            case 0:
                shootchoice = Random.Range(1, numOfOptions);
                canShoot = true;
                break;
            case 1:
                ShootAllBarrelsFromStart();
                    break;
            case 2:
                ShootAllBarrelsFromOne();
                break;
            case 3:
                ShootFromRandomTube();
                break;
            case 4:
                StartCoroutine(ShowerLeftToRight(0, 0.5f));
                break;
            case 5:
                StartCoroutine(ShowerRightToLeft(numOfPipes - 1, 0.5f));
                break;
        }
        shootchoice = Random.Range(1, numOfOptions);
    }
}
