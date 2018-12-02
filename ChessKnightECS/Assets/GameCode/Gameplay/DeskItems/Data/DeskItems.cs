using System;
using Unity.Entities;

namespace Ck.Gameplay
{
  public static class DeskItems
  {

    [Serializable]
    public struct Background: IComponentData
    {
    } 

    [Serializable]
    public struct Bomb: IComponentData
    {
    } 

    [Serializable]
    public struct Figure: IComponentData
    {
    } 

    [Serializable]
    public struct Goal: IComponentData
    {
    } 

    [Serializable]
    public struct Lock: IComponentData
    {
    } 

    [Serializable]
    public struct MoveTarget: IComponentData
    {
    }

    [Serializable]
    public struct PlayerUnit: IComponentData
    {
    }

    [Serializable]
    public struct Highlight: IComponentData
    {

    }

  }

}