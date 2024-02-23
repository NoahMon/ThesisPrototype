using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject Pon;
    public GameObject Poff;
    // Start is called before the first frame update
    private void Awake()
    {
        Pon.gameObject.SetActive(false);
        Poff.gameObject.SetActive(false);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
