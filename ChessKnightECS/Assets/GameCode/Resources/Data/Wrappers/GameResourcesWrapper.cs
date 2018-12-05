using System;
using Fwt.Core;
using Unity.Entities;
using UnityEngine;

namespace Ck.Resources
{
  public class GameResourcesWrapper: SharedComponentDataWrapper<GameResources> 
  {
    [Space]
    public MatchResourcesWrapper[] Matches;

    private void OnValidate()
    {
      Init();
    }

    public void Init()
    {
      var matchesResources = new MatchResources[Matches.Length];
      for (int i = 0; i < matchesResources.Length; i++)
      {
        var matchResource = matchesResources[i];
        var matchWrapper = Matches[i];
        if (matchWrapper != null)
        {
          matchResource = matchWrapper.Value;
          matchResource.Name = matchWrapper.name;
        }

        matchResource.Id = i;
        matchesResources[i] = matchResource;
      }
      var value = Value;
      value.Matches = matchesResources;
      Value = value;
    }
  }
}