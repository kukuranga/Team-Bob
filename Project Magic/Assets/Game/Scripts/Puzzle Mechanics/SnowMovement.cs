using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SnowMovement : MonoBehaviour
{
      private GameObject player;
      private GameObject[] Snow;
      private GameObject[] Rock;
      public GameObject waterSnowIsIn;
      private GameObject controller;
      public List<GameObject> purpleSnow;
      public int directionMovable;
      private Rigidbody2D rigidbody;
      public float zLevel;
      public bool moveable;
      private int playerCanMove;
      public bool isStatic;
      public int movementSpeed;
      public bool isMoving;
      private bool partOfGroup;
      private Vector3 directionVector;
      public Vector3 onIceDirectionVector;
      private Vector3 snowsEndingPosition;
      private bool bloackingSnow;
      private List<GameObject> immoveableObjects;
      private List<Vector3> positions;
      public int times = 0;

      [SerializeField] private int distance; 


      // Start is called before the first frame update
      void Awake()
      {
            movementSpeed = 10;
            positions = new List<Vector3>();
            immoveableObjects = new List<GameObject>();
            partOfGroup = false;
            zLevel = 1;
            rigidbody = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player");
            Snow = GameObject.FindGameObjectsWithTag("Pushable");
            Rock = GameObject.FindGameObjectsWithTag("NonPushable");

            directionMovable = 0;
            moveable = true;
            isStatic = false;
            bloackingSnow = false;
      }

      public void truePosition()
      {
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), zLevel);
      }

      // Update is called once per frame
      void Update()
      {
            if (isMoving == true && partOfGroup == true)
            {
                  transform.Translate(directionVector * Time.deltaTime);
            }
            else if (isMoving == false)
            {
                  truePosition();
                  checkIfMoving();
            }
      }

      private void checkIfMoving()
      {
            float distance = Vector2.Distance(transform.position,player.transform.position);
            if(distance <= 1 && isMoving == false)
            {
                  checkPlayerPositionStatus();
                  checkIfStatic(this.gameObject);
                  moveSnowBlocks(this.gameObject);
            }
      }

      private bool inRange(float distanceAway, bool isVertical)
      {
            Vector2 a = transform.position;
            Vector2 b;
            Vector2 c;
            Vector2 p = player.transform.position;
            float angle = 20 * Mathf.Deg2Rad;
            // r^2 = x^2 + y^2
            if(isVertical == true)
            {
                  b = new Vector2(a.x + distanceAway*Mathf.Tan(angle),a.y + distanceAway);
                  c = new Vector2(a.x - distanceAway*Mathf.Tan(angle),a.y + distanceAway);
            }
            else{
                  b = new Vector2(a.x + distanceAway, a.y + distanceAway*Mathf.Tan(angle));
                  c = new Vector2(a.x + distanceAway, a.y - distanceAway*Mathf.Tan(angle));
            }

            float w1 = (a.x * (c.y - a.y) + (p.y - a.y) * (c.x - a.x) - p.x * (c.y - a.y)) / ((b.y - a.y) * (c.x - a.x) - (b.x - a.x) * (c.y - a.y));
            float w2 = (p.y - a.y - w1 * (b.y - a.y)) / (c.y - a.y);

            if (w1 >= 0 && w2 >= 0 && (w1 + w2) <= 1)
            {
                  return true;
            }
            else
            {
                  return false;
            }
      }

      public void checkPlayerPositionStatus()
      {
            if (inRange(-5,false)) // Player is to the left
            {
                  //print("Player to left");
                  this.directionMovable = 1;
            }
            else if (inRange(5,false)) // Player is to the right
            {
                  //print("Player to right");
                  this.directionMovable = 2;
            }
            else if (inRange(5,true)) //Player is above 
            {
                  //print("Player above");
                  this.directionMovable = 3;
            }
            else if (inRange(-5,true)) // Player is below
            {
                  //print("Player below");
                  this.directionMovable = 4;
            }
      }

      private bool determinVertical()
      {
            if(inRange(-5,true) && inRange(5,true))
            {
                  return true;
            }
            else if (inRange(-5,false) && inRange(5,false))
            {
                  return false;
            }
            return false;
      }

      public int getSnowPositionAroundRock(int directionMoveableOfSnow)
      {
            int directionConnectedToRock = 0;
            for (int r = 0; r < Rock.Length; r++)
            {
                  if (directionMoveableOfSnow == 1 && Rock[r].GetComponent<Rigidbody2D>().transform.position.y == rigidbody.transform.position.y
                      && Rock[r].GetComponent<Rigidbody2D>().transform.position.x - 2 == rigidbody.transform.position.x)
                  {
                        directionConnectedToRock = 1; // Left
                        return directionConnectedToRock;
                  }
                  else if (directionMoveableOfSnow == 2 && Rock[r].GetComponent<Rigidbody2D>().transform.position.y == rigidbody.transform.position.y
                      && Rock[r].GetComponent<Rigidbody2D>().transform.position.x + 2 == rigidbody.transform.position.x)
                  {
                        directionConnectedToRock = 2; // Right
                        return directionConnectedToRock;
                  }
                  else if (directionMoveableOfSnow == 4 && Rock[r].GetComponent<Rigidbody2D>().transform.position.x == rigidbody.transform.position.x
                      && Rock[r].GetComponent<Rigidbody2D>().transform.position.y - 2 == rigidbody.transform.position.y)
                  {
                        directionConnectedToRock = 3; //up
                        return directionConnectedToRock;
                  }
                  else if (directionMoveableOfSnow == 3 && Rock[r].GetComponent<Rigidbody2D>().transform.position.x == rigidbody.transform.position.x
                     && Rock[r].GetComponent<Rigidbody2D>().transform.position.y + 1 == rigidbody.transform.position.y)
                  {
                        directionConnectedToRock = 4;
                        return directionConnectedToRock; //down
                  }
            }
            return directionConnectedToRock;
      }

      public int getFutureSnowPositionAroundRock(int directionMoveableOfSnow)
      {
            int directionConnectedToRock = 0;
            for (int r = 0; r < Rock.Length; r++)
            {

                  if (directionMoveableOfSnow == 1 && Rock[r].GetComponent<Rigidbody2D>().transform.position.y == rigidbody.transform.position.y
                      && Rock[r].GetComponent<Rigidbody2D>().transform.position.x - 1 == rigidbody.transform.position.x)
                  {
                        directionConnectedToRock = 1; // Left
                        return directionConnectedToRock;
                  }
                  else if (directionMoveableOfSnow == 2 && Rock[r].GetComponent<Rigidbody2D>().transform.position.y == rigidbody.transform.position.y
                      && Rock[r].GetComponent<Rigidbody2D>().transform.position.x + 1 == rigidbody.transform.position.x)
                  {
                        directionConnectedToRock = 2; // Right
                        return directionConnectedToRock;
                  }
                  else if (directionMoveableOfSnow == 4 && Rock[r].GetComponent<Rigidbody2D>().transform.position.x == rigidbody.transform.position.x
                      && Rock[r].GetComponent<Rigidbody2D>().transform.position.y - 1 == rigidbody.transform.position.y)
                  {
                        directionConnectedToRock = 4; //up
                        return directionConnectedToRock;
                  }
                  else if (directionMoveableOfSnow == 3 && Rock[r].GetComponent<Rigidbody2D>().transform.position.x == rigidbody.transform.position.x
                     && Rock[r].GetComponent<Rigidbody2D>().transform.position.y + 1 == rigidbody.transform.position.y)
                  {
                        directionConnectedToRock = 3;
                        return directionConnectedToRock; //down
                  }
            }
            return directionConnectedToRock;
      }

      public List<GameObject> findConnectedSnowBlocks(List<GameObject> snowBlocks, GameObject snowBlock)
      {
            for (int i = 0; i < Snow.Length; i++)
            {
                  GameObject adjacentBlock = Snow[i].gameObject;
                  if (snowBlock != adjacentBlock && isConnected(snowBlock, adjacentBlock) && adjacentBlock.GetComponent<BoxCollider2D>().isTrigger == true)
                  {
                        if (snowBlocks.IndexOf(adjacentBlock) == -1)
                        {
                              snowBlocks.Add(adjacentBlock);
                              //print("added " + adjacentBlock.gameObject.name);
                              findConnectedSnowBlocks(snowBlocks, adjacentBlock);
                        }
                  }
            }
            return snowBlocks;
      }

      //These statment works I just have to only use one at a time
      private Boolean isConnected(GameObject alpha, GameObject beta)
      {
            if (moveable == true)
            {
                  //print("A=" + alpha + ",B=" + beta);
                  if (alpha.transform.position.x - 2 == beta.GetComponent<Rigidbody2D>().transform.position.x &&
                      alpha.transform.position.y == beta.GetComponent<Rigidbody2D>().transform.position.y)
                  {
                        if(directionMovable == 1 || directionMovable == 2)
                        {
                              return true;
                        }
                  }
                  else if (alpha.transform.position.x + 2 == beta.GetComponent<Rigidbody2D>().transform.position.x &&
                      alpha.transform.position.y == beta.GetComponent<Rigidbody2D>().transform.position.y)
                  {
                        if(directionMovable == 1 || directionMovable == 2)
                        {
                              return true;
                        }
                  }
                  else if (alpha.transform.position.x == beta.GetComponent<Rigidbody2D>().transform.position.x &&
                      alpha.transform.position.y + 2 == beta.GetComponent<Rigidbody2D>().transform.position.y)
                  {
                        if(directionMovable == 3 || directionMovable == 4)
                        {
                              return true;
                        }
                  }
                  else if (alpha.transform.position.x == beta.GetComponent<Rigidbody2D>().transform.position.x &&
                      alpha.transform.position.y - 2 == beta.GetComponent<Rigidbody2D>().transform.position.y)
                  {
                        if(directionMovable == 3 || directionMovable == 4)
                        {
                              return true;
                        }
                  }
            }
            return false;
      }

      public Vector3 createMovementVector(int dir)
      {
            Vector3 m = new Vector3(0, 0);
            if (dir == 1)
            {
                  m = new Vector3(1 * movementSpeed, 0);
            }
            else if (dir == 2)
            {
                  m = new Vector3(-1 * movementSpeed, 0);
            }
            else if (dir == 3)
            {
                  m = new Vector3(0, -1 * movementSpeed);
            }
            else if (dir == 4)
            {
                  m = new Vector3(0, 1 * movementSpeed);
            }
            return m;
      }

      public void checkIfStatic(GameObject snowBlock)
      {
            List<GameObject> futureSnowBlocks = new List<GameObject>();

            snowBlock.GetComponent<SnowMovement>().checkPlayerPositionStatus();
            snowBlock.GetComponent<SnowMovement>().isStatic = false;

            for (int r = 0; r < Rock.Length; r++)
            {
                  List<GameObject> rockConnected = new List<GameObject>();
                  Rock[r].GetComponent<RockBehavior>().findConnectedSnowBlocksToRock(rockConnected, Rock[r].gameObject, snowBlock.GetComponent<SnowMovement>().directionMovable);

                  for (int rc = 0; rc < rockConnected.ToArray().Length; rc++)
                  {
                        if (rockConnected[rc].gameObject == snowBlock)
                        {
                              futureSnowBlocks.Add(snowBlock);
                        }
                  }
            }
            for (int f = 0; f < futureSnowBlocks.ToArray().Length; f++)
            {
                  futureSnowBlocks[f].GetComponent<SnowMovement>().isStatic = true;
                  player.GetComponent<PlayerController>().checkIfPlayerCanMove(futureSnowBlocks[f]);
            }
      }

      public void moveSnowBlocks(GameObject Snowblock)
      {
            if (moveable == true)
            {
                  List<GameObject> snowBlocksConnectedToRock = new List<GameObject>();
                  List<GameObject> snowBlocks = new List<GameObject>();


                  snowBlocks.Add(Snowblock);
                  findConnectedSnowBlocks(snowBlocks, Snowblock);

                  
                  //This function will add the first snow block connected to the rock
                  for (int i = 0; i < snowBlocks.ToArray().Length; i++)
                  {
                        if (snowBlocks[i].GetComponent<SnowMovement>().getSnowPositionAroundRock(Snowblock.GetComponent<SnowMovement>().directionMovable) == 1)
                        {
                              snowBlocksConnectedToRock.Add(snowBlocks[i]);
                        }
                        else if (snowBlocks[i].GetComponent<SnowMovement>().getSnowPositionAroundRock(Snowblock.GetComponent<SnowMovement>().directionMovable) == 2)
                        {
                              snowBlocksConnectedToRock.Add(snowBlocks[i]);
                        }
                        else if (snowBlocks[i].GetComponent<SnowMovement>().getSnowPositionAroundRock(Snowblock.GetComponent<SnowMovement>().directionMovable) == 3)
                        {
                              snowBlocksConnectedToRock.Add(snowBlocks[i]);
                        }
                        else if (snowBlocks[i].GetComponent<SnowMovement>().getSnowPositionAroundRock(Snowblock.GetComponent<SnowMovement>().directionMovable) == 4)
                        {
                              snowBlocksConnectedToRock.Add(snowBlocks[i]);
                        }
                  }
                  if (snowBlocksConnectedToRock.ToArray().Length > 0)
                  {
                        for (int r = 0; r < Rock.Length; r++)
                        {
                              Rock[r].GetComponent<RockBehavior>().findConnectedSnowBlocksToRock(snowBlocksConnectedToRock, Rock[r].gameObject, Snowblock.GetComponent<SnowMovement>().directionMovable);
                        }
                  }

                  for (int sr = 0; sr < snowBlocksConnectedToRock.ToArray().Length; sr++)
                  {
                        snowBlocks.Remove(snowBlocksConnectedToRock[sr].gameObject);
                  }

                  Vector3 movement = createMovementVector(Snowblock.GetComponent<SnowMovement>().directionMovable);


                  if (Snowblock.GetComponent<SnowMovement>().directionMovable == 1
                      || Snowblock.GetComponent<SnowMovement>().directionMovable == 2
                      || Snowblock.GetComponent<SnowMovement>().directionMovable == 3 ||
                      Snowblock.GetComponent<SnowMovement>().directionMovable == 4)
                  {
                        for (int c = 0; c < snowBlocks.ToArray().Length; c++)
                        {
                              snowBlocks[c].GetComponent<SnowMovement>().calculateFuturePosition(movement);
                        }
                        StartCoroutine(movementGo(snowBlocks, createMovementVector(Snowblock.GetComponent<SnowMovement>().directionMovable)));
                  }
            }
      }


      private bool seeIfSnowCanMoveMoved(GameObject objectCheckingAgainst, GameObject snow, int direction)
      {
            if (direction == 1)
            {
                  if (snow.transform.position.x == objectCheckingAgainst.transform.position.x - 2 && snow.transform.position.y == objectCheckingAgainst.transform.position.y)
                  {
                        return false;
                  }
            }
            else if (direction == 2)
            {
                  if (snow.transform.position.x == objectCheckingAgainst.transform.position.x + 2 && snow.transform.position.y == objectCheckingAgainst.transform.position.y)
                  {
                        return false;
                  }
            }
            else if (direction == 3)
            {
                  if (snow.transform.position.x == objectCheckingAgainst.transform.position.x && snow.transform.position.y == objectCheckingAgainst.transform.position.y + 2)
                  {
                        return false;
                  }
            }
            else if (direction == 4)
            {
                  if (snow.transform.position.x == objectCheckingAgainst.transform.position.x && snow.transform.position.y == objectCheckingAgainst.transform.position.y - 2)
                  {
                        return false;
                  }
            }

            return true;
      }

      public IEnumerator movementGo(List<GameObject> snowBlock, Vector3 movement)
      {
            for (int i = 0; i < snowBlock.ToArray().Length; i++)
            {
                  if (snowBlock[i] != player)
                  {
                        snowBlock[i].GetComponent<SnowMovement>().directionVector = movement;
                        snowBlock[i].GetComponent<SnowMovement>().partOfGroup = true;
                        snowBlock[i].GetComponent<SnowMovement>().isMoving = true;
                  }
                  else if (snowBlock[i] == player)
                  {
                        //player.GetComponent<PlayerMovement>().CanMove = false;
                  }
            }
            yield return new WaitForSeconds(0.09f);
            for (int i = 0; i < snowBlock.ToArray().Length; i++)
            {
                  if (snowBlock[i] != player)
                  {
                        snowBlock[i].GetComponent<SnowMovement>().directionVector = new Vector3(0, 0, 0);
                        snowBlock[i].GetComponent<SnowMovement>().partOfGroup = false;
                        snowBlock[i].GetComponent<SnowMovement>().isMoving = false;
                  }
                  else if (snowBlock[i] == player)
                  {
                        //player.GetComponent<PlayerMovement>().CanMove = true;
                  }
            }
            for (int s = 0; s < snowBlock.ToArray().Length; s++)
            {
                  if (snowBlock[s] != player)
                  {
                        snowBlock[s].transform.position = snowBlock[s].GetComponent<SnowMovement>().snowsEndingPosition;
                  }
            }
            snowConnectedDown();
            isMoving = false;
      }

      public void snowConnectedDown()
      {
            int count = 0;
            for (int i = 0; i < Snow.Length; i++)
            {
                  if (Snow[i].gameObject != this.gameObject)
                  {
                        if (transform.position + new Vector3(0, -1) != Snow[i].transform.position)
                        {
                              count += 1;
                        }
                  }
            }
      }

      public bool hasSnowBeside(int direction)
      {
            for (int i = 0; i < Snow.Length; i++)
            {
                  if (direction == 1)
                  {
                        if (transform.position.x == Snow[i].transform.position.x + 2 && transform.position.y == Snow[i].transform.position.y)
                        {
                              return true;
                        }
                  }
                  else if (direction == 2)
                  {
                        if (transform.position.x == Snow[i].transform.position.x - 2 && transform.position.y == Snow[i].transform.position.y)
                        {
                              return true;
                        }
                  }
                  else if (direction == 3)
                  {
                        if (transform.position.y == Snow[i].transform.position.y - 2 && transform.position.x == Snow[i].transform.position.x)
                        {
                              return true;
                        }
                  }
                  else if (direction == 4)
                  {
                        if (transform.position.y == Snow[i].transform.position.y + 2 && transform.position.x == Snow[i].transform.position.x)
                        {
                              return true;
                        }
                  }
            }
            return false;
      }




      public void calculateFuturePosition(Vector3 movementVector)
      {
            if (movementVector.x > 0)
            {
                  //We are moving right
                  snowsEndingPosition = new Vector3(transform.position.x + 1, transform.position.y);

            }
            else if (movementVector.x < 0)
            {
                  //We are moving left
                  snowsEndingPosition = new Vector3(transform.position.x - 1, transform.position.y);
            }
            else if (movementVector.y > 0)
            {
                  //We are moving up
                  snowsEndingPosition = new Vector3(transform.position.x, transform.position.y + 1);
            }
            else if (movementVector.y < 0)
            {
                  //We are miiving down
                  snowsEndingPosition = new Vector3(transform.position.x, transform.position.y - 1);
            }
      }
}