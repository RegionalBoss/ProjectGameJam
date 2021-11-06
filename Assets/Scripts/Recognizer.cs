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

  public class Recognizer : MonoBehaviour
  {

    public Transform gestureOnScreenPrefab;

    private List<Gesture> runesSet = new List<Gesture>();

    private List<Point> points = new List<Point>();
    private int strokeId = -1;

    private Vector3 virtualKeyPosition = Vector2.zero;
    private Rect drawArea;

    private RuntimePlatform platform;
    private int vertexCount = 0;

    private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
    private LineRenderer currentGestureLineRenderer;

    public GameObject final;
    public TextMeshProUGUI finalText;
    public Button finalRestart;

    public int usedGesturesCount = 0;
    public List<Gesture> usedGestures = new List<Gesture>();
    public bool canAddNew = false;

    public InputField SaveNewFiled;
    public Button SaveNewButton;

    public float GlobalScore;

    public GameObject runes;

    //GUI
    private string message;
    private string score;
    private bool recognized;

    private float time = 0;
    private bool timer = false;

    void Start()
    {

      platform = Application.platform;
      drawArea = new Rect(Screen.width * 0.3f, 0, Screen.width - Screen.width / 1.62f, Screen.height);

      //Load user custom gestures
      // string[] runesXml = Directory.GetFiles(Application.persistentDataPath, "*.xml");
      TextAsset[] runesXml = Resources.LoadAll<TextAsset>("Runes/");
      foreach (TextAsset runeXml in runesXml)
      {
        Gesture rune = GestureIO.ReadGestureFromXML(runeXml.text);
        float score = UnityEngine.Random.Range(-3.0f, 3.0f);
        rune.Score = (int)Math.Round(score);
        runesSet.Add(rune);
      }
      string scoreTable = "";
      foreach (Gesture rune in runesSet)
      {
        scoreTable += rune.Name + ": " + rune.Score + "\n";
      }
      Debug.Log(scoreTable);
      // usedGestures.text = "";
      if (!canAddNew) SaveNewFiled.gameObject.SetActive(false);
      if (SaveNewButton != null) SaveNewButton.onClick.AddListener(SaveNewGesture);
    }

    void FixedUpdate()
    {
      if (timer)
        time += Time.deltaTime;
    }

    void onResetClick()
    {
      recognized = true;
      ResetLines();
    }
    void ResetLines()
    {
      if (recognized)
      {

        recognized = false;
        strokeId = -1;

        points.Clear();

        foreach (LineRenderer lineRenderer in gestureLinesRenderer)
        {

          lineRenderer.positionCount = 0;
          Destroy(lineRenderer.gameObject);
        }

        gestureLinesRenderer.Clear();
      }

      ++strokeId;

      Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
      currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

      gestureLinesRenderer.Add(currentGestureLineRenderer);

      vertexCount = 0;
    }

    void SaveNewGesture()
    {
      string newGestureName = SaveNewFiled.text;
      // GUI.TextField(new Rect(Screen.width - 500, 150, 200, 30), newGestureName, textFieldStyle);
      Debug.Log(Application.persistentDataPath);
      string fileName = String.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, newGestureName, DateTime.Now.ToFileTime());

#if !UNITY_WEBPLAYER
      GestureIO.WriteGesture(points.ToArray(), newGestureName, fileName);
#endif

      runesSet.Add(new Gesture(points.ToArray(), newGestureName));

      SaveNewFiled.text = "";
    }
    void Update()
    {
      if (!canAddNew && time > 2 && timer)
      {
        timer = false;
        OnResult();
      }

      if (canAddNew)
        SaveNewFiled.gameObject.SetActive(true);
      else SaveNewFiled.gameObject.SetActive(false);

      if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
      {
        if (Input.touchCount > 0)
        {
          virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
        }
      }
      else
      {
        if (Input.GetMouseButton(0))
        {
          virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        }
      }

      if (drawArea.Contains(virtualKeyPosition))
      {

        if (Input.GetMouseButtonDown(0))
        {
          if (!canAddNew)
            timer = true;
          this.ResetLines();
        }

        if (Input.GetMouseButton(0))
        {
          time = 0;
          points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

          currentGestureLineRenderer.positionCount = ++vertexCount;
          currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
        }
      }
    }

    void restartScene()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnResult()
    {
      recognized = true;
      runes.GetComponent<RunePlaceholder>().AddLines(gestureLinesRenderer);
      usedGesturesCount++;
      Gesture candidate = new Gesture(points.ToArray());
      Result gestureResult = PointCloudRecognizer.Classify(candidate, runesSet.ToArray());

      message = gestureResult.GestureClass + " " + gestureResult.Score;
      if (gestureResult.Score > 0.85f)
      {
        // usedGestures.text += gestureResult.GestureClass + "\n";
        Debug.Log("rune score: " + gestureResult.Points);
        GlobalScore += gestureResult.Points;
        score = GlobalScore + " - " + gestureResult.Points;
        Gesture used = null;
        bool found = false;
        foreach (Gesture rune in runesSet)
        {
          if (rune.Name == gestureResult.GestureClass && !found)
          {
            used = rune;
            Debug.Log("Change rune score: " + rune.Name + ", prev score: " + rune.Score);
            int usedRuneCount = 0;
            foreach (Gesture usedRune in usedGestures)
            {
              if (usedRune.Name == gestureResult.GestureClass) usedRuneCount += 1;
            }
            int newScore = (int)Math.Round(gestureResult.Points - usedRuneCount);
            rune.Score -= newScore;
            Debug.Log("Changed rune score: " + rune.Name + ", new score: " + rune.Score);
            found = true;
          }
        }
        if (used != null)
          usedGestures.Add(used);
      }
      if (usedGesturesCount == 10)
      {
        recognized = false;
        timer = false;
        ResetLines();
        drawArea = new Rect();
        if (final != null && finalText != null)
        {
          foreach (Gesture rune in runesSet)
            finalText.text += rune.Name;
          final.SetActive(true);
        }
        finalRestart.onClick.AddListener(restartScene);
        return;
      }
      ResetLines();
      // usedSet.Add(gestureResult.GestureClass);
    }

    void OnGUI()
    {
      GUIStyle style = new GUIStyle();
      GUI.Box(drawArea, "");

      GUIStyle messageStyle = new GUIStyle();
      messageStyle.normal.textColor = Color.black;
      messageStyle.fontSize = 64;

      GUI.Label(new Rect(Screen.width - 500, 40, 500, 50), message, messageStyle);
      GUI.Label(new Rect(Screen.width - 500, 100, 500, 50), score, messageStyle);

    }
  }
}
