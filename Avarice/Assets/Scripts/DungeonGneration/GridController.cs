using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Room room;


    [System.Serializable]

    public struct Grid
    {
    	public int columns, rows;
    	public float verticalOffset,horizontalOffset;
    }

    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> availablePoints = new List<Vector2>();

    void Awake()
    {
    	room = GetComponentInParent<Room>();
    	grid.columns = room.Width - 3;
    	grid.rows = room.Height - 3;
    	GenerateGrid();
    }

    public void GenerateGrid()
    {
    	grid.verticalOffset += room.transform.localPosition.y;
    	grid.horizontalOffset += room.transform.localPosition.x;

        int x_start = 0;
        int x_end = grid.columns;
    	for(int y=0; y<grid.rows; y++)
    	{
            x_start = 0;
            x_end = grid.columns;
            

    		for(int x=x_start; x<x_end; x++)
            {
                if( (x >= 7 & x <= (grid.columns-8) & (y <= 3 | y >= (grid.rows - 4))) | (((x < 5 | x > (grid.columns-6))) & (y >= 2 & y <= (grid.rows - 3))) )
                {
                    continue;
                }
                    
    			GameObject go = Instantiate(gridTile, transform);
    			go.GetComponent<Transform>().position = new Vector2(x - (grid.columns - grid.horizontalOffset-11.5f), y - (grid.rows - grid.verticalOffset-6.0f));
    			go.name = "X: " + x + ", Y: " + y;
    			availablePoints.Add(go.transform.position);
                go.SetActive(false);
    		}
    	}

        GetComponentInParent<ObjectRoomSpawner>().InitialiseObjectSpawning();
    }
}
