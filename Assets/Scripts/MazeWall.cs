using UnityEngine;

public class MazeWall : MazeCellEdge
{

	public Transform wall;
    private GameObject collidedObject = null;

    public override void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction)
	{
		base.Initialize(cell, otherCell, direction);
		wall.GetComponent<Renderer>().material = cell.room.settings.wallMaterial;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hammer"))
        {
            //wall.GetComponent<Object>().gameObject = cell.gameObject;
            Debug.Log("Wall down");
            collidedObject = other.gameObject;
        }

        if (collidedObject != null && other.gameObject == collidedObject)
        {
            Destroy(collidedObject);
            collidedObject = null;
        }
    }
}