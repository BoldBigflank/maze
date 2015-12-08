using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Maze : MonoBehaviour
{

	public class Cell {
		public bool visited;
		public bool north;
		public bool east;
		public bool south;
		public bool west;

		public Cell(){
			north = true;
			east  = true;
			south = true;
			west  = true;
		}
	}

	public GameObject wall;
	public float wallLength = 1.0F;
	public int xSize = 10;
	public int ySize = 10;

	Vector3 initialPos;

	GameObject wallHolder;
	List<GameObject> allWalls;
	Cell[,] cells;

	// Use this for initialization
	void Start ()
	{

		CreateCells(); // The data object
		// CreateDFSMaze();
		CreateWalls (); // Visualize the cells
	}
	
	void CreateCells (){
		cells = new Cell[xSize,ySize];
		
		for( int y = 0; y < ySize; y++){
			for ( int x = 0; x < xSize; x++){
				Cell cell = new Cell();
				cells[x,y] = cell;
			}
		}
	}

	void CreateDFSMaze(){
		// Pick a spot

		// check each of its four neighbors
		// if it isn't visited, blow out the wall
	}

	bool isVisited(int x, int y){
		return cells[x,y].visited;
	}

	void CreateWalls(){
		wallHolder = new GameObject();
		wallHolder.name = "Maze";
		allWalls = new List<GameObject>();

		initialPos = new Vector3((-xSize/2.0f) + wallLength / 2.0f, 0.0f, (ySize/2.0f) - wallLength / 2.0f);
		Vector3 myPos = initialPos;
		GameObject tempWall;


		for (int j = 0; j < ySize; j++) {
			for (int i = 0; i < xSize; i++){
				// Check the cell
				Cell cell = cells[i,j];

				// North wall
				if(i == 0){
					myPos = new Vector3 (initialPos.x + (j * wallLength), 0.0f, initialPos.z - (i*wallLength) + wallLength/2);
					tempWall = Instantiate (wall, myPos, Quaternion.Euler (0.0f, 90.0f, 0.0f)) as GameObject;
					tempWall.transform.parent = wallHolder.transform;
					allWalls.Add(tempWall);
				}

				// West wall
				if(j == 0){
					myPos = new Vector3 (initialPos.x + (j * wallLength) - wallLength/2, 0.0f, initialPos.z-(i*wallLength));
					tempWall = Instantiate (wall, myPos, Quaternion.identity) as GameObject;
					tempWall.transform.parent = wallHolder.transform;
					allWalls.Add(tempWall);
				}

				// East wall
				if(cell.east){
					myPos = new Vector3 (initialPos.x + (j * wallLength) + wallLength/2, 0.0f, initialPos.z-(i*wallLength));
					tempWall = Instantiate (wall, myPos, Quaternion.identity) as GameObject;
					tempWall.transform.parent = wallHolder.transform;
					allWalls.Add(tempWall);
				}

				// South wall
				if(cell.south){
					myPos = new Vector3 (initialPos.x + (j * wallLength), 0.0f, initialPos.z - (i*wallLength) - wallLength/2);
					tempWall = Instantiate (wall, myPos, Quaternion.Euler (0.0f, 90.0f, 0.0f)) as GameObject;
					tempWall.transform.parent = wallHolder.transform;
					allWalls.Add(tempWall);
				}

//				// For x axis
//				if(i < ySize){
//					myPos = new Vector3 (initialPos.x + (j * wallLength)-wallLength/2, 0.0f, initialPos.z+(i*wallLength)-wallLength/2);
//					tempWall = Instantiate (wall, myPos, Quaternion.identity) as GameObject;
//					tempWall.transform.parent = wallHolder.transform;
//					allWalls.Add(tempWall);
//				}
//
//				// For y axis
//				if(j < xSize){
//					myPos = new Vector3 (initialPos.x + (j * wallLength), 0.0f, initialPos.z+(i*wallLength)-wallLength);
//					tempWall = Instantiate (wall, myPos, Quaternion.Euler (0.0f, 90.0f, 0.0f)) as GameObject;
//					tempWall.transform.parent = wallHolder.transform;
//					allWalls.Add(tempWall);
//				}
			}
		}

		CreateCells();
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}

