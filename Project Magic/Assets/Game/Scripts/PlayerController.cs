using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Benjamin Kerr
 * Date: 4/21/2020
 * Description: This class is responsible for controlling the charecter. It will handle movement and player location
 */
public class PlayerController : MonoBehaviour
{
    //Inventory information
    private Inventory inventory;
    [SerializeField]
    private UI_Inventory uiIinventory;
    public KeyCode OpenInventory;


    // Movement related variables 
    public float acceleration; // This value will determin how fast the player accelerates to full speed
      public float maxVerticalSpeed; // This value is the max vertical speed the player can reach
      public float maxHorizontalSpeed; // This value is the max horizontal speed the player can reach
      public float friction; // The value will be the ammount of friction 
      private Vector3 movementVector; // The vector the player will move in
      private Vector3 frictionVector; // The vector will control the speed at which the player will slow down
      public Rigidbody2D rigidbody; // 2D rigid body of the player game object
      public int facingDirection;



      //Setup key variables
      [System.NonSerialized] public KeyCode moveForward; //This key will move the player forward
      [System.NonSerialized] public KeyCode moveBackwards; // This key will move the player backwards
      [System.NonSerialized] public KeyCode moveRight; //This key will move the player to the right
      [System.NonSerialized] public KeyCode moveLeft; // This key will move the player left
      //Movement related variables ^



      //Health related variables
      [System.NonSerialized] public int healthSize; // varriable will be responisble for controlling the health of the player


      // Animation related variables
      public Animator animator;


