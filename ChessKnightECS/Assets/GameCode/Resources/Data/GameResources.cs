using System;
using Unity.Entities;

namespace Ck.Resources
{
  [Serializable]
  public struct GameResources: ISharedComponentData
  {
    public MatchResources[] Matches;
  }
}