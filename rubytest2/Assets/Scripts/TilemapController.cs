using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
	public Tilemap tileMap;
    public float minX = 100.0f;
    public float minY = 100.0f;
    public float maxX = -100.0f;
    public float maxY = -100.0f;
 
    void Start () {
        tileMap = transform.GetComponentInParent<Tilemap>();
 
        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Debug.Log("adding tile position");
                    //Debug.Log(place);
                    //Tile at "place"
                    if(place.x < minX){
                        minX = place.x;
                    }
                    if(place.y < minY){
                        minY = place.y;
                    }
                    if(place.x > maxX){
                        maxX = place.x;
                    }
                    if(place.y > maxY){
                        maxY = place.y;
                    }                    
                }
            }
        }
        Debug.Log("Tile world minx, miny, maxX, maxY");
        Debug.Log("x " + minX + " to " + maxX + " , " + " y " + minY + " to " + maxY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
