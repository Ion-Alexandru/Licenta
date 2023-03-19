using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Wire
{
    private Tilemap tilemap;
    public List<Vector3Int> wireParts;
    private float voltage;
    private float resistance;

    public Wire(Tilemap tilemap)
    {
        this.tilemap = tilemap;
        wireParts = new List<Vector3Int>();
        voltage = 0f;
        resistance = 0f;
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

    public bool ContainsWirePart(Vector3Int position)
    {
        return wireParts.Contains(position);
    }
}