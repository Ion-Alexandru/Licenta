using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapClick : MonoBehaviour
{
    public Tilemap tilemap;

    public TileBase ruleTile;

    public bool cablePlacemenet = false;

    public List<Wire> Wires = new List<Wire>();

    private void Start()
    {
        
    }

    private void Update() 
    {
        // if(Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     cablePlacemenet = true;

        // } 
        
        // if (Input.GetKeyUp(KeyCode.Alpha1) && cablePlacemenet == true)
        // {
        //     cablePlacemenet = false;

        //     Debug.Log("You cannot place Cables anymore!");
        // }

        if(cablePlacemenet == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
            //Debug.Log("Tile clicked at " + cellPosition);

            // Place the rule tile at the given position.
            if (Wires.Count > 0)
            {
                bool connectedToExistingWire = false;
                Wire existingWire = null;
                Vector3Int existingWirePart = default;

                // Check if there is already a wirePart on the right, left, up or down of the ruleTile
                foreach (Wire w in Wires)
                {
                    foreach (Vector3Int wirePart in w.wireParts)
                    {
                        if ((wirePart.x == cellPosition.x + 1 && wirePart.y == cellPosition.y) ||
                            (wirePart.x == cellPosition.x - 1 && wirePart.y == cellPosition.y) ||
                            (wirePart.x == cellPosition.x && wirePart.y == cellPosition.y + 1) ||
                            (wirePart.x == cellPosition.x && wirePart.y == cellPosition.y - 1))
                        {
                            connectedToExistingWire = true;
                            existingWire = w;
                            existingWirePart = wirePart;
                            break;
                        }
                    }

                    if (connectedToExistingWire)
                    {
                        break;
                    }
                }

                if (connectedToExistingWire)
                {
                    // Add the new wirePart to the existing wire
                    existingWire.AddWirePart(cellPosition);

                    // Merge existing wire with other wires that are connected to the existing wire part
                    foreach (Wire w in Wires)
                    {
                        if (w != existingWire && w.ContainsWirePart(existingWirePart))
                        {
                            existingWire.MergeWith(w);
                            Wires.Remove(w);
                        }
                    }

                    Debug.Log("Connected to existing wire.");
                }
                else
                {
                    // Create a new wire for the tile
                    Wire newWire = new Wire(tilemap);
                    newWire.AddWirePart(cellPosition);
                    Wires.Add(newWire);

                    Debug.Log("Created new wire for tile at " + cellPosition);
                }
            }
            else
            {
                // Create a new wire for the tile
                Wire newWire = new Wire(tilemap);
                newWire.AddWirePart(cellPosition);
                Wires.Add(newWire);

                Debug.Log("Created new wire for tile at " + cellPosition);                            

                // Wire wire = new Wire(tilemap, cellPosition);
                // wire.UpdateConnectedTiles(cellPosition);
                // tilemap.SetTile(cellPosition, ruleTile);

                // wire.Voltage = 12f;
                // wire.Resistance = 0.5f;
            }

            // Verify if there is already a wire at that cellPosition
            bool wireExistsAtPosition = false;
            foreach (Wire w in Wires)
            {
                if (w.ContainsWirePart(cellPosition))
                {
                    wireExistsAtPosition = true;
                    break;
                }
            }

            if (!wireExistsAtPosition)
            {
                // Create a new wire for the tile
                Wire newWire = new Wire(tilemap);
                Wires.Add(newWire);
                Debug.Log("Created new wire for tile at " + cellPosition);

                // Set the tile at the given position to the rule tile
                tilemap.SetTile(cellPosition, ruleTile);
            }
            else
            {
                Debug.Log("Wire already exists at " + cellPosition);
            }
        }
    }
}