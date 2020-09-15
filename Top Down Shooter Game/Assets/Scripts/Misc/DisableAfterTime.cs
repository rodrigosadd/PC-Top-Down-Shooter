using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    public float timeDisable;

    void OnEnable()
    {
        StartCoroutine("Disable");
    }

    public IEnumerator Disable()
    {
        yield return new WaitForSeconds(timeDisable);
        gameObject.SetActive(false);
    }
}
