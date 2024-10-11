using System.Collections.Generic;
using UnityEngine;

public class FollowableByDucklingEntity : MonoBehaviour
{
  private LinkedList<Duckling> ducklings = new LinkedList<Duckling>();
  public bool mustBeFollowedDuckly = false;
  public int currentlyFollowingCount
  {
    get => ducklings.Count;
  }

  public GameObject Follow(Duckling duckling)
  {
    Duckling? lastDucklingBeforeInsert = ducklings.Last?.Value;
    ducklings.AddLast(duckling);
    if (mustBeFollowedDuckly)
      return lastDucklingBeforeInsert ? lastDucklingBeforeInsert.gameObject : gameObject;
    else
      return gameObject;
  }

  public void Unfollow(Duckling duckling)
  {
    LinkedListNode<Duckling> node = ducklings.Find(duckling);

    if (node.Next != null)
      node.Next.Value.target = node.Previous != null ? node.Previous.Value.gameObject : gameObject;

    ducklings.Remove(duckling);
  }

  public void DebugList()
  {
    LinkedListNode<Duckling> node = ducklings.First;
    int index = 0;
    string str = "";
    while (node != null)
    {
      str += $"[{index}] - {node.Value.gameObject.name}\n";
      node = node.Next;
      index++;
    }
    Debug.Log(str);
  }
}
