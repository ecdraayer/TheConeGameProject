using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms = new List<Vector2Int>();
    private int chance;
    
    //private static bool canDo = false;

    //public static bool CanDo{get => canDo; set => canDo=value;}

    private void Start()
    {
        //player.SetActive(false);
        //game.SetActive(false);
        //Random.seed = System.DateTime.Now.Millisecond;
        //Debug.Log("Called");
    	dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        
    	SpawnRoom(dungeonRooms);


    }

    private void SpawnRoom(IEnumerable<Vector2Int> rooms)
    {
        int roomCounter = 0;
    	RoomController.instance.LoadRoom("Start",0,0);
    	foreach(Vector2Int roomLocation in rooms)
    	{
            // 
            roomCounter += 1;
            if(roomCounter%6==0 && !(roomLocation == Vector2Int.zero) )
            {
                RoomController.instance.LoadRoom("End",roomLocation.x, roomLocation.y);
            }
            else
            {
                if(GameController.Level >= 10 && roomCounter > 10){
                    chance = Random.Range(0,100);
                    if(chance >= 95){
                        RoomController.instance.LoadRoom("Boss", roomLocation.x, roomLocation.y);
                    }
                    else
                    {
                        RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName(), roomLocation.x, roomLocation.y);
                    }
                }
                else
                {
                    RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName(), roomLocation.x, roomLocation.y);
                }
    		    
            }
    	}

        
       

    }
}
