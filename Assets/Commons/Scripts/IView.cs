using UnityEngine;

namespace Commons.Util
{
    /// <summary>
    /// @author Mayank Grover 
    /// </summary>

    public interface IView
    {
        GameObject Gameobject { get; }
        void Enable();
        void Disable();
    }
}
