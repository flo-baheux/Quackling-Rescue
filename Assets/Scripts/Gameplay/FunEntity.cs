using System;
using System.Collections.Generic;
using UnityEngine;

public class FunEntity : FollowableByDucklingEntity
{
  [NonSerialized] public static readonly HashSet<FunEntity> entities = new HashSet<FunEntity>();
  public float attentionCatchingDistance = 10f;

  void Awake()
  {
    entities.Add(this);
  }

  void OnDestroy()
  {
    entities.Remove(this);
  }

  void OnDrawGizmos()
  {
    Gizmos.color = new Color(255, 0, 0, 0.3f);
    Gizmos.DrawSphere(transform.position, attentionCatchingDistance);
  }
}
