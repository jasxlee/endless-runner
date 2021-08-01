using UnityEngine;
using UnityEngine.SceneManagement;
using DigitalRubyShared;

using System.Collections;
using System.Collections.Generic;

public class MovePlayer : MonoBehaviour
{
    Rigidbody _rigidbody;

    [SerializeField]
    float speed2, MaxSpeed;
    float MovePower = 0.5f;
    public float gravity = 50.0f;
    public bool isdead = false;
    public bool rewarding = false;
    public bool CheckingReward = false;
    public DeathMenu deathMenu;
    public float speed = 25.0f;
    private float? sideSpeed, forwardSpeed;

    public PanGestureRecognizer PanGesture { get; private set; }
    // Start is called before the first frame update
    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        speed = 25.0f;
        MaxSpeed = 15;
    }

    void OnEnable()
    {
        PanGesture = new PanGestureRecognizer();
        PanGesture.StateUpdated += MovePan_StateUpdated;
        FingersScript.Instance.AddGesture(PanGesture);

    }

    private void OnDisable() {
        if (FingersScript.HasInstance) {
            FingersScript.Instance.RemoveGesture(PanGesture);
        }
    }

    // public void ExitGame() {
    //     Application.Quit();
    // }
    
    // public void RestartGame() {
    //     SceneManager.LoadScene(0);
    // }

    private void MovePan_StateUpdated(DigitalRubyShared.GestureRecognizer gesture) {
        if (gesture.State == GestureRecognizerState.Executing) {
            Vector3 velocity = _rigidbody.velocity;
            velocity.x += (gesture.VelocityX * Time.deltaTime * speed);
            //velocity.y += (gesture.VelocityY * Time.deltaTime * speed);
            velocity.z = 0;
            _rigidbody.velocity = velocity;
        }
    }


    public void Moved(Vector2 panAmount) {
        sideSpeed = panAmount.x * MaxSpeed;
        forwardSpeed = Mathf.Sign(panAmount.y) * Mathf.Pow(Mathf.Abs(panAmount.y), MovePower) * MaxSpeed;
    }


    void Update() {
        transform.position += new Vector3(0, 0, speed * Time.deltaTime);

        // calculate new velocity
        Vector3 velRight = Vector3.zero;
        Vector3 velForward = Vector3.zero;
        Vector3 velUp = new Vector3(0.0f, _rigidbody.velocity.y, 0.0f);

        if (sideSpeed != null) {
            velRight = _rigidbody.transform.right * sideSpeed.Value;
        }
        // if(GetComponent<score>().checkings == true && rewarding == true)
        // {
        // Debug.Log("restarting reward to false");
        // rewarding = false ;
        // }
     
        // if ( GetComponent<score>().checkings == rewarding)
        // {
        //     Debug.Log("Restarting reward to fasle");
        //     rewarding = false;
        // }
        if(transform.position.y < 0f)
        {
          //StartCoroutine(TimeDelay());
          death();          
        }
        Vector3 vel = velRight + velUp;
        _rigidbody.velocity = vel;
    }
    
//it is begin called every time our player hits something 
     void OnTriggerEnter (Collider collision)
    {
        switch(collision.gameObject.tag)
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
      Debug.Log("reward hit");
     
   
    }
   
    void death()
   {
     isdead = true;
     gravity = -30f;
     speed = -30f;
     
   }
   public void SetSpeed(float modifier)
    {
        modifier = modifier + 8.0f;
        speed = 15.0f + modifier;
    }

  
}
