using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float moveSpeed = 7;
    private float dashSpeed = 40;
    private Rigidbody _rb;
    public GameObject _mouseLocaiton;
    private bool dash = false, paused = false;
    public GameObject dashSprite;
    private bool dashLoaded = true;
    private GameObject elfUI;
    private bool gameMenuOpen = false;


    // Start is called before the first frame update
    void Start()
    {
        elfUI = GameObject.Find("ManagerObj");
        _rb = GetComponent<Rigidbody>();
        if(dashSprite.activeInHierarchy)
        {
            dashSprite.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            gameMenuOpen = !gameMenuOpen;
            elfUI.GetComponent<UI_elf>().SetMenuActive(gameMenuOpen);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(dashLoaded)
            {
                dash = true;
                StartCoroutine(Dashing());
            }
        }
        if(!dash && !paused)
        {
            _mouseLocaiton.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10);
            transform.LookAt(_mouseLocaiton.transform, Vector3.up);
            transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * moveSpeed;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else if (dash && !paused)
        {
            transform.position += transform.forward * Time.deltaTime * dashSpeed;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else if(!dash && paused)
        {
            // do nothing I guess?
        }
    }
    IEnumerator Dashing()
    {
        dashLoaded = false;
        elfUI.GetComponent<UI_elf>().SetDashActive(false);
        dashSprite.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        dash = false;
        dashSprite.SetActive(false);
        StartCoroutine(LoadDash());
    }
    IEnumerator LoadDash()
    {
        yield return new WaitForSeconds(5f);
        dashLoaded = true;
        elfUI.GetComponent<UI_elf>().SetDashActive(true);
    }
    public void SetPaused(bool toggle)
    {
        paused = toggle;
    }
}
