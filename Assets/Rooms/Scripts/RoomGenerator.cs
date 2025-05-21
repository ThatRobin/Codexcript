using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomGenerator : MonoBehaviour {

    [SerializeField] Tilemap tilemap;

    [SerializeField] Tilemap startRoom;
    [SerializeField] RuleTile hallwayTile;




    private void Start() {
        tilemap = this.GetComponent<Tilemap>();
        GenerateRoomFromTilemap(startRoom);

        GenerateRandomRoom(startRoom, new Vector2Int(0, 0));
        GenerateRandomRoom(startRoom, new Vector2Int(0, 0));

        //GenerateRoom(10, 10, 0,0);
    }

    private void GenerateRandomRoom(Tilemap map, Vector2Int offset) {
        Vector2Int pos = Vector2Int.zero;
        while (pos.magnitude < 13) {
            pos = Vector2Int.RoundToInt(Random.insideUnitCircle * 25);
        }
        pos += offset;
        GenerateRoomFromTilemap(map, pos.x, pos.y);
    }

    public void GenerateRoomFromTilemap(Tilemap map, int xPos = 0, int yPos = 0) {
        BoundsInt newBounds = map.cellBounds;
        int hallways = 2;
        for (int x = newBounds.xMin; x < newBounds.xMax; x++) {
            for (int y = newBounds.yMin; y < newBounds.yMax; y++) {
                if (hallways > 0) {
                    Vector2Int otherRoomPos = new Vector2Int(x - 5 + xPos, y - 5 + yPos);
                    Vector2Int startRoomPos = new Vector2Int(0, 0);
                    createHallway(startRoomPos, otherRoomPos);
                    hallways--;
                }
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                if (map.HasTile(tilePos)) {
                    tilemap.SetTile(new Vector3Int(x - 5 + xPos, y - 5 + yPos, 0), map.GetTile(tilePos));
                }
            }
        }
    }

    public void createHallway(Vector2Int startPoint, Vector2Int endPoint) {
        Vector2Int difference = endPoint - startPoint;

        int steps = Mathf.Max(Mathf.Abs(difference.x), Mathf.Abs(difference.y));

        if(steps > 0) {
            for (int i = 0; i <= steps; i++) {
                Vector3Int position = new Vector3Int(
                    Mathf.RoundToInt(startPoint.x + difference.x * i / steps),
                    Mathf.RoundToInt(startPoint.y + difference.y * i / steps),
                    0);

                tilemap.SetTile(position, hallwayTile);
            }
        }
    }

    /*
    public void GenerateRoom(int width, int height, int xPos, int yPos) {
        int xPos2 = xPos + width;
        int yPos2 = yPos + height;
        if (Random.Range(0,1) == 0) {
            xPos2 = Random.Range(xPos, xPos + width);
        } else {
            yPos2 = Random.Range(yPos, yPos + height);
        }
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Vector3Int pos = new Vector3Int(xPos + x, yPos + y, 0);
                tilemap.SetTile(pos, wallTile);
            }
        }
        int xSize = Random.Range(1, 10);
        int ySize = Random.Range(1, 10);
        
    }
    */
}
