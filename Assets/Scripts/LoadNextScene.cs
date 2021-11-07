using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using TMPro;

public class LoadNextScene : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    gameObject.GetComponent<Button>().onClick.AddListener(LoadNext);

  }

  // Update is called once per frame
  void LoadNext()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
}
