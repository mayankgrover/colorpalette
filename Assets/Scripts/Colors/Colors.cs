using Commons.Singleton;
using UnityEngine;

public class Colors: MonoSingleton<Colors>
{
    [SerializeField]
    public Color[] GameColors;


    //public static Color[] GameColors = 
    //{
    //    new Color(133f/255f, 40f/255f, 40f/255f),
    //    new Color(99f/255f,  40f/255f, 133f/255f),
    //    new Color(40f/255f,  93f/255f, 133f/255f),
    //    new Color(40f/255f,  133f/255f, 75f/255f),
    //    new Color(133f/255f, 122f/255f, 40f/255f)
    //};
}
