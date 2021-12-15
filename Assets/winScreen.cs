using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winScreen : MonoBehaviour
{

    public static bool playerHasWon;
    public GameObject deathScreenUI;
    public GameObject player;
    public GameObject winDoor;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        winDoor = GameObject.FindGameObjectWithTag("WinDoor");
        playerHasWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(winDoor.GetComponent<WinGame>().win)
        {
            playerHasWon = true;
        }
        if (playerHasWon)
        {
            Win();
        }
    }

    public void Win()
    {
        deathScreenUI.SetActive(true);
        playerHasWon = true;
        player.GetComponent<PlayerLook>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu...");
        Time.timeScale = 1f;
        playerHasWon = false;
        player.GetComponent<PlayerLook>().enabled = true;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}