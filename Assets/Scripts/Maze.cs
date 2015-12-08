using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class IListExtensions {
	/// <summary>
	/// Shuffles the element order of the specified list.
	/// </summary>
	public static void Shuffle<T>(this IList<T> ts) {
		var count = ts.Count;
		var last = count - 1;
		for (var i = 0; i < last; ++i) {
			var r = UnityEngine.Random.Range(i, count);
			var tmp = ts[i];
			ts[i] = ts[r];
			ts[r] = tmp;
		}
	}
}

public class Maze : MonoBehaviour
{

	public class Cell {
		public bool visited;
		public bool north;
		public bool east;
		public bool south;
		public bool west;

		public int row;
		public int column;

		public Cell(){
			visited = false;
			north = true;
			east  = true;
			south = true;
			west  = true;
		}
	}

	public class CellPosition{
		public int x;
		public int y;

		public CellPosition(int x, int y){
			this.x = x;
			this.y = y;
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
		Debug.Log ("Cells created");
		CreateDFSMaze();
		Debug.Log ("Maze created");
		CreateWalls (); // Visualize the cells
		Debug.Log("Walls Created");
//		DebugMaze();
	}
	
	void CreateCells (){
		cells = new Cell[xSize,ySize];
		
		for( int y = 0; y < ySize; y++){
			for ( int x = 0; x < xSize; x++){
				Cell cell = new Cell();
				cell.column = x;
				cell.row = y;

				cells[x,y] = cell;
			}
		}
	}

	void DebugMaze(){
		string outLine = "\n";
		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				outLine += (cells[x, y].west) ? "|" : " " ;
				outLine += (cells[x, y].south) ? "_" : "  " ;
				outLine += (cells[x, y].east) ? "|" : " " ;

			}
			outLine += "\n";
		}
		Debug.Log(outLine);
	}

	void CreateDFSMaze(){
		// Pick a spot
//		MakePath(null, cells[Random.Range(0, xSize), Random.Range(0, ySize)]);
		MakePath(null, cells[0,0]);
	}

	void MakePath(Cell prev, Cell current){
		current.visited = true;
		
		// Where it came from
		if(prev != null){
			if(prev.column < current.column){
				current.west = false;
				prev.east = false;
			}
			if(prev.column > current.column){
				current.east = false;
				prev.west = false;
			}
			if(prev.row < current.row){
				current.north = false;
				prev.south = false;
			}
			if(prev.row > current.row){
				current.south = false;
				prev.north = false;
			}
		}
		
		List<CellPosition> neighbors = new List<CellPosition>();
		neighbors.Add(new CellPosition(current.column+1, current.row)); 
		neighbors.Add(new CellPosition(current.column-1, current.row)); 
		neighbors.Add(new CellPosition(current.column, current.row+1));
		neighbors.Add(new CellPosition(current.column, current.row-1));
		
		neighbors.Shuffle();
		
		foreach(CellPosition c in neighbors){
			if(!isVisited(c.x, c.y)){
				MakePath(current, cells[c.x, c.y]);
			}
		}

	}

	bool isVisited(int x, int y){
		if( 0 <= x && x < xSize && 0 <= y && y < ySize){
			return cells[x,y].visited;
		}
		return true;
	}

	void CreateWalls(){
//		wallHolder = new GameObject();
//		wallHolder.name = "Maze";
		allWalls = new List<GameObject>();

		initialPos = new Vector3((-xSize/2.0f) + wallLength / 2.0f, 0.0f, (ySize/2.0f) - wallLength / 2.0f);
		Vector3 myPos = initialPos;
		GameObject tempWall;


		for (int j = 0; j < ySize; j++) {
			for (int i = 0; i < xSize; i++){
				// Check the cell
				Cell cell = cells[i,j];


				// North wall
				if(j == 0){
					myPos = new Vector3 (initialPos.x + (i * wallLength), 0.0f, initialPos.z - (j * wallLength) + wallLength/2);
					tempWall = Instantiate (wall, myPos, Quaternion.Euler (0.0f, 90.0f, 0.0f)) as GameObject;
					tempWall.transform.parent = gameObject.transform;
					allWalls.Add(tempWall);
				}

				// West wall
				if(i == 0){
					myPos = new Vector3 (initialPos.x + (i * wallLength) - wallLength/2, 0.0f, initialPos.z-(j * wallLength));
					tempWall = Instantiate (wall, myPos, Quaternion.identity) as GameObject;
					tempWall.transform.parent = gameObject.transform;
					allWalls.Add(tempWall);
				}

				// East wall
				if(cell.east){
					myPos = new Vector3 (initialPos.x + (i * wallLength) + wallLength/2, 0.0f, initialPos.z-( j * wallLength));
					tempWall = Instantiate (wall, myPos, Quaternion.identity) as GameObject;
					tempWall.transform.parent = gameObject.transform;
					allWalls.Add(tempWall);
				}

				// South wall
				if(cell.south){
					myPos = new Vector3 (initialPos.x + (i * wallLength), 0.0f, initialPos.z - ( j * wallLength) - wallLength/2);
					tempWall = Instantiate (wall, myPos, Quaternion.Euler (0.0f, 90.0f, 0.0f)) as GameObject;
					tempWall.transform.parent = gameObject.transform;
					allWalls.Add(tempWall);
				}
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}

