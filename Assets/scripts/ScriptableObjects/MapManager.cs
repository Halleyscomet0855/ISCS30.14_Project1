using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
public class MapManager : MonoBehaviour
{
    
    [SerializeField]
    private Tilemap map;
    [SerializeField]
    private List<TileData> tileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles;
    
    private void Awake() {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var tileData in tileDatas) {
            foreach (var tile in tileData.tiles) {
                dataFromTiles.Add(tile, tileData);
            }
        }

    }
    private void Update() {
    if(Input.GetMouseButton(0)) {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = map.WorldToCell(mousePosition);

       
        TileBase clickedTile = map.GetTile(gridPosition);
        bool passable = dataFromTiles[clickedTile].ForcedMove;

        print(clickedTile + " is " + passable);

    }
    }

    public bool GetPassable(Vector2 worldPosition) {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);

        TileBase tile = map.GetTile(gridPosition);

        bool passable = dataFromTiles[tile].Passable;

        return passable;
    }
     public bool GetSpeedy(Vector2 worldPosition) {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);

        TileBase tile = map.GetTile(gridPosition);

        bool Speedy = dataFromTiles[tile].Speedy;

        return Speedy;
    }
    public bool GetOneWay(Vector2 worldPosition) {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);

        TileBase tile = map.GetTile(gridPosition);

        bool OneWay = dataFromTiles[tile].OnewayMove;

        return OneWay;
    }

    public bool GetCliff(Vector2 worldPosition) { 
        Vector3Int gridPosition = map.WorldToCell(worldPosition);

        TileBase tile = map.GetTile(gridPosition);

        bool Waycheck = dataFromTiles[tile].OneWay;

        return Waycheck;
    }

    //So the Cliff was an unimplemented tile that was supposed to be included, but we ran out of time to do so.
    //How it was supposed to work is that each Cliff tile will store a direction int, which is the only direction 
    // you can enter. If your direction (determined via OneWayTest in gridmovement.cs) equalled the Cliff direction,
    //Movement will occur, otherwise, pass. I was furiously writing this and nearly had it before we got called. rip lmao

    public int GetWayDirection(Vector2 worldPosition) {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);

        TileBase tile = map.GetTile(gridPosition);

        int WayDirection = dataFromTiles[tile].WayDirection;

        return WayDirection;
    }

    public bool GetForcedWay(Vector2 worldPosition) {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);

        TileBase tile = map.GetTile(gridPosition);

        bool ForcedWay = dataFromTiles[tile].ForcedMove;

        return ForcedWay;
    }

    public Vector3 GetForcedDirection(Vector2 worldPosition) {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);

        TileBase tile = map.GetTile(gridPosition);

        Vector3 ForcedDirection = dataFromTiles[tile].Direction;

        return ForcedDirection;
    }
}
