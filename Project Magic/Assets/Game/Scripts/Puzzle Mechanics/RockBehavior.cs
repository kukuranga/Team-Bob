using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// If the player has requested to move
// Check if the player can move

// If the player can move
// Move correct snow blocks (THIS PART IS WORKING)

// If The player can't move 
// check if other snow blocks can move

public class RockBehavior : MonoBehaviour
{
    private int directionConnected;
    private GameObject[] Snow;
    private GameObject player;
    public Rigidbody2D rigidbody;
    private int directionMovable;

    public List<GameObject> snowBlocksConnectedToRock = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Snow = GameObject.FindGameObjectsWithTag("Snow");
    }

    // Update is called once per frame
    void Update()
    {
        truePosition();
        checkPlayerPositionStatus();
    }

    private void truePosition()
    {
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
    }


    private void checkPlayerPositionStatus()
    {
        //directionMovable = 0;
        if (player.GetComponent<Rigidbody2D>().transform.position.x == rigidbody.transform.position.x - 1 && player.GetComponent<Rigidbody2D>().transform.position.y == rigidbody.transform.position.y) // Player is to the left
        {
            directionMovable = 1;
        }
        else if (player.GetComponent<Rigidbody2D>().transform.position.x == rigidbody.transform.position.x + 1 && player.GetComponent<Rigidbody2D>().transform.position.y == rigidbody.transform.position.y) // Player is to the right
        {
            directionMovable = 2;
        }
        else if (player.GetComponent<Rigidbody2D>().transform.position.x == rigidbody.transform.position.x && player.GetComponent<Rigidbody2D>().transform.position.y == rigidbody.transform.position.y + 1) //Player is above 
        {
            directionMovable = 3;
        }
        else if (player.GetComponent<Rigidbody2D>().transform.position.x == rigidbody.transform.position.x && player.GetComponent<Rigidbody2D>().transform.position.y == rigidbody.transform.position.y - 1) // Player is below
        {
            directionMovable = 4;
        }
        else
        {
        }
    }

    public List<GameObject> findConnectedSnowBlocksToRock(List<GameObject> snowBlocks, GameObject snowBlock, int directionSnowMoveable)
    {
        for (int i = 0; i < Snow.Length; i++)
        {
            GameObject currentBlock = Snow[i].gameObject;
            if (snowBlock != currentBlock && isBumpingIntoRock(currentBlock, snowBlock, directionSnowMoveable) && currentBlock.GetComponent<BoxCollider2D>().isTrigger == true)
            {
                snowBlocks.Add(currentBlock);
                findConnectedSnowBlocksToRock(snowBlocks, currentBlock, directionSnowMoveable);
            }
            else if (isBumpingIntoRock(player.gameObject, snowBlock, directionSnowMoveable))
            {
                findConnectedSnowBlocksToRock(snowBlocks, player.gameObject, directionSnowMoveable);
            }
        }
        return snowBlocks;
    }

    private Boolean isBumpingIntoRock(GameObject alpha, GameObject pastSnow, int directionMovable)
    {
        if (directionMovable == 1)
        {

            if (alpha.GetComponent<Rigidbody2D>().transform.position.x == pastSnow.GetComponent<Rigidbody2D>().transform.position.x - 1 &&
            alpha.GetComponent<Rigidbody2D>().transform.position.y == GetComponent<Rigidbody2D>().transform.position.y)
            {
                return true;
                //print("ture");
            }
        }
        else if (directionMovable == 2)
        {
            //The player cannot push the block to the left
            if (alpha.GetComponent<Rigidbody2D>().transform.position.x == pastSnow.GetComponent<Rigidbody2D>().transform.position.x + 1 &&
            alpha.GetComponent<Rigidbody2D>().transform.position.y == GetComponent<Rigidbody2D>().transform.position.y)
            {
                return true;
            }
        }
        else if (directionMovable == 3)
        {
            //The player cannot push the block up
            if (alpha.GetComponent<Rigidbody2D>().transform.position.y == pastSnow.GetComponent<Rigidbody2D>().transform.position.y + 1 &&
             alpha.GetComponent<Rigidbody2D>().transform.position.x == this.GetComponent<Rigidbody2D>().transform.position.x)
            {

                return true;
            }
        }

        else if (directionMovable == 4)
        {
            //The player cannot push the block down
            if (alpha.GetComponent<Rigidbody2D>().transform.position.y == pastSnow.GetComponent<Rigidbody2D>().transform.position.y - 1 &&
             alpha.GetComponent<Rigidbody2D>().transform.position.x == this.GetComponent<Rigidbody2D>().transform.position.x)
            {

                return true;
            }
        }
        return false;
    }

    /*
    public GameObject isSnowBesideObject(List<GameObject> snowBlocks, int direction)
    {
        for (int i = 0; i < snowBlocks.ToArray().Length; i++)
        {
            if (rigidbody.transform.position.x - 1 == snowBlocks[i].gameObject.GetComponent<Rigidbody2D>().transform.position.x
                && rigidbody.transform.position.y == snowBlocks[i].gameObject.GetComponent<Rigidbody2D>().transform.position.y && direction == 1)
            {
                return gameObject;
            }
            else if (rigidbody.transform.position.x + 1 == snowBlocks[i].gameObject.GetComponent<Rigidbody2D>().transform.position.x
              && rigidbody.transform.position.y == snowBlocks[i].gameObject.GetComponent<Rigidbody2D>().transform.position.y && direction == 2)
            {
                return gameObject;
            }
            else if (rigidbody.transform.position.y + 1 == snowBlocks[i].gameObject.GetComponent<Rigidbody2D>().transform.position.y
            && rigidbody.transform.position.x == snowBlocks[i].gameObject.GetComponent<Rigidbody2D>().transform.position.x && direction == 3)
            {
                return gameObject;
            }
            else if (rigidbody.transform.position.y - 1 == snowBlocks[i].gameObject.GetComponent<Rigidbody2D>().transform.position.y
            && rigidbody.transform.position.x == snowBlocks[i].gameObject.GetComponent<Rigidbody2D>().transform.position.x && direction == 4)
            {
                return gameObject;
            }
        }
        return null;
    }
    */
}