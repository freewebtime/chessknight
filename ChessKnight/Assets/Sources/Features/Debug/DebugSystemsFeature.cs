using Entitas;

public class DebugSystemsFeature : Feature
{
  public DebugSystemsFeature(Contexts contexts): base ("Debug Systems") {
    Add (new HelloWorldSystem (contexts));
    Add (new DebugMessageSystem (contexts));
  }
}