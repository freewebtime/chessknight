using System;
using Fwt.Core.Collections;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  public class CreateDeskItemsViewSystem : ComponentSystem
  {
    struct CreateDeskItemViewsCache: ISystemStateSharedComponentData
    {
      public Entity ViewEntity;
      public GameObject ViewGameObject;
    }

    struct Added
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public ComponentArray<Transform> Transform;
      [ReadOnly] public ComponentDataArray<DeskItem> DeskItem;
      [ReadOnly] public ComponentDataArray<DeskItemType> DeskItemType;
      public SubtractiveComponent<CreateDeskItemViewsCache> NoCache;
    }

    struct Removed
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SharedComponentDataArray<CreateDeskItemViewsCache> Cache;
      public SubtractiveComponent<DeskItem> NoDeskItem;
    }

    [Inject] Added added;
    [Inject] Removed removed;

    [Inject] MediaResourcesApi mediaResourcesApi;

    protected override void OnUpdate()
    {
      UpdateRemoved();
      UpdateAdded();
    }

    private void UpdateAdded()
    {
      if (added.Length == 0) {
        return;
      }

      // check media resources
      var deskResourcesSorted = mediaResourcesApi.GetDeskResourcesSorted();
      if (!deskResourcesSorted.HasValue) {
        return;
      }
      var sortedDeskItems = deskResourcesSorted.Value.DeskItems;

      // extract data from component group, 'cause we'll use EntityManager within loop
      var deskItemTransforms = added.Transform.ToArray();
      var deskItemEntities = new NativeArray<Entity>(added.Length, Allocator.Temp);
      added.Entity.CopyTo(deskItemEntities);
      var deskItemTypes = new NativeArray<DeskItemType>(added.Length, Allocator.Temp);
      added.DeskItemType.CopyTo(deskItemTypes);

      for (int i = 0; i < deskItemEntities.Length; i++)
      {
        var deskItemType = deskItemTypes[i].Type;

        // get view prefab
        GameObject[] deskItemPrefabs;
        if (sortedDeskItems.TryGetValue(deskItemType, out deskItemPrefabs) && deskItemPrefabs.Length > 0) {

          var deskItemEntity = deskItemEntities[i];
          var deskItemTransform = deskItemTransforms[i];

          GameObject viewPrefab = deskItemPrefabs.GetRandom();

          // create view
          var viewGo = UnityEngine.Object.Instantiate(viewPrefab);
          var viewGoEntity = viewGo.GetComponent<GameObjectEntity>();
          var viewEntity = viewGoEntity.Entity;

          // set view as child of deskItem
          viewGo.transform.SetParent(deskItemTransform);
          
          // add references
          PostUpdateCommands.AddComponent(deskItemEntity, new DeskItemViewReference {
            Target = viewEntity
          });
          PostUpdateCommands.AddComponent(viewEntity, new DeskItemReference {
            Target = deskItemEntity
          });

          // add cache
          PostUpdateCommands.AddSharedComponent(deskItemEntity, new CreateDeskItemViewsCache {
            ViewEntity = viewEntity,
            ViewGameObject = viewGo
          });
        }

      }

      deskItemEntities.Dispose();
      deskItemTypes.Dispose();
    }

    private void UpdateRemoved()
    {
      for (int i = 0; i < removed.Length; i++)
      {
        PostUpdateCommands.RemoveComponent<CreateDeskItemViewsCache>(removed.Entity[i]);
      }
    }
  }
}