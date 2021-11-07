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
    public float transitionTime = 1.0f;
    public AudioClip audioClip;
    public ShowDialog dialog;
    public Image sceneLoader;
    public Button skipButton;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
      List<DialogText> text = new List<DialogText>();
      text.Add(new DialogText("Long-forgotten legends speak of magical scrolls, used to seal away beings we couldn’t understand or even comprehend.", 11));
      text.Add(new DialogText("I spent my whole life studying this mystery, and over time, darkness crept into my mind. After years of searching and nightmares, I managed to find one of these scrolls in an abandoned mansion.", 18));
      text.Add(new DialogText("But the voice that guided me to it keeps calling me still, pleading for help, and the visions keep getting worse. ", 15));
      text.Add(new DialogText("I tried getting rid of the scroll, only to find it back in my pocket. It seems I am tethered to it… for better or worse.", 15));
      Debug.Log(text + " " + audioClip.ToString());
      dialog.Show(text, audioClip, 5, 10);
      skipButton.onClick.AddListener(SkipClick);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      time += Time.deltaTime;
    }
    void SkipClick()
    {
      StartCoroutine(LoadNext());
    }
    void Update()
    {
      if (time > 60)
        StartCoroutine(LoadNext());
    }

    IEnumerator LoadNext()
    {
      sceneLoader.GetComponent<Animator>().SetTrigger("Show");
      yield return new WaitForSecondsRealtime(transitionTime);
      SceneManager.LoadScene(1);
    }
  }

}