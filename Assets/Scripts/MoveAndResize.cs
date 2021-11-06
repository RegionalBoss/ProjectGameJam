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
      Debug.Log(lastChild.ToString());
      gameObject.transform.localPosition = new Vector3(0, 0, 0);
      gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 1);
    }
  }
}