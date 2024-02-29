using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLanding : StateMachineBehaviour
{
    public float forwardSpeed = 5f; // Speed of forward movement
    public float downSpeed = 1f;    // Speed of downward movement
    public float targetY = 0f;      // Y position at which to transition to landing state

    private Animator animator;
    private bool hasReachedTargetY = false;
    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Cache the Animator component
        this.animator = animator;
        // Reset flag
        hasReachedTargetY = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Move the character forward
        animator.transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // Move the character slightly downward
        animator.transform.Translate(Vector3.down * downSpeed * Time.deltaTime);

         // Get the Animator component from the dragon object
        GameObject dragonObject = GameObject.FindGameObjectWithTag("DragonComplete");
        Animator dragonAnimator = dragonObject.GetComponent<Animator>();
        
        // Check if character has reached target Y position
        if (!hasReachedTargetY && animator.transform.position.y <= targetY)
        {
            // Stop the animation
            //animator.speed = 0f;

            // Stop the movement
            forwardSpeed = 0f;
            downSpeed = 0f;

            hasReachedTargetY = true; // Set flag to prevent repeated triggers

            // Trigger the specified state in the Animator
            dragonAnimator.SetTrigger("Land");
        }
    }

    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     // Reset animation speed when exiting the state
    //     animator.speed = 1f;
    // }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DragonLanding : StateMachineBehaviour
// {
//     public float forwardSpeed = 5f; // Speed of forward movement
//     public float downSpeed = 1f;    // Speed of downward movement
//     public float targetY = 0f;      // Y position at which to transition to landing state

//     private Animator animator;
//     private bool hasReachedTargetY = false;

//     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     {
//         // Cache the Animator component
//         this.animator = animator;
//         // Reset flag
//         hasReachedTargetY = false;
//     }

//     // OnStateMove is called right after Animator.OnAnimatorMove()
//     override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     {
//         // Move the character forward
//         animator.transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

//         // Move the character slightly downward
//         animator.transform.Translate(Vector3.down * downSpeed * Time.deltaTime);

//         // Check if character has reached target Y position
//         if (!hasReachedTargetY && animator.transform.position.y <= targetY)
//         {
//             // Trigger landing animation
//             animator.SetTrigger("Landing");
//             hasReachedTargetY = true; // Set flag to prevent repeated triggers
//         }
//     }
// }

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DragonLanding : StateMachineBehaviour
// {
//     public float forwardSpeed = 5f; // Speed of forward movement
//     public float downSpeed = 1f;    // Speed of downward movement

//     // OnStateMove is called right after Animator.OnAnimatorMove()
//     override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     {
//         // Implement code that processes and affects root motion

//         // Move the character forward
//         animator.transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

//         // Move the character slightly downward
//         animator.transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
//     }
// }
