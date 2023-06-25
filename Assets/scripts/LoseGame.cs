using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{
    public GameObject menu;
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
    void OnTriggerEnter(Collider Player)
    {
        if (off && Player.CompareTag("Player"))
        {
            Time.timeScale = 0;
            menu.SetActive(true);
            off = false;
            on = true;
        }
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
