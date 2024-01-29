using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gem : MonoBehaviour
{
    float x;
    float y;
    float z;
    Vector3 pos;

    public GameObject Player;

    void Start()
    {
        x = Random.Range(-9.5f, 9.5f);
        y = 0.2f;
        z = Random.Range(-9.6f, 9.5f);
        pos = new Vector3(x, y, z);
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject == Player)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

}
