using System;
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
      // check media resources
      var deskResources = mediaResourcesApi.GetDeskResources();
      if (!deskResources.HasValue) {
        return;
      }

      // extract data from component group, 'cause we'll use EntityManager within loop
      var deskItemTransforms = added.Transform;
      var deskItemEntities = new NativeArray<Entity>(added.Length, Allocator.Temp);
      added.Entity.CopyTo(deskItemEntities);

      for (int i = 0; i < deskItemEntities.Length; i++)
      {
        // get view prefab
        GameObject viewPrefab = null;

        // prepare to creation view
        var deskItemEntity = deskItemEntities[i];
        var deskItemTransform = deskItemTransforms[i];

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

    private void UpdateRemoved()
    {
      for (int i = 0; i < removed.Length; i++)
      {
        PostUpdateCommands.RemoveComponent<CreateDeskItemViewsCache>(removed.Entity[i]);
      }
    }
  }
}