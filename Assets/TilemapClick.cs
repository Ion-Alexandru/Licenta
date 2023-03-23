using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapClick : MonoBehaviour
{
    public Tilemap tilemap;
    PushButton pushButton;

    public TileBase wireTile;
    public TileBase pushButtonUnpressedTile;
    public TileBase pushButtonPressedTile;
    public TileBase diodeTile;
    public TileBase unlitDiodeTile;
    public TileBase litDiodeTile;
    public TileBase groundTile;

    public bool wirePlacemenet = false;
    public bool pushButtonPlacement = false;
    public bool diodePlacement = false;
    public bool groundPlacement = false;

    public List<Wire> wireList = new List<Wire>();
    public List<PushButton> pushButtonList = new List<PushButton>();
    public List<Diode> diodeList = new List<Diode>();
    public List<Ground> groundList = new List<Ground>();

    private void Start()
    {
        
    }

    private void Update() 
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);

        pushButtonClick(cellPosition);

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            diodePlacement = true;
        } 
        
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            diodePlacement = false;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            pushButtonPlacement = true;
        } 
        
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            pushButtonPlacement = false;
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            groundPlacement = true;
        } 
        
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            groundPlacement = false;
        }

        if (pushButtonPlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            PushButton newPushButton = new PushButton(this, tilemap, cellPosition);
            pushButtonList.Add(newPushButton);

            // Set the tile at the given position to the rule tile
            tilemap.SetTile(cellPosition, pushButtonUnpressedTile);
        }

        if (diodePlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Diode newDiode = new Diode(this, tilemap, cellPosition);
            diodeList.Add(newDiode);

            // Set the tile at the given position to the rule tile
            tilemap.SetTile(cellPosition, diodeTile);
        }

        if (groundPlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ground newGround = new Ground(this, tilemap, cellPosition);
            groundList.Add(newGround);

            // Set the tile at the given position to the rule tile
            tilemap.SetTile(cellPosition, groundTile);
        }

        foreach (PushButton button in pushButtonList)
        {
            button.givePower();
            button.isPushButtonPressed(pushButtonUnpressedTile, pushButtonPressedTile);
        }

        foreach (Diode diode in diodeList)
        {
            diode.isPowred(unlitDiodeTile, litDiodeTile);
        }

        foreach (Ground ground in groundList)
        {
            ground.giveGround();
        }

        if(wirePlacemenet == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Debug.Log("Tile clicked at " + cellPosition);

            // Place the rule tile at the given position.
            // if (wireList.Count > 0)
            // {
            //     foreach (Wire wire in wireList)
            //     {
            //         if(wire.isNearExistingWirePart(cellPosition))
            //         {
            //             wire.AddWirePart(cellPosition);

            //             // Set the tile at the given position to the rule tile
            //             tilemap.SetTile(cellPosition, wireTile);

            //             Debug.Log("That part is near an existing wire");

            //             break;
            //         } 
            //         else if(wire.isWireNearWire(cellPosition, otherWire))
            //         {
            //             // Set the tile at the given position to the rule tile
            //             tilemap.SetTile(cellPosition, wireTile);

            //             Debug.Log("That wire was near another wire");

            //             break;
            //         } 
            //         else
            //         {
            //             Wire newWire = new Wire(tilemap);
            //             newWire.AddWirePart(cellPosition);
            //             wireList.Add(newWire);

            //             // Set the tile at the given position to the rule tile
            //             tilemap.SetTile(cellPosition, wireTile);

            //             Debug.Log("Here a new wire would be placed");

            //             break;
            //         }
            //     }
            // } else 
            // {
            //     Wire newWire = new Wire(tilemap);
            //     newWire.AddWirePart(cellPosition);
            //     wireList.Add(newWire);

            //     // Set the tile at the given position to the rule tile
            //     tilemap.SetTile(cellPosition, wireTile);
            // }
            if (wireList.Count > 0)
            {
                foreach (Wire wire in wireList)
                {
                    // Iterate over all other wires in the list
                    foreach (Wire otherWire in wireList)
                    {
                        if (wire != otherWire && otherWire.isWireNearWire(cellPosition, wire))
                        {
                            // Set the tile at the given position to the rule tile
                            tilemap.SetTile(cellPosition, wireTile);

                            // Debug.Log("That wire was near another wire");

                            return;
                        }
                    }

                    if(wire.isNearExistingWirePart(cellPosition))
                    {
                        wire.AddWirePart(cellPosition);

                        // Set the tile at the given position to the rule tile
                        tilemap.SetTile(cellPosition, wireTile);

                        // Debug.Log("That part is near an existing wire");

                        return;
                    } 
                }

                Wire newWire = new Wire(tilemap);
                newWire.AddWirePart(cellPosition);
                wireList.Add(newWire);

                // Set the tile at the given position to the rule tile
                tilemap.SetTile(cellPosition, wireTile);

                // Debug.Log("Here a new wire would be placed");
            }
            else
            {
                Wire newWire = new Wire(tilemap);
                newWire.AddWirePart(cellPosition);
                wireList.Add(newWire);

                // Set the tile at the given position to the rule tile
                tilemap.SetTile(cellPosition, wireTile);
            }
        }
    }

    void pushButtonClick(Vector3Int mousePosition)
    {
        if(Input.GetMouseButtonDown(0))
        {
            foreach(PushButton pushButton in pushButtonList)
            {
                if(mousePosition == pushButton.position){
                    pushButton.isPressed = true;
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            foreach(PushButton pushButton in pushButtonList)
            {
                if(mousePosition == pushButton.position){
                    pushButton.isPressed = false;
                }
            }
        }
    }
}