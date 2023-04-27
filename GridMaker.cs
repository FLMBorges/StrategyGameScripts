using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMaker : MonoBehaviour
{
    public Tile tileGrass;
    public Tile tileMag;
    public Tile tileGround;
    public Tile tileSci;
    public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        tilemap.SetTile(new Vector3Int(0,0,0), tileGround);
        tilemap.SetTile(new Vector3Int(9,1,0), tileGrass);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
