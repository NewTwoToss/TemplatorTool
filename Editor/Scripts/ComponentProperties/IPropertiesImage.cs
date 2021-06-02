// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 10.05.2021
// =================================================================================================

using UnityEngine;

namespace Plugins.Templator.Editor.Scripts.ComponentProperties
{
    public interface IPropertiesImage
    {
        string Name { get; }

        int Width { get; }

        int Height { get; }
        
        int IndexAnchor { get; }
        
        int IndexPivot { get; }

        Sprite SourceImage { get; }

        Color Color { get; }

        bool RaycastTarget { get; }
        
        bool Maskable { get; }
        
        bool SetNativeSize { get; }
    }
}