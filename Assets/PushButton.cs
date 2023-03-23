using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class PushButton
{
    TilemapClick tilemapClick;
    private Tilemap tilemap;
    public Vector3Int position;
    public bool buttonIsPowered = false;
    public bool isPressed = false;

    public PushButton(TilemapClick tilemapClick, Tilemap tilemap, Vector3Int position)
    {
        this.tilemapClick = tilemapClick;
        this.tilemap = tilemap;
        this.position = position;
    }

    public void givePower()
    {
        if(buttonIsPowered)
        {
            foreach(Wire wire in tilemapClick.wireList)
            {
                bool isPowered = false;

                bool hasAdjacentWirePart = false;
                foreach(Vector3Int wirePart in wire.wireParts)
                {
                    if((position.x - 1 == wirePart.x && position.y == wirePart.y) ||
                        (position.x + 1 == wirePart.x && position.y == wirePart.y) ||
                        (position.x == wirePart.x && position.y - 1 == wirePart.y) ||
                        (position.x == wirePart.x && position.y + 1 == wirePart.y))
                    {
                        hasAdjacentWirePart = true;
                        if(buttonIsPowered)
                        {
                            isPowered = true;
                            break;
                        }
                    }
                }

                if(!hasAdjacentWirePart || !buttonIsPowered)
                {
                    isPowered = false;
                }

                // Set the wire's powered state based on the result of the loop
                wire.isPowered = isPowered;

                if(isPowered)
                {
                    // Debug.Log("A wire is now powered! " + wire);
                }
                else
                {
                    // Debug.Log("A wire is now unpowered! " + wire);
                }
            }
        }
    }

    public void isPushButtonPressed(TileBase pushButtonUnpressed, TileBase psuhButtonPressed)
    {
        if(isPressed){
            tilemap.SetTile(position, psuhButtonPressed);
            buttonIsPowered = true;
        } else
        {
            tilemap.SetTile(position, pushButtonUnpressed);
            buttonIsPowered = false;
        }
    }
}