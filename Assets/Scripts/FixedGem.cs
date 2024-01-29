using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FixedGem : MonoBehaviour
{
    public GameObject Player;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == Player)
        {
            SceneManager.LoadScene("WinScene");
        }
    }
}
