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
        if(tilemapClick.cablePlacemenet == false)
        {
            tilemapClick.cablePlacemenet = true;   
            // Debug.Log("You can place Cables");
        } 
        else 
        {
            tilemapClick.cablePlacemenet = false;
            Debug.Log("You cannot place Cables");

            Debug.Log(tilemapClick.WireList.Count);
        }
    }
}
