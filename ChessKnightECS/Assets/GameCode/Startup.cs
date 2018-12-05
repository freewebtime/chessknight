using System;
using Ck.Gui;
using Ck.Resources;
using Unity.Entities;
using UnityEngine;

public class Startup: MonoBehaviour 
{
  public void Start()
  {
    PrepareResources();
  }

  private void PrepareResources()
  {
    var gameResourcesWrapper = GameObject.FindObjectOfType<GameResourcesWrapper>();
    if (gameResourcesWrapper != null) {
      gameResourcesWrapper.Init();
    }
  }
}