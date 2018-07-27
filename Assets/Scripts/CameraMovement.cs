using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour//IPauseRestart
{


    [SerializeField]
    private Transform playerTrans;

    [SerializeField]
    private Vector3 OffsetFromPlayer;

    private Vector3 nextPosition;

    private Transform currentTransform;


    private void Start()
    {
        if (currentTransform == null)
            currentTransform = GetComponent<Transform>();

        nextPosition = new Vector3(0, 0, -10);
    }

    private void LateUpdate()
    {
        nextPosition.y = playerTrans.position.y + OffsetFromPlayer.y; 
        currentTransform.position = nextPosition; 
    }

    /*
    private float yspeed;


    private Vector2 velocityVector; 

    private Rigidbody2D _rigidbody; 
	// Use this for initialization
	void Start ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        LevelManagerScript.Instance.LoosingEvent += StopMoving;
        velocityVector = new Vector2(0,0);
        


    }


    private void FixedUpdate()
    {
        if (!LevelManagerScript.Instance.isPaused)
            UpdateSpeed(yspeed); 
    }
    public void StopMoving()
    {
        UpdateSpeed(LevelManagerScript.Instance.LoosingSpeed);
    }

    public void UpdateSpeed(float newSpeed)
    {
        yspeed = newSpeed;
        velocityVector.y = yspeed;
        _rigidbody.velocity = velocityVector ;

    }

    public void Pause()
    {

        _rigidbody.velocity = Vector2.zero;

    }

    public void Restart()
    {
        UpdateSpeed(yspeed);
    }
    public void ResetPosition(Vector3 newPosition)
    {
        Pause();
        yspeed = LevelManagerScript.Instance.GameSpeed;
        transform.position = newPosition;
    }*/



}
