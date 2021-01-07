
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim = null;

    // AWAKE IS CALLED BEFORE ANYTHING ELSE
    //
    private void Awake() => anim = GetComponent<Animator>();       // -> GET A REFERENCE TO THE PLAYER'S ANIMATOR COMPONENT
    public void Run() => anim.SetInteger("State", 0);              // -> YUNG SetInteger("State", 0) ANDOON PO SYA SA ANIMATOR. 
                                                                   // -> INTERGER PARAMETER PO SYA, GAGAMITIN SA TRANSITION

    public void Jump() => anim.SetBool("Jump", true);              // -> YUNG SetBool("Jump", true) ANDOON PO SYA SA ANIMATOR. 
                                                                   // -> BOOLEAN PARAMETER PO SYA, GAGAMITIN SA TRANSITION
    public void ClearJump() => anim.SetBool("Jump", false);

    public void Fall() => anim.SetTrigger("Fall");                 // -> YUNG SetTrigger("Fall") ANDOON PO SYA SA ANIMATOR.
                                                                   // -> TRIGGER PO SYA, 
                                                                   // -> IBIG SABIHIN KAHIT ANONG STATE NG ANIMATION NYO, 
                                                                   //    PWEDE SYA AGAD MAGCHANGE ANIMATION
}
