using System;
using Unity.Entities;

namespace Ck.Gameplay
{
  public static class DeskItems
  {

    [Serializable]
    public struct Background: IComponentData
    {
      public int Id;
    } 

    [Serializable]
    public struct Bomb: IComponentData
    {
      public int Id;
    } 

    [Serializable]
    public struct Figure: IComponentData
    {
      public ChessFigureTypes Id;
    } 

    [Serializable]
    public struct Goal: IComponentData
    {
      public int Id;
    } 

    [Serializable]
    public struct Lock: IComponentData
    {
      public int Id;
    } 

    [Serializable]
    public struct MoveTarget: IComponentData
    {
      public int Id;
    }

    [Serializable]
    public struct PlayerUnit: IComponentData
    {
      public int Id;
    }

  }
  
}