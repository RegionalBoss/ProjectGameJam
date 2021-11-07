using UnityEngine;

namespace ProjectGameJam
{

  public class MoveAndResize : MonoBehaviour
  {
    void Start()
    {
      GameObject parent = gameObject.transform.parent.gameObject;
      Transform[] ts = parent.GetComponentsInChildren<Transform>();
      Debug.Log(ts.Length);
      Transform lastChild = ts[ts.Length - 1];
      Debug.Log("last child: " + lastChild.ToString());
      gameObject.transform.localPosition = new Vector3(0, 0.5f, -2);
      print(gameObject.transform.lossyScale.ToString());
      gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 1);
    }
  }
}