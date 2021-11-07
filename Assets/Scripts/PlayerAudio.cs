using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using TMPro;

using PDollarGestureRecognizer;

namespace ProjectGameJam
{
  public class PlayerAudio : MonoBehaviour
  {

    public GameObject dialogContainer;
    [SerializeField]
    public List<DialogText> good = new List<DialogText>();
    [SerializeField]
    public List<DialogText> neutral = new List<DialogText>();
    [SerializeField]
    public List<DialogText> bad = new List<DialogText>();

    private AudioSource audioSource;
    private ShowDialog dialog;

    void Start()
    {
      audioSource = gameObject.GetComponent<AudioSource>();
      dialog = dialogContainer.GetComponent<ShowDialog>();
    }

    public void PlayGood()
    {
      int _score = UnityEngine.Random.Range(0, good.Count);
      DialogText dialogText = good[_score];
      StartCoroutine(PlaySound(dialogText));
    }
    public void PlayBad()
    {
      Debug.Log("Play bad!");
      int _score = UnityEngine.Random.Range(0, bad.Count);
      DialogText dialogText = bad[_score];
      StartCoroutine(PlaySound(dialogText));
    }
    public void PlayNeutral()
    {
      int _score = UnityEngine.Random.Range(0, neutral.Count);
      DialogText dialogText = neutral[_score];
      StartCoroutine(PlaySound(dialogText));
    }

    IEnumerator PlaySound(DialogText dialogText)
    {
      List<DialogText> dialogList = new List<DialogText>();
      dialogList.Add(dialogText);
      yield return new WaitForSecondsRealtime(0.5f);
      dialog.Show(dialogList);
    }

    void ShowDialog(List<DialogText> text)
    {
      dialogContainer.GetComponent<ShowDialog>().Show(text);
    }
  }
}