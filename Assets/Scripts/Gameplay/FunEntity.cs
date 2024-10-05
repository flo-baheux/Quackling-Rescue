using System;
using System.Collections.Generic;

public class FunEntity : FollowableByDucklingEntity
{
  [NonSerialized] public static readonly HashSet<FunEntity> entities = new HashSet<FunEntity>();

  void Awake()
  {
    entities.Add(this);
  }

  void OnDestroy()
  {
    entities.Remove(this);
  }
}
