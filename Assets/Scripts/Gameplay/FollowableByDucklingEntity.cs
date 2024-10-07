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
    Duckling? lastDucklingBeforeInsert = GetLast();
    if (mustBeFollowedDuckly)
      Debug.Log($"{duckling.name}: should follow {lastDucklingBeforeInsert} (GetLast before insert)");
    ducklings.AddLast(duckling);
    if (mustBeFollowedDuckly)
      return lastDucklingBeforeInsert ? lastDucklingBeforeInsert.gameObject : gameObject;
    else
      return gameObject;
  }

  public void Unfollow(Duckling duckling)
  {
    LinkedListNode<Duckling> node = ducklings.Last;
    while (node != null && node.Value != duckling)
    {
      Duckling ducklingToRemove = node.Value;
      node = node.Previous;
      ducklings.Remove(ducklingToRemove);
    }
    ducklings.Remove(duckling);
  }

  private Duckling? GetLast()
  {
    return ducklings.Last?.Value;
  }

  public void DebugList()
  {
    LinkedListNode<Duckling> node = ducklings.First;
    int index = 0;
    Debug.Log($"[{index}] - {node.Value.gameObject.name}");
    while (node != ducklings.Last)
    {
      node = node.Next;
      index++;
      Debug.Log($"[{index}] - {node.Value.gameObject.name}");
    }
  }
}
