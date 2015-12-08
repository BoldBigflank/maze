using UnityEngine;
using System.Collections;

public class Maze : MonoBehaviour
{
	public GameObject wall;
	public float wallLength = 1.0F;
	public int xSize = 10;
	public int ySize = 10;

	Vector3 initialPos;

	GameObject wallHolder;


	// Use this for initialization
	void Start ()
	{
		wallHolder = new GameObject();
		wallHolder.name = "Maze";
		CreateWalls();

	}

	void CreateWalls(){
		initialPos = new Vector3((-xSize/2) + wallLength / 2, 0.0f, (-ySize/2) + wallLength / 2);
		Vector3 myPos = initialPos;
		GameObject tempWall;

		// For x axis
		for (int i = 0; i < ySize; i++) {
			for (int j = 0; j <= xSize; j++){
				myPos = new Vector3 (initialPos.x + (j * wallLength)-wallLength/2, 0.0f, initialPos.z+(i*wallLength)-wallLength/2);
				tempWall = Instantiate (wall, myPos, Quaternion.identity) as GameObject;
				tempWall.transform.parent = wallHolder.transform;
			}
		}

		// For x axis
		for (int j = 0; j < ySize; j++) {
			for (int i = 0; i <= xSize; i++){
				myPos = new Vector3 (initialPos.x + (j * wallLength), 0.0f, initialPos.z+(i*wallLength)-wallLength);
				tempWall = Instantiate (wall, myPos, Quaternion.Euler (0.0f, 90.0f, 0.0f)) as GameObject;
				tempWall.transform.parent = wallHolder.transform;
			}
		}

	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}

