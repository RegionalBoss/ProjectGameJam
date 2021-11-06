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

  public class IntroScript : MonoBehaviour
  {
    public AudioClip audioClip;
    public ShowDialog dialog;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
      List<DialogText> text = new List<DialogText>();
      text.Add(new DialogText("Long-forgotten legends speak of magical scrolls, used to seal away beings we couldn’t understand or even comprehend.", 12));
      text.Add(new DialogText("I spent my whole life studying this mystery, and over time, darkness crept into my mind. After years of searching and nightmares, I managed to find one of these scrolls in an abandoned mansion.", 15));
      text.Add(new DialogText("But the voice that guided me to it keeps calling me still, pleading for help, and the visions keep getting worse. ", 15));
      text.Add(new DialogText("I tried getting rid of the scroll, only to find it back in my pocket. It seems I am tethered to it… for better or worse.", 15));
      Debug.Log(text + " " + audioClip.ToString());
      dialog.Show(text, audioClip, 5, 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      time += Time.deltaTime;
    }
    void Update()
    {
      if (time > 60) SceneManager.LoadScene(1);
    }
  }

}