using UnityEngine;
using System.IO;

public class Unity_CSV : MonoBehaviour
{
    private string filename = "player_position.csv";
    private Transform playerTransform;
    private float timer = 0f;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
        if (!File.Exists(filename))
        {
            File.WriteAllText(filename, "Time,X,Y,Z\n");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0f;
            SavePlayerPosition();
        }
    }

    void SavePlayerPosition()
    {
        Vector3 position = playerTransform.position;
        string data = Time.time + "," + position.x + "," + position.y + "," + position.z + "\n";
        File.AppendAllText(filename, data);
    }
}
