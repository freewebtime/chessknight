using System;
using Ck.Resources;
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
      [ReadOnly] public SharedComponentDataArray<DeskItemResources> DeskItemResources;
      public SubtractiveComponent<CreateDeskItemViewsCache> NoCache;
    }

    struct Removed
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SharedComponentDataArray<CreateDeskItemViewsCache> Cache;
      public SubtractiveComponent<DeskItem> NoDeskItem;
      public SubtractiveComponent<DeskItemResources> NoDeskItemResources;
    }

    [Inject] Added added;
    [Inject] Removed removed;

    protected override void OnUpdate()
    {
      UpdateRemoved();
      UpdateAdded();
    }

    struct DeskItemData
    {
      public Entity Entity;
      public Transform Transform;
      public DeskItemResources ItemResources;
    }

    private void UpdateAdded()
    {
      if (added.Length == 0) {
        return;
      }

      // extract data from component group, 'cause we'll use EntityManager within loop
      var itemsData = new DeskItemData[added.Length];
      for (int i = 0; i < added.Length; i++)
      {
        itemsData[i] = new DeskItemData {
          Entity = added.Entity[i],
          Transform = added.Transform[i],
          ItemResources = added.DeskItemResources[i]
        };
      }

      for (int i = 0; i < itemsData.Length; i++)
      {
        var itemData = itemsData[i];
        var itemResources = itemData.ItemResources;
        var itemTransform = itemData.Transform;
        var deskItemEntity = itemData.Entity;

        // get view prefab
        var viewPrefab = itemResources.ViewPrefab;
        if (viewPrefab != null) {

          // create view
          var viewGo = UnityEngine.Object.Instantiate(viewPrefab);
          var viewGoEntity = viewGo.GetComponent<GameObjectEntity>();
          var viewEntity = viewGoEntity.Entity;

          // set view as child of deskItem
          viewGo.transform.SetParent(itemTransform);

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