using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;

	private Maze mazeInstance;

	private void Pause()
    {
		Time.timeScale = 0;
    }

	private void Resume()
    {
		Time.timeScale = 1;
    }

    private void Start () {
		StartCoroutine(BeginGame());
	}

	private IEnumerator BeginGame () {
		Pause();
		mazeInstance = Instantiate(mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());
		Debug.Log("Done");
		Resume();

	}
}