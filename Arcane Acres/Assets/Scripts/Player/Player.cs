using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // this should go on the player object you place in each scene
    /* whenever a sceen changes, this needs to get and set the data in this script on the player
    object prefab that stays in that scene */
    [SerializeField] private PlayerData playerData;

    private GameObject playerGender;

    public Transform rootBone;

    private void Start()
    {
        playerData = GameManager.instance.playerData;
    }
    // getters and setters from player data 


}
