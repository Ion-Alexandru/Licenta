using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Wire
{
    private Tilemap tilemap;
    public List<Vector3Int> wireParts;
    public bool isPowered = false;
    public bool isGrounded = false;

    public Wire(Tilemap tilemap)
    {
        this.tilemap = tilemap;
        wireParts = new List<Vector3Int>();
    }

    public void AddWirePart(Vector3Int position)
    {
        wireParts.Add(position);
    }

    public void MergeWith(Wire other)
    {
        List<Vector3Int> wirePartsToRemove = new List<Vector3Int>();
        foreach (Vector3Int wirePart in other.wireParts)
        {
            if (!wireParts.Contains(wirePart))
            {
                AddWirePart(wirePart);
            }
            else
            {
                wirePartsToRemove.Add(wirePart);
            }
        }

        foreach (Vector3Int wirePartToRemove in wirePartsToRemove)
        {
            wireParts.Remove(wirePartToRemove);
        }
    }

    public bool isNearExistingWirePart(Vector3Int position)
    {
        foreach (Vector3Int wirePart in wireParts)
        {
            if( position.x - 1 == wirePart.x && position.y == wirePart.y ||
                position.x + 1 == wirePart.x && position.y == wirePart.y ||
                position.x == wirePart.x && position.y - 1 == wirePart.y ||
                position.x == wirePart.x && position.y + 1 == wirePart.y )
            {
                return true;
            }
        }

        return false;
    }

    public bool isWireNearWire(Vector3Int position, Wire otherWire)
    {
        foreach(Vector3Int wirePart in wireParts)
        {
            if( position.x - 1 == wirePart.x && position.y == wirePart.y ||
                position.x + 1 == wirePart.x && position.y == wirePart.y ||
                position.x == wirePart.x && position.y - 1 == wirePart.y ||
                position.x == wirePart.x && position.y + 1 == wirePart.y )
            {
                foreach (Vector3Int wirePartOther in otherWire.wireParts)
                {
                    if( position.x - 1 == wirePartOther.x && position.y == wirePartOther.y ||
                        position.x + 1 == wirePartOther.x && position.y == wirePartOther.y ||
                        position.x == wirePartOther.x && position.y - 1 == wirePartOther.y ||
                        position.x == wirePartOther.x && position.y + 1 == wirePartOther.y )
                    {
                        // Merge the wires
                        MergeWith(otherWire);
                        return true;
                    }
                }
            }
        }
        return false;
    }
}