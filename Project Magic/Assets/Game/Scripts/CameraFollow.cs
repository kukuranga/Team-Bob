using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Author: Benjamin Kerr
Date: 3/10/2021
Description: This script will follow the player around on a 2D axsis
*/
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject Camera;
    [SerializeField] private GameObject Player;

    [SerializeField] private Vector3 offset;

[Range(0.0f, 10.0f)]
    [SerializeField] private float Speed;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // LateUpdate is called once per frame after Update. This is important because since we are following a moving object we want to know its new position
    void LateUpdate()
    {
        Vector3 targetPos = Player.transform.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, Speed * Time.deltaTime);

        transform.position = smoothPos;
    }
}
