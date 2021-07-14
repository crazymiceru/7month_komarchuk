using UnityEngine;

[CreateAssetMenu(menuName = "My/Paralax",fileName = "ParalaxData")]
public class Paralax : ScriptableObject
{
    [Header("Background")]
    [SerializeField] private Vector2 _SizeLoopBackground;
    public Vector2 SizeLoopBackground => _SizeLoopBackground;

    [SerializeField] private Vector2 _coefficientParallaxBackground;
    public Vector2 coefficientParallaxBackground => _coefficientParallaxBackground;

    [SerializeField] private Vector2 _coefficientParallaxBackgroundFly;
    public Vector2 coefficientParallaxBackgroundFly => _coefficientParallaxBackgroundFly;
}
