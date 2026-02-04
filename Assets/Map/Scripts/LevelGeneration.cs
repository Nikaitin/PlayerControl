using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public float size = 30;
    Vector2 worldSize = new Vector2(4, 4);  //radius of the world is 4 =>world is 8x8. Note: this radius is being defined by vectors
    Room[,] rooms; //create arrays that hold grid of rooms with any dimensions types like 2 dimension or 3...
    List<Vector2> takenPositions = new List<Vector2>(); //list that remember every coordinate of placed rooms
    int gridSizeX, gridSizeY; //sizes of the grids in integers
    public int numORooms = 15;
    public GameObject MapSprite;  //access to the prefab that holds all the sprites of rooms to create

    void Start()
    {
        if (numORooms >= (worldSize.x * 2) * (worldSize.y * 2))                //if the number of rooms is bigger than the size of the world can hold, reduce it down to a max number can hold
            numORooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));

        gridSizeX = Mathf.RoundToInt(worldSize.x);      //converts the size of world from vectors to integers to use
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        CreateRooms();
        SetRoomDoors();
        DrawMap();
    }

    void CreateRooms()
    {
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];     //Define the size of the array
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, 1);    //rooms[4,4]= room in the center and type(starting room). This is referencing the code from Room.cs
        takenPositions.Insert(0, Vector2.zero); //Add the starting room just assigned to the list of taken rooms at number 0 at [0,0]
        Vector2 checkPos = Vector2.zero;    //position of start room is (0,0) in vector

        //magic numbers
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
        //add rooms
        for (int i = 0; i < numORooms - 1; i++) //create the next numORooms - 1 cause we already created the start room
        {
            float randomPerc = i / ((float)numORooms - 1);  //percentage will be go from 0 to closer to 1 as loops go on
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc); //grab a value between 0.2 to 0.01, earlier loops will be closer 0.2 and vice versa
            //grab new position avaiable to create room
            checkPos = NewPosition();

            //test new position
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)    //this will more likely to happen as more rooms are created
            {
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                }
                while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);    //find a position where this room is only connected to 1 room (the room that isnt crowded)
                if (iterations >= 50)   //more than 50 is bad
                    print("error: could not create with fewer neighbors than :" + NumberOfNeighbors(checkPos, takenPositions));
            }
            //finalize position
            rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, 0);    //assigned the positions of rooms ready and 0 is normal rooms
            takenPositions.Insert(0, checkPos); //mark the positions as taken
        }
    }

    Vector2 NewPosition()   //find position avaiable to create room
    {
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));    //choose a random room already created
            x = (int)takenPositions[index].x;       //get the vector of that room
            y = (int)takenPositions[index].y;
            bool UpDown = Random.value < 0.5f;      //assigned true or false random to up down to determine whether to create the next room in up or down
            bool positive = Random.value < 0.5f;    //same for left and right
            if (UpDown)
            {
                if (positive) y++;      //create room up or down
                else y--;
            }
            else
            {
                if (positive) x++;  //if didnt create room in up or down, create in left or right
                else x--;
            }
            checkingPos = new Vector2(x, y);    //check the position of the room going to be created
        }
        while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);   //doesnt create the create the room if its out of the map, and try again to be inside
        return checkingPos;     //return the position available to create next room
    }

    Vector2 SelectiveNewPosition()  //find position to create a room that is connected to a room that isnt crowded
    {
        int index = 0, inc = 0;
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            inc = 0;
            do
            {
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));    //choose a random created room
                inc++;
            }
            while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100); //starting from the start room, this function keep trying to find a room that only has 1 neighbor
            //the inc is a failsafe to limit the number of attempts to find to 100
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;       //proceed to create new room there
            bool UpDown = Random.value < 0.5f;
            bool positive = Random.value < 0.5f;
            if (UpDown)
            {
                if (positive) y++;
                else y--;
            }
            else
            {
                if (positive) x++;
                else x--;
            }
            checkingPos = new Vector2(x, y);
        }
        while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        if (inc >= 100) print("Error: could not find position with only one neighbor");
        return checkingPos;
    }

    int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int ret = 0;
        if (usedPositions.Contains(checkingPos + Vector2.left)) ret++;      //check each side of the room if there is a room there
        if (usedPositions.Contains(checkingPos + Vector2.right)) ret++;
        if (usedPositions.Contains(checkingPos + Vector2.up)) ret++;
        if (usedPositions.Contains(checkingPos + Vector2.down)) ret++;
        return ret;     //return the numbers of rooms next to it
    }

    void SetRoomDoors()
    {
        //checking every position in the map to see if theres a room besides that position
        for (int x = 0; x < (gridSizeX * 2); x++)
        {
            for (int y = 0; y < (gridSizeY * 2); y++)
            {
                if (rooms[x, y] == null) continue;

                Vector2 gridPosition = new(x, y);
                if (y - 1 < 0) rooms[x, y].doorBot = false;          //check above
                else rooms[x, y].doorBot = rooms[x, y - 1] != null;

                if (y + 1 >= gridSizeY * 2) rooms[x, y].doorTop = false;   //check bellow
                else rooms[x, y].doorTop = rooms[x, y + 1] != null;

                if (x - 1 < 0) rooms[x, y].doorLeft = false;              //check left
                else rooms[x, y].doorLeft = rooms[x - 1, y] != null;

                if (x + 1 >= gridSizeX * 2) rooms[x, y].doorRight = false;              //check right
                else rooms[x, y].doorRight = rooms[x + 1, y] != null;
            }
        }
    }
    //checking
    void DrawMap()  //creating the room at the assigned positions
    {
        foreach (Room room in rooms)
        {
            if (room == null) continue;

            Vector2 drawPos = room.gridPos; //find the position
            drawPos.x *= size;
            drawPos.y *= size;
            MapSpriteSelector mapper = Instantiate(MapSprite, drawPos, Quaternion.identity).GetComponent<MapSpriteSelector>();    //Actually create the room with the sprite it should be
            mapper.type = room.type;    //checking the type of sprite
            mapper.up = room.doorTop;
            mapper.down = room.doorBot;
            mapper.left = room.doorLeft;
            mapper.right = room.doorRight;
        }
    }
}
