using Commons.Singleton;
using UnityEngine;

public class Colors: MonoSingleton<Colors>
{
    [SerializeField]
    public Color[] GameColors;
}
