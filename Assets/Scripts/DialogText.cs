using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using TMPro;


[Serializable]
public class DialogText
{
  public string text;
  public AudioClip clip = null;
  public float delay;
  public DialogText(string text, float delay)
  {
    this.text = text;
    this.delay = delay;
  }
  public DialogText(string text, AudioClip clip, float delay)
  {
    this.text = text;
    this.clip = clip;
    this.delay = delay;
  }
}