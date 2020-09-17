using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Spawner spawner;

    public void PlayGame()
    {
        DefaultValues();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void DefaultValues()
    {
        if (spawner.gameMinutes == 0 && spawner.timeSpawnEnemy == 0)
        {
            spawner.gameMinutes = 1;
            spawner.timeSpawnEnemy = 3;
        }
    }
}
