using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TMP_InputField sessionTimeInputField;
    public TMP_InputField spawnTimeInputField;
    public Spawner spawner;

    public void OnSessionTimeChange()
    {
        if (string.IsNullOrEmpty(sessionTimeInputField.text))
        {
            sessionTimeInputField.text = 0.ToString();
            return;
        }
        spawner.gameMinutes = float.Parse(sessionTimeInputField.text);
    }

    public void OnSpawnTimeChange()
    {
        if (string.IsNullOrEmpty(spawnTimeInputField.text))
        {
            spawnTimeInputField.text = 0.ToString();
            return;
        }
        spawner.timeSpawnEnemy = float.Parse(spawnTimeInputField.text);
    }
}
