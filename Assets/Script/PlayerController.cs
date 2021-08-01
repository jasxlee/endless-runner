using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public Vector3 moveVector;
    public float speed = 15.0f;
    private float verticalVelocity = 0.0f;
    public float gravity = 50.0f;
    private float animationDuration = 3.0f;
    public bool isdead = false;
    public bool rewarding = false;
   public DeathMenu deathMenu;
   
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
       
    }

    // Update is called once per frame
    void Update()
    {
      if(GetComponent<score>().checkings == false && rewarding == true)
        {
        rewarding = false ;
        }
     // moveVector = Vector3.zero;
      if(transform.position.y < 0f)
        {
          //StartCoroutine(TimeDelay());
          death();          
        }
      if(controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }

        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        
        
        //Calucate X - left and right
       moveVector.x = Input.GetAxisRaw("Horizontal") * speed;

        // Y = up and down
        moveVector.y = verticalVelocity;

        //Z = forwared and backward
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);
        
        
        
    }
    
    public void SetSpeed(float modifier)
    {
        modifier = modifier + 8.0f;
        speed = 15.0f + modifier;
    }

   //it is begin called every time our player hits something 
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
      switch (hit.gameObject.tag)
      {
        
        case "Obstacle":
        death();
        break;
        
        case "Reward":
        reward();
        
        break;
          
      }
      
     
     
    }
    void reward()
    {
      rewarding = true;
      
    }
    
    
   void death()
   {
     isdead = true;
     gravity = -30f;
     speed = -30f;
     moveVector.y = gravity;
     moveVector.z = speed;
   }

  /* private IEnumerator TimeDelay()
   {
     yield return new WaitForSeconds(2f);
   }*/
  
}
