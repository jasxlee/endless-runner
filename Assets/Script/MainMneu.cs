﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMneu : MonoBehaviour
{
    // Start is called before the first frame update
  
  public void playgame()
  {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
  }
  public void QuitGame()
  {
      Application.Quit();
  }
}
