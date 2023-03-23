using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireButtonScript : MonoBehaviour
{
    TilemapClick tilemapClick;

    // Start is called before the first frame update
    void Start()
    {
        // Find the TilemapClick component on a GameObject in the scene
        tilemapClick = FindObjectOfType<TilemapClick>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if(tilemapClick.wirePlacemenet == false)
        {
            tilemapClick.wirePlacemenet = true;   
            // Debug.Log("You can place Cables");
        } 
        else 
        {
            tilemapClick.wirePlacemenet = false;
            // Debug.Log("You cannot place wires");

            Debug.Log(tilemapClick.wireList.Count);
        }
    }
}
