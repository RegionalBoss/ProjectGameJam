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
  [Serializable]

  public class ImageSet
  {
    public Sprite image;
    public float delay;

    public ImageSet(Sprite image, float delay)
    {
      this.image = image;
      this.delay = delay;
    }
  }
  public class IntroScript : MonoBehaviour
  {
    public TextMeshProUGUI textMesh;

    public float transitionTime = 1.0f;
    public AudioClip audioClip;
    public Image sceneLoader;
    public Image imagesPlace;
    public Button skipButton;
    [SerializeField]
    public List<ImageSet> imagesSet = new List<ImageSet>();
    private List<DialogText> dialogTexts = new List<DialogText>();
    private float time;
    // Start is called before the first frame update
    void Start()
    {
      skipButton.onClick.AddListener(SkipClick);
      dialogTexts.Add(new DialogText("Long-forgotten legends speak of magical scrolls, used to seal away beings we couldn’t understand or even comprehend.", 11));
      dialogTexts.Add(new DialogText("I spent my whole life studying this mystery, and over time, darkness crept into my mind. After years of searching and nightmares, I managed to find one of these scrolls in an abandoned mansion.", 18));
      dialogTexts.Add(new DialogText("But the voice that guided me to it keeps calling me still, pleading for help, and the visions keep getting worse. ", 8));
      dialogTexts.Add(new DialogText("I tried getting rid of the scroll, only to find it back in my pocket. It seems I am tethered to it… for better or worse.", 10));
      Debug.Log("text: " + dialogTexts);
      // dialog.GetComponent<ShowDialog>().Show(dialogTexts, null, 5, 10);
      StartCoroutine(ShowImages());
      StartCoroutine(ShowText());
    }

    IEnumerator ShowImages()
    {
      foreach (ImageSet sprite in imagesSet)
      {
        imagesPlace.gameObject.GetComponent<Animator>().SetTrigger("fade");
        imagesPlace.sprite = sprite.image;
        yield return new WaitForSecondsRealtime(sprite.delay);
      }
    }

    IEnumerator ShowText()
    {
      textMesh.text = "";

      foreach (DialogText dialogText in dialogTexts)
      {
        textMesh.gameObject.GetComponent<Animator>().SetTrigger("Fade");
        textMesh.text = dialogText.text;

        yield return new WaitForSecondsRealtime(dialogText.delay);
      }
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
      if (Input.GetKeyDown("space")) SkipClick();
      if (time > 50)
        StartCoroutine(LoadNext());
    }

    IEnumerator LoadNext()
    {
      sceneLoader.GetComponent<Animator>().SetTrigger("Start");
      yield return new WaitForSecondsRealtime(transitionTime);
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
  }

}