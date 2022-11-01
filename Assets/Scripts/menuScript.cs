using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    public TMP_Text text;
    private void Start()
    {
        try
        {
            print(PlayerPrefs.GetFloat("score", 0).ToString());
        }
        catch
        {

        }
        text.text = PlayerPrefs.GetFloat("score", 0).ToString();
        
    }
    public void StartGame()
    {
        Debug.Log("Starting Game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Terminating Program...");
        Application.Quit();
    }
}
