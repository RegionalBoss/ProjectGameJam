using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;

using PDollarGestureRecognizer;

namespace ProjectGameJam
{

  public enum PaperState
  {
    Demon,
    Angel
  }

  public class RunePlaceholder : MonoBehaviour
  {
    public Material bloodMaterial;
    public SpriteRenderer overlay;

    public List<Sprite> demonOverlays = new List<Sprite>();
    public List<Sprite> angelOverlays = new List<Sprite>();

    private PaperState state;

    public int runesCount = 8;
    public RuntimeAnimatorController animator;
    public List<Transform> runesPositions = new List<Transform>();

    public void SetState(PaperState newState)
    {
      this.state = newState;
    }

    public void AddLines(List<LineRenderer> Lines)
    {
      Transform runePlaceholder = null;
      Debug.Log("Find position: " + runesPositions.Count);
      foreach (Transform pos in runesPositions)
      {
        if (runePlaceholder == null && !pos.gameObject.GetComponent<RunePosition>().isUsed)
        {
          Debug.Log("Found position: " + pos.ToString());
          runePlaceholder = pos;
          pos.gameObject.GetComponent<RunePosition>().isUsed = true;
        }
      }
      if (runePlaceholder == null) return;
      foreach (LineRenderer line in Lines)
      {
        if (line.positionCount > 1)
        {
          GameObject lineRendererObject = line.gameObject;
          LineRenderer lineRenderer = line;
          Debug.Log(gameObject);
          float x = gameObject.transform.position.x;
          float y = gameObject.transform.position.y;

          Mesh mesh = new Mesh();
          mesh.name = "LineMesh";
          // lineRenderer.endWidth = 0.20f;
          // lineRenderer.startWidth = 0.11f;
          lineRenderer.BakeMesh(mesh);
          // lineRenderer.SetPositions(newPos);

          GameObject obj = new GameObject("Line");
          obj.AddComponent<MeshRenderer>();
          obj.AddComponent<MeshFilter>();
          obj.AddComponent<MoveAndResize>();
          obj.AddComponent<Animator>();
          obj.GetComponent<Animator>().runtimeAnimatorController = animator;
          obj.GetComponent<MeshFilter>().mesh = mesh;
          obj.GetComponent<MeshRenderer>().material = bloodMaterial;

          GameObject GO = Instantiate(obj);
          Destroy(obj);
          GO.transform.parent = runePlaceholder.transform;
        }
      }
    }

    void Update()
    {

    }
  }
}