using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerToTile : MonoBehaviour
{
    Tilemap tilemap;
    public Tile selectedTile;

    public Tile nature_Tile;
    public Tile science_Tile;
    public Tile magic_Tile;

    public Button nature_Button;
    public Button science_Button;
    public Button magic_Button;
    Vector3Int cellPosition;
    public Vector3Int lastCellSelect;
    PointerEventData eventData;
    void Start()
    {
        //Get components inside the children of the Tilemap
        tilemap = GetComponentInChildren<Tilemap>();
        //Initiate buttons
        Button ntr_Btn = nature_Button.GetComponent<Button>();
        ntr_Btn.onClick.AddListener(delegate{MaterialOnClick("Nature");});
        Button sci_Btn = science_Button.GetComponent<Button>();
        sci_Btn.onClick.AddListener(delegate{MaterialOnClick("Science");});
        Button mgc_Btn = magic_Button.GetComponent<Button>();
        mgc_Btn.onClick.AddListener(delegate{MaterialOnClick("Magic");});
        //Initiate the selected Tile as the nature one
        MaterialOnClick("Nature");
        //Resets colors for all grid Hexes
        for(int x = -10; x > 11; x++){
            for(int y = -10; y > 11; y++){
                Tile resetTile = tilemap.GetTile<Tile>(new Vector3Int(x,y,0));
                resetTile.color = Color.white;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0){
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit)
            {
                GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
                cellPosition = gridLayout.WorldToCell(hit.point);
                if(cellPosition != lastCellSelect){
                    Tile coloredTile = tilemap.GetTile<Tile>(cellPosition);
                    if(coloredTile != null){
                        coloredTile.color = Color.red;
                        tilemap.SetTile(cellPosition, coloredTile);
                        tilemap.RefreshTile(cellPosition);
                    }
                    Tile lastTile = tilemap.GetTile<Tile>(lastCellSelect);
                    if(lastTile != null){
                        lastTile.color = Color.white;
                        tilemap.SetTile(lastCellSelect, lastTile);
                        tilemap.RefreshTile(lastCellSelect);
                    }
                    lastCellSelect = cellPosition;
                }
            }
        }
        if(Input.GetMouseButtonDown(0)){
            Debug.Log(cellPosition);
            Tile thisTile = tilemap.GetTile<Tile>(cellPosition);
            if(cellPosition != new Vector3Int(0,10,0) && cellPosition != new Vector3Int(0,-10,0)){
                if(thisTile != null){
                    tilemap.SetTile(cellPosition, selectedTile);
                    tilemap.RefreshTile(cellPosition);
                }
            }
        }
    }
    void MaterialOnClick(string material){
        switch(material){
            case "Nature":
                selectedTile = nature_Tile;
                break;
            case "Science":
                selectedTile = science_Tile;
                break;
            case "Magic":
                selectedTile = magic_Tile;
                break;
        }
    }
}
