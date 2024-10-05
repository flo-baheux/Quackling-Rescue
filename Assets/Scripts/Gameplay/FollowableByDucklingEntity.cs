using System.Collections.Generic;
using UnityEngine;

public class FollowableByDucklingEntity : MonoBehaviour
{
  private LinkedList<Duckling> ducklings = new LinkedList<Duckling>();
  public bool mustBeFollowedDuckly = false;

  public GameObject Follow(Duckling duckling)
  {
    Duckling? lastDucklingBeforeInsert = GetLast();
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
}
