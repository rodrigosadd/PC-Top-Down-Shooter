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
        spawner.gameMinutes = float.Parse(sessionTimeInputField.text);
    }

    public void OnSpawnTimeChange()
    {
        spawner.timeSpawnEnemy = float.Parse(spawnTimeInputField.text);
    }
}
