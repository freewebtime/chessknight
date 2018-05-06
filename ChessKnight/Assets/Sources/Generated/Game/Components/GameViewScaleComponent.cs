//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ChessKnight.View.ViewScaleComponent viewScale { get { return (ChessKnight.View.ViewScaleComponent)GetComponent(GameComponentsLookup.ViewScale); } }
    public bool hasViewScale { get { return HasComponent(GameComponentsLookup.ViewScale); } }

    public void AddViewScale(UnityEngine.Vector3 newValue) {
        var index = GameComponentsLookup.ViewScale;
        var component = CreateComponent<ChessKnight.View.ViewScaleComponent>(index);
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceViewScale(UnityEngine.Vector3 newValue) {
        var index = GameComponentsLookup.ViewScale;
        var component = CreateComponent<ChessKnight.View.ViewScaleComponent>(index);
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveViewScale() {
        RemoveComponent(GameComponentsLookup.ViewScale);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherViewScale;

    public static Entitas.IMatcher<GameEntity> ViewScale {
        get {
            if (_matcherViewScale == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ViewScale);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherViewScale = matcher;
            }

            return _matcherViewScale;
        }
    }
}
