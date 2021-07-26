using UnityEngine;

[CreateAssetMenu(menuName = "My/Paralax",fileName = "Paralax")]
public class ParalaxCfg : ScriptableObject
{
    [SerializeField] private Vector2 _SizeLoopBackground;
    [SerializeField] private Vector2 _coefficientParallaxBackground;

    public Vector2 SizeLoopBackground => _SizeLoopBackground;
    public Vector2 Coefficient => _coefficientParallaxBackground;
}
