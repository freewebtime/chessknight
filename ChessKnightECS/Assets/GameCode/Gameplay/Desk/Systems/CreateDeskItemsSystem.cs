using System;
using System.Collections.Generic;
using Ck.Resources;
using Fwt.Core;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Ck.Gameplay
{
  [UpdateInGroup(typeof(GameLoop.InitializeGroup))]
  [UpdateAfter(typeof(CreateMatchDeskSystem))]
  public class CreateDeskItemsSystem : ComponentSystem
  {
    struct CreateDeskItemsCache: ISystemStateSharedComponentData
    {
      public List<Entity> DeskItemEntities;
      public List<GameObject> DeskItemGo;
    }

    struct Added
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public ComponentDataArray<Desk> Desk;
      [ReadOnly] public SharedComponentDataArray<DeskConfig> DeskConfig;
      [ReadOnly] public SharedComponentDataArray<DeskResources> DeskResources;
      [ReadOnly] public ComponentArray<Transform> Transform;
      public SubtractiveComponent<CreateDeskItemsCache> NoCache;
    }

    struct Removed
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SubtractiveComponent<Desk> NoDesk;
      [ReadOnly] public SubtractiveComponent<DeskConfig> NoConfig; 
      [ReadOnly] public SubtractiveComponent<DeskResources> NoDeskResources;
      [ReadOnly] public SharedComponentDataArray<CreateDeskItemsCache> Cache;
    }

    [Inject] Added added;
    [Inject] Removed removed;

    protected override void OnUpdate()
    {
      UpdateAdded();
      UpdateRemoved();
    }

    private void UpdateAdded() 
    {
      if (added.Length == 0) {
        return;
      }

      var deskEntities = new NativeArray<Entity>(added.Length, Allocator.Temp);
      added.Entity.CopyTo(deskEntities);

      // for each added desk create desk items
      for (int i = 0; i < deskEntities.Length; i++)
      {
        var deskEntity = deskEntities[i];
        var deskTransform = added.Transform[i];

        // get desk config and resources
        var deskConfig = added.DeskConfig[i];
        var deskResources = added.DeskResources[i];
        var deskItemsGroups = deskResources.DeskItems.DeskItemsByItemType;

        // then get data about deskItems from that config
        var deskItemConfigs = deskConfig.DeskItems;
        var deskItemsCount = 0;
        if (deskItemConfigs != null) {
          deskItemsCount = deskItemConfigs.Length;
        }
        
        // create desk item entities
        var deskItemEntities = new List<Entity>(deskItemsCount);
        var deskItemGameObjects = new List<GameObject>(deskItemsCount);
        var deskItemEntitiesByCoord = new Dictionary<int2, List<Entity>>();
  
        for (int k = 0; k < deskItemsCount; k++)
        {
          var deskItemConfig = deskItemConfigs[k];

          // get desk item prefab
          var coordinate = deskItemConfig.Coordinate;
          var position = new float3(coordinate.x, coordinate.y, 0);

          var itemType = deskItemConfig.DeskItemType;
          var itemVersion = deskItemConfig.DeskItemVersion;

          var deskItemResources = GetDeskItemResources(deskItemsGroups, itemType, itemVersion);
          if (deskItemResources.HasValue) {
            
            var dataPrefab = deskItemResources.Value.DataPrefab;
            if (dataPrefab != null) {
              
              // create desk item
              var deskItemGo = UnityEngine.Object.Instantiate(dataPrefab);
              deskItemGo.name = string.Format("Desk item {0}", coordinate);
              deskItemGo.transform.SetParent(deskTransform);

              // get desk item entity from desk game object
              var deskItemGoEntity = deskItemGo.GetComponent<GameObjectEntity>();
              var deskItemEntity = deskItemGoEntity.Entity;

              // set desk item entity values
              // add desk reference
              PostUpdateCommands.AddComponent(deskItemEntity, new DeskReference {
                Target = deskEntity
              });
              // set coordinate and position
              PostUpdateCommands.SetComponent(deskItemEntity, new Coordinate { 
                Value = coordinate 
              });
              PostUpdateCommands.SetComponent(deskItemEntity, new Position {
                Value = position
              });
              PostUpdateCommands.AddSharedComponent(deskItemEntity, deskItemResources.Value);

              // add desk item to all desk items registry
              deskItemEntities.Add(deskItemEntity);
              deskItemGameObjects.Add(deskItemGo);

              List<Entity> coordList;
              if (!deskItemEntitiesByCoord.TryGetValue(coordinate, out coordList) || coordList == null) {
                coordList = new List<Entity>();
                deskItemEntitiesByCoord[coordinate] = coordList;
              }

              coordList.Add(deskItemEntity);
            }
          }

        }

        // add registers to desk
        PostUpdateCommands.AddSharedComponent(deskEntity, new DeskItemsList {
          DeskItemsEntity = deskItemEntities,
          DeskItemGo = deskItemGameObjects
        });
        PostUpdateCommands.AddSharedComponent(deskEntity, new DeskItemsListByCoord {
          Value = deskItemEntitiesByCoord
        });

        // add cache mark to desk
        PostUpdateCommands.AddSharedComponent(deskEntity, new CreateDeskItemsCache {
          DeskItemEntities = deskItemEntities,
          DeskItemGo = deskItemGameObjects
        });
      }

      deskEntities.Dispose();
    }

    private DeskItemResources? GetDeskItemResources(Dictionary<int, DeskItemsGroupResources> deskItemsGroups, int itemType, int itemVersion)
    {
      if (deskItemsGroups == null || deskItemsGroups.Count == 0) {
        return null;
      }

      DeskItemsGroupResources itemsGroup;
      if (!deskItemsGroups.TryGetValue(itemType, out itemsGroup)) {
        return null;
      }

      if (itemsGroup.DeskItems == null || itemsGroup.DeskItems.Length == 0) {
        return null;
      }

      if (itemVersion < 0 || itemVersion >= itemsGroup.DeskItems.Length) {
        return null;
      }

      return itemsGroup.DeskItems[itemVersion];
    }

    private void UpdateRemoved() 
    {
      var removedGroup = GetComponentGroup(
        ComponentType.Create<CreateDeskItemsCache>(),
        ComponentType.Subtractive<Desk>(),
        ComponentType.Subtractive<DeskConfig>()
      );
      var removedEntities = removedGroup.GetEntityArray();
      var cacheArray = removedGroup.GetSharedComponentDataArray<CreateDeskItemsCache>();
      var entityArray = removedGroup.GetEntityArray();

      for (int i = 0; i < removedEntities.Length; i++)
      {
        // clear system cache
        PostUpdateCommands.RemoveComponent<CreateDeskItemsCache>(entityArray[i]);

        var deskCache = cacheArray[i];
        var deskItemEntities = deskCache.DeskItemEntities;
        var deskItemGo = deskCache.DeskItemGo;

        if (deskItemGo != null) {
          for (int j = 0; j < deskItemGo.Count; j++)
          {
            var diGameObject = deskItemGo[j];
            if (diGameObject != null) {
              UnityEngine.Object.Destroy(diGameObject);
            }
          }
        }
      }
    }
  }
}