using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{

    public GameObject menu;
    public GameObject resume;
    public GameObject exit;

    public bool on;
    public bool off;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        off = true;
        on = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(off && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            menu.SetActive(true);
            off = false;
            on = true;
        }
        else if(on && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            menu.SetActive(false);
            off = true;
            on = false;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        off = true;
        on = false;
    }

    public void Exit()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        off = true;
        on = false;
        SceneManager.LoadScene(0);
    }
}
