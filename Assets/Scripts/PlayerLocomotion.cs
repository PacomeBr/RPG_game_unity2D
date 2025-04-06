using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerMovements playerMovements;
    public BreakingObjectSystem BreakingObjectSystem;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovements = GetComponent<PlayerMovements>();
    }

    
    void Update()
    {
        //Debug.Log(animator.GetInteger("State"));    


        if (playerMovements.isWalkling)
        {
            animator.SetInteger("State", 1);
        }
        else if (playerMovements.isRunning)
        {
            animator.SetInteger("State", 2);
        }
        else
        {
            animator.SetInteger("State", 0);
        }
    }
}
