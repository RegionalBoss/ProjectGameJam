using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using TMPro;

public class BackToMenu : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    gameObject.GetComponent<Button>().onClick.AddListener(BackToMenuLoad);
  }

  // Update is called once per frame
  void BackToMenuLoad()
  {
    SceneManager.LoadScene(1);
  }
}
