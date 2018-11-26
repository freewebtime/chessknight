using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Ck.Terrains
{
  public struct TerrainSize: IComponentData
  {
    public int2 Value;
  }

  public struct CellSize: IComponentData
  {
    public int2 Value;
  }

  public struct ChunkSize: IComponentData
  {
    public int2 Value;
  }

  public struct TerrainResources: ISharedComponentData
  {
    public Material GroundMaterial;
    public Material WaterMaterial;
  }

  public class TerrainLayers
  {
    
    public struct Heightmap: IComponentData
    {
      public byte Height;
      public byte h00;
      public byte h01;
      public byte h11;
      public byte h10;
    }

    public struct Watermap: IComponentData
    {
      public byte Height;
      public byte h00;
      public byte h01;
      public byte h11;
      public byte h10;
    }

    public struct Bordermap: IComponentData
    {
      public byte North;
      public byte South;
      public byte East;
      public byte West;
    }

    public struct Itemmap: IComponentData
    {
      public byte Id;
    }

    public struct ItemTransform: IComponentData
    {
      public float3 Offset;
      public float3 Rotation;
      public float3 Scale;

      public Matrix4x4 Transform;
    }

  }

  public class ChunkLayers
  {

    public struct GroundMesh: ISharedComponentData
    {
      public Mesh Value;
    }

    public struct WaterMesh: ISharedComponentData
    {
      public Mesh Value;
    }

    public struct BorderMesh: ISharedComponentData
    {
      public Mesh Value;
    }

  }

}