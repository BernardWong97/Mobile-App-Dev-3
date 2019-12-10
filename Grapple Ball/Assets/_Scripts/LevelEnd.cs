using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public LevelLoader levelLoader;
    public GameObject eraseCanvas;
    public GameObject eraseOverlay;

    private void OnTriggerEnter2D(Collider2D colObj)
    {
        eraseCanvas.SetActive(false);
        eraseOverlay.SetActive(false);
        levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Tutorial Complete");
    }
}
