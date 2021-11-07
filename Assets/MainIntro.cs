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
  public class MainIntro : MonoBehaviour
  {
    public Button skip;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
      skip.onClick.AddListener(Close);
    }

    void FixedUpdate()
    {
      time += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
      if (time > 18) Close();
    }

    void Close()
    {

      gameObject.GetComponent<Animator>().SetBool("Close", true);
      gameObject.GetComponent<AudioSource>().Pause();
    }
  }
}