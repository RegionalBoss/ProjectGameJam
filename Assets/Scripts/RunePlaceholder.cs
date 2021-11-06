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

  public class RunePlaceholder : MonoBehaviour
  {
    public Material bloodMaterial;
    public int runesCount = 8;
    public List<Transform> runesPositions = new List<Transform>();

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
          lineRenderer.endWidth = 0.18f;
          lineRenderer.startWidth = 0.34f;
          lineRenderer.BakeMesh(mesh);
          // lineRenderer.SetPositions(newPos);

          GameObject obj = new GameObject("Line");
          obj.AddComponent<MeshRenderer>();
          obj.AddComponent<MeshFilter>();
          obj.AddComponent<MoveAndResize>();
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