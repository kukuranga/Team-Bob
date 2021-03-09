using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Author: Benjamin Kerr
Date: 3/9/2021
Description: This script is responsible for handaling the interaction between the player and all objects. 
Objects include AI and stationary objects
*/
public class PlayerInteraction : MonoBehaviour
{

    private InteractableObject objectWithInteraction;

    // Start is called before the first frame update
    void Start()
    {
        objectWithInteraction = null;
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    public void Interact()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x,mousePos.y,10);
            print(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position,mousePos,40);
            if(hit)
            {
                if(hit.collider.gameObject.GetComponent<InteractableObject>() != null)
                {
                    // Do interaction
                }
            }
        }
    }
}
