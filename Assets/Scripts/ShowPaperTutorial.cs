using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using TMPro;


public class ShowPaperTutorial : MonoBehaviour
{

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Debug.Log("Mouse Clicked");

      Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
      RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

      if (hit.transform != null)
      {
        Animator animator = hit.transform.gameObject.GetComponent<Animator>();
        bool isOpen = animator.GetBool("Show");
        animator.SetBool("Show", !isOpen);
      }

      // if (Physics.Raycast(ray, out hit, 100.0f))
      // {
      //   print(hit.transform.gameObject.name);
      //   if (hit.transform != null)
      //   {
      //     hit.transform.gameObject.GetComponent<Animator>().SetBool("Show", true);
      //   }
      // }
    }
  }
}
