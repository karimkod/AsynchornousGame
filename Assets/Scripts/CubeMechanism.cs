using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeMechanism : MonoBehaviour,IPauseRestart {

    private bool right;
    public float yspeed = 0;
    //public float xspeed;
    public Vector2 collisionParticlePosition;
    private Animator _animator;
    private Collider2D _collider;
    private ParticleSystem _particles;
    private SpriteRenderer _sprite; 
    private Rigidbody2D _rigidbody;
    private ParticleSystem _collisionParticles;
    private Transform _collisionParticlesTransform;

    private static bool alreadyCalled = false;
    public static bool AlreadyCalled { set { alreadyCalled = value; } get { return alreadyCalled; } }
    
    private bool previousDirection; // true means right and false means left

    private float xPosition; 
    public Transform currentTransform { set; get; }

    [SerializeField]
    private AudioSource loosingAudioEffect;

    private Vector2 velocityVector; 

	// Use this for initialization
	void Start ()
    {

        currentTransform = GetComponent<Transform>();
        xPosition = currentTransform.position.x;
        _animator = GetComponent<Animator>(); 
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _sprite = currentTransform.GetChild(0).GetComponent<SpriteRenderer>(); 
        _particles = currentTransform.GetChild(1).GetComponent<ParticleSystem>();
        _collisionParticlesTransform = currentTransform.GetChild(2);
        _collisionParticles = _collisionParticlesTransform.GetComponent<ParticleSystem>();

        LevelManagerScript.Instance.LoosingEvent += Lost;
        velocityVector = new Vector2();




    }

    // Update is called once per frame
    void Update ()
    {
       
        if (_animator != null)
        {
            _animator.SetFloat("Xposition", currentTransform.position.x); 
        }
		
	}

    private void FixedUpdate()
    {
        if (!LevelManagerScript.Instance.isPaused)
            UpdateSpeed(yspeed);

    }



    public void Switch()
    {
        right = !right;

        UpdateSpeed(yspeed);
    }

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            if (!alreadyCalled)
            {
                LevelManagerScript.Instance.Loosing(other.transform.parent.gameObject);
                alreadyCalled = true;
            }
            
        }
        yield return new WaitForEndOfFrame();
        alreadyCalled = false;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {

            if (previousDirection = GetVerticalSide(other.contacts[0].point))
            {
                _collisionParticlesTransform.localPosition = collisionParticlePosition;
            }else
            {
                _collisionParticlesTransform.localPosition = -collisionParticlePosition.x * Vector2.right +
                collisionParticlePosition.y * Vector2.up; 
            }
            _collisionParticles.Play();


        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        
        if (other.gameObject.tag == "Wall")
        {
            if(!_collisionParticles.isPlaying)
                _collisionParticles.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (right == previousDirection)
            return; 
        if (other.gameObject.tag == "Wall")
        {
            _collisionParticles.Stop(); 
        }
    }

    private bool GetVerticalSide(Vector3 touchPoint)
    {
        if(touchPoint.x > currentTransform.position.x)
        {
            return true; 
        }else if (touchPoint.x < currentTransform.position.x)
        {
            return false;
        }
        return false; 

    }

    public void Pause()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    public void Restart()
    {
        UpdateSpeed(yspeed);
    }

    public void UpdateSpeed(float newYSpeed)
    {
        yspeed = newYSpeed;
        velocityVector.x = (right ? 1 : -1) * (yspeed + 10);
        velocityVector.y = yspeed;
        _rigidbody.velocity = velocityVector; 
    }

   
    public void Lost()
    {
        UpdateSpeed(LevelManagerScript.Instance.LoosingSpeed); 
        _collider.enabled = false;
        _collisionParticles.gameObject.SetActive(false); 
        _sprite.enabled = false;
        _particles.Play();
        loosingAudioEffect.Play();
        //StartCoroutine(LevelManagerScript.Instance.ReloadLevel()); 
    }
        
    public void ResetPosition(Vector3 newPosition)
    {
        Pause();
        yspeed = LevelManagerScript.Instance.GameSpeed;
        currentTransform.position = newPosition + xPosition*Vector3.right;
        _collider.enabled = true;
        _collisionParticles.gameObject.SetActive(true);
        _sprite.enabled = true;
        _particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
