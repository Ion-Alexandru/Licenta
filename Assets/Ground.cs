using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Ground
{
    TilemapClick tilemapClick;
    private Tilemap tilemap;
    private Vector3Int position;

    public Ground(TilemapClick tilemapClick, Tilemap tilemap, Vector3Int position)
    {
        this.tilemapClick = tilemapClick;
        this.tilemap = tilemap;
        this.position = position;
    }

    public void giveGround()
    {
        foreach (Wire wire in tilemapClick.wireList)
        {
            bool isGrounded = false;

            foreach (Vector3Int wirePart in wire.wireParts)
            {
                if ((position.x - 1 == wirePart.x && position.y == wirePart.y) ||
                    (position.x + 1 == wirePart.x && position.y == wirePart.y) ||
                    (position.x == wirePart.x && position.y - 1 == wirePart.y) ||
                    (position.x == wirePart.x && position.y + 1 == wirePart.y))
                {
                    isGrounded = true;
                    break;
                }
            }

            wire.isGrounded = isGrounded;

            if (isGrounded)
            {
                // Debug.Log("A wire is now grounded! " + wire);
            }
            else
            {
                // Debug.Log("A wire is now ungrounded! " + wire);
            }
        }
    }
}