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

  public class ShowDialog : MonoBehaviour
  {
    public TextMeshProUGUI textMesh;
    // Start is called before the first frame update

    public bool noClose = false;
    private bool animate = true;
    private AudioSource audioData;
    void Start()
    {
      animate = true;
      audioData = gameObject.GetComponent<AudioSource>();
    }

    public void Show(List<string> text, AudioClip audioClip = null, float time = 5, float delay = 1)
    {
      textMesh.text = "";
      audioData.clip = null;
      gameObject.SetActive(true);
      animate = true;
      print("time: " + time);
      gameObject.GetComponent<Animator>().SetBool("Show", true);

      // textMesh.text = text;
      audioData.clip = audioClip;
      audioData.Play(0);
      StartCoroutine(PopulateText(text, delay));
      if (!noClose)
        StartCoroutine(WaitAndClose(time));
    }

    IEnumerator PopulateText(List<string> text, float delay)
    {
      foreach (string line in text)
      {
        textMesh.text = line;
        // foreach (string str in line.Split(' '))
        // {
        //   yield return new WaitForSecondsRealtime(0.25f);
        //   textMesh.text += (str + ' ');
        // }
        yield return new WaitForSecondsRealtime(delay);
      }
    }

    IEnumerator WaitAndClose(float time)
    {
      //yield on a new YieldInstruction that waits for 5 seconds.
      yield return new WaitForSecondsRealtime(time);
      gameObject.GetComponent<Animator>().SetBool("Show", false);
      Debug.Log("close!");
    }
  }

}