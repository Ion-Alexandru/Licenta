using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapClick : MonoBehaviour
{
    public Tilemap tilemap;

    public TileBase ruleTile;

    public bool cablePlacemenet = false;

    public List<Wire> WireList = new List<Wire>();

    private void Start()
    {
        
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            cablePlacemenet = true;

        } 
        
        if (Input.GetKeyUp(KeyCode.Alpha1) && cablePlacemenet == true)
        {
            cablePlacemenet = false;

            Debug.Log("You cannot place wires anymore!");

            Debug.Log(WireList.Count);
        }

        if(cablePlacemenet == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
            //Debug.Log("Tile clicked at " + cellPosition);

            // Place the rule tile at the given position.
            // if (WireList.Count > 0)
            // {
            //     foreach (Wire wire in WireList)
            //     {
            //         if(wire.isNearExistingWirePart(cellPosition))
            //         {
            //             wire.AddWirePart(cellPosition);

            //             // Set the tile at the given position to the rule tile
            //             tilemap.SetTile(cellPosition, ruleTile);

            //             Debug.Log("That part is near an existing wire");

            //             break;
            //         } 
            //         else if(wire.isWireNearWire(cellPosition, otherWire))
            //         {
            //             // Set the tile at the given position to the rule tile
            //             tilemap.SetTile(cellPosition, ruleTile);

            //             Debug.Log("That wire was near another wire");

            //             break;
            //         } 
            //         else
            //         {
            //             Wire newWire = new Wire(tilemap);
            //             newWire.AddWirePart(cellPosition);
            //             WireList.Add(newWire);

            //             // Set the tile at the given position to the rule tile
            //             tilemap.SetTile(cellPosition, ruleTile);

            //             Debug.Log("Here a new wire would be placed");

            //             break;
            //         }
            //     }
            // } else 
            // {
            //     Wire newWire = new Wire(tilemap);
            //     newWire.AddWirePart(cellPosition);
            //     WireList.Add(newWire);

            //     // Set the tile at the given position to the rule tile
            //     tilemap.SetTile(cellPosition, ruleTile);
            // }
            if (WireList.Count > 0)
            {
                foreach (Wire wire in WireList)
                {
                    // Iterate over all other wires in the list
                    foreach (Wire otherWire in WireList)
                    {
                        if (wire != otherWire && otherWire.isWireNearWire(cellPosition, wire))
                        {
                            // Set the tile at the given position to the rule tile
                            tilemap.SetTile(cellPosition, ruleTile);

                            Debug.Log("That wire was near another wire");

                            return;
                        }
                    }

                    if(wire.isNearExistingWirePart(cellPosition))
                    {
                        wire.AddWirePart(cellPosition);

                        // Set the tile at the given position to the rule tile
                        tilemap.SetTile(cellPosition, ruleTile);

                        Debug.Log("That part is near an existing wire");

                        return;
                    } 
                }

                Wire newWire = new Wire(tilemap);
                newWire.AddWirePart(cellPosition);
                WireList.Add(newWire);

                // Set the tile at the given position to the rule tile
                tilemap.SetTile(cellPosition, ruleTile);

                Debug.Log("Here a new wire would be placed");
            }
            else 
            {
                Wire newWire = new Wire(tilemap);
                newWire.AddWirePart(cellPosition);
                WireList.Add(newWire);

                // Set the tile at the given position to the rule tile
                tilemap.SetTile(cellPosition, ruleTile);
            }
        }
    }
}