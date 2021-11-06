using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using TMPro;

public class ShowDialog : MonoBehaviour
{
  public TextMeshProUGUI textMesh;
  // Start is called before the first frame update

  private bool animate = true;

  public void Show(string text, float time = 2)
  {
    gameObject.SetActive(true);
    animate = true;
    gameObject.GetComponent<Animator>().SetBool("Show", true);

    PopulateText(text);
    WaitAndClose(time);
  }

  IEnumerator PopulateText(string text)
  {
    foreach (string character in text.Split())
    {
      yield return new WaitForSeconds(0.1f);
      textMesh.text += character;
    }
  }

  IEnumerator WaitAndClose(float time)
  {
    //yield on a new YieldInstruction that waits for 5 seconds.
    yield return new WaitForSeconds(time);
    gameObject.GetComponent<Animator>().SetBool("Show", false);
  }
}
