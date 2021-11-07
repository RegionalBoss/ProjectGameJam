using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using TMPro;
public class ExitGame : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    gameObject.GetComponent<Button>().onClick.AddListener(QuitGame);
  }

  // Update is called once per frame
  void QuitGame()
  {
    Application.Quit();
  }
}
