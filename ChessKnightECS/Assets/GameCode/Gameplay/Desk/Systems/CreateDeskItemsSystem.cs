using System.Collections.Generic;
using Fwt.Core;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Ck.Gameplay
{
  [UpdateInGroup(typeof(GameLoop.InitializeGroup))]
  [UpdateAfter(typeof(CreateMatchDeskSystem))]
  public class CreateDeskItemsSystem : ComponentSystem
  {
    struct CreateDeskItemsCache: ISystemStateSharedComponentData
    {
      public List<Entity> Entities;
    }

    struct Added
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public ComponentDataArray<Desk> Desk;
      [ReadOnly] public SharedComponentDataArray<DeskConfig> Config; 
      public SubtractiveComponent<CreateDeskItemsCache> NoCache;
    }

    struct Removed
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SubtractiveComponent<Desk> NoDesk;
      [ReadOnly] public SubtractiveComponent<DeskConfig> NoConfig; 
      [ReadOnly] public SharedComponentDataArray<CreateDeskItemsCache> Cache;
    }

    [Inject] Added added;
    [Inject] Removed removed;

    [Inject] DataResourcesApi dataResourcesApi;

    protected override void OnUpdate()
    {
      UpdateAdded();
      UpdateRemoved();
    }

    private void UpdateAdded() {
      if (added.Length == 0) {
        return;
      }

      var deskResources = dataResourcesApi.GetDeskResources();
      if (!deskResources.HasValue) {
        return;        
      }

      var deskEntities = new NativeArray<Entity>(added.Length, Allocator.Temp);
      added.Entity.CopyTo(deskEntities);

      // for each added desk create desk items
      for (int i = 0; i < deskEntities.Length; i++)
      {
        var deskEntity = deskEntities[i];

        // get desk config 
        var deskConfig = EntityManager.GetSharedComponentData<DeskConfig>(deskEntity);
        // then get data about deskItems from that config
        var deskItemConfigs = deskConfig.DeskItems;
        var deskItemsCount = 0;
        if (deskItemConfigs != null) {
          deskItemsCount = deskItemConfigs.Length;
        }
        
        // create desk item entities
        var deskItemEntities = new List<Entity>(deskItemsCount);
        var deskItemEntitiesByCoord = new Dictionary<int2, List<Entity>>();
  
        for (int k = 0; k < deskItemsCount; k++)
        {
          var deskItemConfig = deskItemConfigs[k];
          // get desk item prefab
          var deskItemPrefab = deskItemConfig.Prefab;
          var coordinate = deskItemConfig.Coordinate;

          // create desk item
          var deskItemGo = UnityEngine.Object.Instantiate(deskItemPrefab);
          var deskItemGoEntity = deskItemGo.GetComponent<GameObjectEntity>();
          var deskItemEntity = deskItemGoEntity.Entity;
          
          PostUpdateCommands.SetComponent(deskItemEntity, new Coordinate { 
            Value = coordinate 
          });
          PostUpdateCommands.AddComponent(deskItemEntity, new DeskReference {
            Target = deskEntity
          });

          // add desk item to all desk items registry
          deskItemEntities.Add(deskItemEntity);

          List<Entity> coordList;
          if (!deskItemEntitiesByCoord.TryGetValue(coordinate, out coordList) || coordList == null) {
            coordList = new List<Entity>();
            deskItemEntitiesByCoord[coordinate] = coordList;
          }

          coordList.Add(deskItemEntity);
        }

        // add registers to desk
        PostUpdateCommands.AddSharedComponent(deskEntity, new DeskItemsList {
          Value = deskItemEntities
        });
        PostUpdateCommands.AddSharedComponent(deskEntity, new DeskItemsListByCoord {
          Value = deskItemEntitiesByCoord
        });

        // add cache mark to desk
        PostUpdateCommands.AddSharedComponent(deskEntity, new CreateDeskItemsCache {
          Entities = deskItemEntities
        });
      }

      deskEntities.Dispose();
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
        var cacheList = cacheArray[i].Entities;
        for (int k = 0; k < cacheList.Count; k++)
        {
          var deskItemEntity = cacheList[k];
          if (EntityManager.Exists(deskItemEntity)) {
            PostUpdateCommands.DestroyEntity(deskItemEntity);
          }
        }

        PostUpdateCommands.RemoveComponent<CreateDeskItemsCache>(entityArray[i]);
      }
    }
  }
}