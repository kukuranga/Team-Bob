﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScene : MonoBehaviour
{
    public string level = "FinalSceneTest";

    void OnCollisionEnter2D(Collision2D Colider)
    {
        if (Colider.gameObject.tag == "Player");
        SceneManager.LoadScene(level);
    }
}