    public void Awake()
    {
        inventory = new Inventory();
        uiIinventory.SetInventory(inventory);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            //Touching Item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.Despawn();
        }

    }

    // Start is called before the first frame update
    void Start()
      {
            //Defult Key values for now are WASD
            //Can be changed in the future with public so player can acsess them in a settings menu later on
            moveForward = KeyCode.W;
            moveBackwards = KeyCode.S;
            moveRight = KeyCode.D;
            moveLeft = KeyCode.A;
            frictionVector = new Vector3(friction, friction);

            healthSize = 0; 

            healthSize = 0;
            
      }

      // Update is called once per frame
      void Update()
      {
      }

      // Fixed Update called before everything else so move player here to account for physic objects
      private void FixedUpdate()
      {
            movePlayer(); // This method will determin what direction the player is moving in

            applyFriction(); // This method will slow the player down by applying friction
            transform.Translate(movementVector * Time.deltaTime); // The last step is to translate the player in the correct vector
            //animatePlayerMovement(); // Animate the players animation

      }

      private void ToggleInventory()
    {
        if(Input.GetKey(OpenInventory))
        {
            uiIinventory.gameObject.SetActive(true); 
        }
    }

      //This function will apply a friction vector to the movement vector in order to slow down the player
      private void applyFriction()
      {
            //print(movementVector.y);
            frictionVector.x = 0;
            frictionVector.y = 0;

            //Horizontal friction
            //Folling 'if' 'else' statment determin the correct direction the horizontal friction is applied in
            if (movementVector.x > friction)
            {
                  frictionVector.x = friction * -1;
            }
            else if (movementVector.x < friction * -1)
            {
                  frictionVector.x = friction;
            }
            else
            {
                  movementVector.x = 0; // If we are not moving horizontally then we don't want to move at all
            }

            //Vertical friction
            //Following 'if' 'else' statment determines the correct direction the vertical friction is applied in
            if (movementVector.y > friction)
            {
                  frictionVector.y = friction * -1;
            }
            else if (movementVector.y < friction * -1)
            {
                  frictionVector.y = friction;
            }
            else
            {
                  movementVector.y = 0; // If we are not moving vertically then we don't want to move at all
            }

            //print("(" + frictionVector.x + "," + frictionVector.y + ")");
            movementVector += frictionVector; //Finally we add the friction to the movement vector
      }

      //The move player function will be used to move the player
      private void movePlayer()
      {
            movementVector.x = (float)System.Math.Round(movementVector.x, 1); // Round the charicters x position to one decimal place
            movementVector.y = (float)System.Math.Round(movementVector.y, 1); // Round the charicters y position to one decimal place
            //print("(" + movementVector.x + "," + movementVector.y + ")");


            if (Input.GetKey(moveForward)) // If the player wants to move forward
            {
                  movementVector.y += acceleration; // Increase the movement vector's y component positivley
                  if (movementVector.y >= maxVerticalSpeed) // We need to check if we are over the max speed so we do not infiniatly accelerate
                  {
                        movementVector.y = maxVerticalSpeed; // Set the movement vector's y component to the max vertical speed;
                                                             // We have reached max speed vertically
                  }

            }

            if (Input.GetKey(moveBackwards)) // If the player wants to move forward
            {
                  movementVector.y -= acceleration; // Increase the movement vector's y component positivley
                  if (Mathf.Abs(movementVector.y) >= maxVerticalSpeed) // We need to check if we are over the max speed so we do not infiniatly accelerate
                  {
                        movementVector.y = maxVerticalSpeed * -1f; // Set the movement vector's y component to the max vertical speed;
                                                                   // We have reached max speed vertically
                  }
            }

            if (Input.GetKey(moveRight)) // If the player wants to move to the right
            {
                  movementVector.x += acceleration; // Increases the movement vector's x component positivly
                  if (movementVector.x >= maxHorizontalSpeed) // We need to check if we are over the max speed so we do not infiniatly accelerate
                  {
                        movementVector.x = maxHorizontalSpeed; // Set the movement vector's x component to the max horizontal speed
                                                               // We have reached max speed horizontally
                  }
            }

            if (Input.GetKey(moveLeft)) // If the player wants to move to the right
            {
                  movementVector.x -= acceleration; // Increases the movement vector's x component positivly
                  if (Mathf.Abs(movementVector.x) >= maxHorizontalSpeed) // We need to check if we are over the max speed so we do not infiniatly accelerate
                  {
                        movementVector.x = maxHorizontalSpeed * -1f; // Set the movement vector's x component to the max horizontal speed
                                                                     // We have reached max speed horizontally
                  }

            }


            transform.Translate(new Vector2(movementVector.x, movementVector.y) * Time.deltaTime);
      }

      // Animate the player based on the movement vector
      private void animatePlayerMovement()
      {
            if (movementVector.x != 0 || movementVector.y != 0)
            {
                  animator.SetBool("isMoving", true);
                  if (movementVector.y < 0)
                  {
                        animator.SetInteger("direction", 1);
                        if (movementVector.x > 0)
                        {
                              animator.SetInteger("direction", 2);
                        }
                        else if (movementVector.x < 0)
                        {
                              animator.SetInteger("direction", 4);
                        }
                  }
                  else if (movementVector.y > 0)
                  {
                        animator.SetInteger("direction", 3);
                        if (movementVector.x > 0)
                        {
                              animator.SetInteger("direction", 2);
                        }
                        else if (movementVector.x < 0)
                        {
                              animator.SetInteger("direction", 4);
                        }
                  }
                  else if (movementVector.y == 0)
                  {
                        if (movementVector.x > 0)
                        {
                              animator.SetInteger("direction", 2);
                        }
                        else if (movementVector.x < 0)
                        {
                              animator.SetInteger("direction", 4);
                        }
                  }
            }
            else if (movementVector.x == 0 || movementVector.y == 0)
            {
                  animator.SetBool("isMoving", false);
            }
      }

      //This method will lower the players health along with replacing the image of a heart 
      //to a damaged one to indicate a life is lost
      private void healthSystem()
      {
            
      }

      public void Damage(float dmg)
      {

      }

      public Vector3 getMovementVector()
      {
            return movementVector;
      }

}
