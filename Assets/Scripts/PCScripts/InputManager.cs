using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private CubeMechanism player1;
    [SerializeField]
    private CubeMechanism player2;

    private bool isAcessInUse; 
	// Use this for initialization
	void Start ()
    {
        isAcessInUse = false; 	
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetAxisRaw("Horizontal") == 0)
        {
            isAcessInUse = false; 
        }
            
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            if (!isAcessInUse)
            {
                isAcessInUse = true;
                RightClick(); 
            }
        }
        
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            if (!isAcessInUse)
            {
                isAcessInUse = true;
                LeftClick();
            }
        }
	}

    void LeftClick()
    {
        player1.Switch();
        player2.Switch();
    }

    void RightClick()
    {
        player2.Switch();
    }

}
