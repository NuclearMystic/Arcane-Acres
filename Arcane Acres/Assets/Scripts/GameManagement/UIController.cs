using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject playerMenu;

    private Animator uiAnimator;

    private void Start()
    {
        uiAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("PlayerMenu"))
        {
            TogglePlayerMenu();
            GameManager.instance.ToggleCursorLock();
        }
    }

    private void TogglePlayerMenu()
    {
        uiAnimator.SetTrigger("MenuToggle");
        //if(playerMenu.activeSelf)
        //{
        //    playerMenu.SetActive(false);
        //    Time.timeScale = 1f;
        //}
        //else
        //{
        //    playerMenu.SetActive(true);
        //    Time.timeScale = 0f;
        //}
    }


}
