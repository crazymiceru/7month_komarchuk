using UnityEngine;

[CreateAssetMenu(menuName = "My/Global",fileName = "Global")]
public class Global : ScriptableObject
{
    [Header("Background")]
    public Vector2 SizeLoopBackground;
    public Vector2 coefficientParallaxBackground;
    public Vector2 coefficientParallaxBackgroundFly;
    
}
