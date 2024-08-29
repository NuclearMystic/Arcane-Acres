using UnityEngine;

public class FootstepHandler : MonoBehaviour
{
    public AudioSource footstepSFX;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayFootstepSFX()
    {
        if(animator.GetFloat("Speed") > 0.5f)
        {
            footstepSFX.Play();
        }
    }
}
