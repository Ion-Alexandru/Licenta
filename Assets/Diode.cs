using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Diode
{
    TilemapClick tilemapClick;
    private Tilemap tilemap;
    private Vector3Int position;
    bool isDiodePowered = false;
    bool isDiodeGrounded = false;

    public Diode(TilemapClick tilemapClick, Tilemap tilemap, Vector3Int position)
    {
        this.tilemapClick = tilemapClick;
        this.tilemap = tilemap;
        this.position = position;
    }

    public void isPowred(TileBase unlitDiodeTile, TileBase litDiodeTile)
    {
        foreach (Wire wire in tilemapClick.wireList)
        {
            if(wire.isPowered)
            {
                foreach (Vector3Int wirePart in wire.wireParts)
                {
                    if ((position.x - 1 == wirePart.x && position.y == wirePart.y) //||
                        // (position.x + 1 == wirePart.x && position.y == wirePart.y) ||
                        // (position.x == wirePart.x && position.y - 1 == wirePart.y) ||
                        // (position.x == wirePart.x && position.y + 1 == wirePart.y))
                       )
                    {
                        isDiodePowered = true;
                        break;
                    }
                }
            }

            if(wire.isGrounded)
            {
                foreach (Vector3Int wirePart in wire.wireParts)
                {
                    if ((position.x + 1 == wirePart.x && position.y == wirePart.y))
                    {
                        isDiodeGrounded = true;
                        break;
                    }
                }
            }

            if (isDiodePowered && isDiodeGrounded)
            {
                tilemap.SetTile(position, litDiodeTile);
                // Debug.Log("The diode was lit!");
            } else
            {
                tilemap.SetTile(position, unlitDiodeTile);
            }

            foreach (Vector3Int wirePart in wire.wireParts)
            {
                if ((position.x + 1 == wirePart.x && position.y == wirePart.y) && isDiodePowered)
                {
                    wire.isPowered = true;
                    // Debug.Log("A wire was powered by a diode!");
                }
                
                if ((position.x - 1 == wirePart.x && position.y == wirePart.y) && isDiodeGrounded)
                {
                    wire.isGrounded = true;
                    // Debug.Log("A wire was grounded by a diode!");
                }
            }
        }
    }
}