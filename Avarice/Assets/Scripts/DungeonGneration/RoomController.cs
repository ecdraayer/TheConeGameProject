using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
	public string name;

	public int X;
	public int Y;
}

public class RoomController : MonoBehaviour
{
	public GameObject player;
    public GameObject game;
    public GameObject canvas;

	public static RoomController instance;

	string currentWorldName = "Basement";

	RoomInfo currentLoadRoomData;

	Room currRoom;

	Queue <RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

	public List<Room> loadedRooms = new List<Room>();

	bool isLoadingRoom = false;
	public bool spawnedBossRoom = false;
	bool updatedRooms = false;

	void Awake()
	{
		instance = null;
		instance = this;
	}

	void Start()
	{
		//LoadRoom("Start",0,0);
		//LoadRoom("Empty",1,0);
		//LoadRoom("Empty",-1,0);
		//LoadRoom("Empty",0,1);
		//LoadRoom("Empty",0,-1);
		

	}

	void Update()
	{
		UpdateRoomQueue();
	}

	void UpdateRoomQueue()
	{
		if(isLoadingRoom)
		{
			return;
		}
		if(loadRoomQueue.Count == 0)
		{
			//if(!spawnedBossRoom){
			//	StartCoroutine(SpawnBossRoom());
			//} 
			//else if(spawnedBossRoom && !updatedRooms)
			if(!updatedRooms)
			{
				foreach(Room room in loadedRooms)
				{
					room.RemoveUnconnectedDoors();
				}
				updatedRooms = true;
				UpdateRooms();
			}
			player.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        	game.SetActive(true);
        	canvas.SetActive(true);
			return;
		}
		currentLoadRoomData = loadRoomQueue.Dequeue();
		isLoadingRoom = true;

		StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
	}

	IEnumerator SpawnBossRoom()
	{
		spawnedBossRoom = true;
		yield return new WaitForSeconds(0.5f);
		if(loadRoomQueue.Count == 0)
		{
			Room bossRoom = loadedRooms[loadedRooms.Count - 1];
			Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
			Destroy(bossRoom.gameObject);
			var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
			loadedRooms.Remove(roomToRemove);
			LoadRoom("End", tempRoom.X, tempRoom.Y);

		}
	}

	public void LoadRoom( string name, int x, int y)
	{
		if(DoesRoomExist(x,y))
		{
			return;
		}
		RoomInfo newRoomData = new RoomInfo();
		newRoomData.name = name;
		newRoomData.X = x;
		newRoomData.Y = y;

		loadRoomQueue.Enqueue(newRoomData);
	}

	IEnumerator LoadRoomRoutine(RoomInfo info)
	{
		string roomName = currentWorldName + info.name;

		AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

		UpdateRooms();

		while(loadRoom.isDone == false)
		{
			yield return null;
		}
		
	}

	public void RegisterRoom( Room room )
	{
		if(!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y)){
			room.transform.position = new Vector3(
				currentLoadRoomData.X * room.Width,
				currentLoadRoomData.Y * room.Height,
				0
			);
			
			room.X = currentLoadRoomData.X;
			room.Y = currentLoadRoomData.Y;
			room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
			room.transform.parent = transform;

			isLoadingRoom = false;
			room.playerEntered = false;

			if(loadedRooms.Count == 0)
			{
				CameraController.instance.currRoom = room;
			}

			loadedRooms.Add(room);
		}
		else{
			Destroy(room.gameObject);
			isLoadingRoom = false;
		}
	}
   
    public bool DoesRoomExist(int x, int y)
    {
    	return loadedRooms.Find( item => item.X == x && item.Y == y ) != null;
    }

    public Room FindRoom(int x, int y)
    {
    	return loadedRooms.Find( item => item.X == x && item.Y == y );
    }


    public string GetRandomRoomName()
    {
    	string[] possibleRooms = new string[] {
    		"Empty",
    		"Chase",
    		"Treasure",
    	};

    	return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }
    public void OnPlayerEnterRoom(Room room)
    {
    		
    	if(room.playerEntered == true)
	    {
	    	if(room.portal != null)
	    	{
	    		room.portal.SetActive(false);
	    	}
	    }
	    if(room.playerEntered == true)
	    {
	    	Debug.Log("Set to True");
	    } 
	    else
	    {
	    	Debug.Log("Set to False");
	    }
    	CameraController.instance.currRoom = room;
    	currRoom =  room;

    	StartCoroutine(RoomCoroutine());
    	currRoom.playerEntered = true;
    }

    public IEnumerator RoomCoroutine()
    {
    	yield return new WaitForSeconds(1.2f);
    	UpdateRooms();
    }

    public void UpdateRooms()
    {
    	foreach(Room room in loadedRooms)
    	{
    		if(currRoom != room)
    		{

    			EnemyController[] enemies = room.GetComponentsInChildren<EnemyController>();
    			if(enemies != null)
    			{
    				foreach(EnemyController enemy in enemies)
    				{
    					enemy.notInRoom = true;
    					//Debug.Log("Enemy not in room!");
    				}

    				foreach(Door door in room.GetComponentsInChildren<Door>())
    				{
    					door.doorCollider.SetActive(false);
    				}
    			}
    			else
    			{
    				foreach(Door door in room.GetComponentsInChildren<Door>())
    				{
    					door.doorCollider.SetActive(false);
    				}
    			}
    		}
    		else
    		{
    			EnemyController[] enemies = room.GetComponentsInChildren<EnemyController>();
    			if(enemies.Length > 0)
    			{
    				foreach(EnemyController enemy in enemies)
    				{
    					enemy.notInRoom = false;
    					//Debug.Log("Enemy in room!");
    				}
    				foreach(Door door in room.GetComponentsInChildren<Door>())
    				{
    					door.doorCollider.SetActive(true);
    				}
    			}
    			else
    			{
    				foreach(Door door in room.GetComponentsInChildren<Door>())
    				{
    					door.doorCollider.SetActive(false);
    				}
    			}
    		}
    		

    	}
    }
    
}
