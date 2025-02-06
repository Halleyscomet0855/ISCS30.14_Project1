using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]

public class TileData : ScriptableObject
{
    public TileBase[] tiles;
    
    public bool Passable;

    public bool OnewayMove;
    public bool ForcedMove;

    public bool Speedy;

    public bool OneWay;

    public int WayDirection;

    public Vector3 Direction;
}
