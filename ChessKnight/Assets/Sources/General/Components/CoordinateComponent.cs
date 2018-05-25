using Entitas;
using UnityEngine;
using Entitas.CodeGeneration.Attributes;

[Game, Input]
public class CoordinateComponent: IComponent {
    public IntVector2 value;
}

[Game, Input]
public class Position2Component: IComponent
{
    [EntityIndex]
    public Vector2 value;
}
