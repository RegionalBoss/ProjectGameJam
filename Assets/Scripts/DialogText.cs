using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using TMPro;
namespace ProjectGameJam
{

  public class DialogText : MonoBehaviour
  {
    public string text;
    public float delay;
    public DialogText(string text, float delay)
    {
      this.text = text;
      this.delay = delay;
    }
  }
}