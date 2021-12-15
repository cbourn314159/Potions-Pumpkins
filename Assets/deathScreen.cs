using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathScreen : MonoBehaviour
{

    public static bool playerIsDead;
    public GameObject deathScreenUI;
    public GameObject player;
    public GameObject spiceUI;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spiceUI = GameObject.FindGameObjectWithTag("SpiceUI");
        playerIsDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (spiceUI.GetComponent<SpiceUI>().health <= 0)
        {
            playerIsDead = true;
        }
        if (playerIsDead)
        {
            Die();
        }
    }

    public void Die()
    {
        deathScreenUI.SetActive(true);
        Time.timeScale = 0f;
        playerIsDead = true;
        player.GetComponent<PlayerLook>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu...");
        Time.timeScale = 1f;
        playerIsDead = false;
        player.GetComponent<PlayerLook>().enabled = true;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}