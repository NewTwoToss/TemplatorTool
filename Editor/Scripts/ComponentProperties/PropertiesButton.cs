// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 02.06.2021
// =================================================================================================

namespace Plugins.Templator.Editor.Scripts.ComponentProperties
{
    public class PropertiesButton : IPropertiesButton
    {
        public string Name { get; set; }
        public int Width { get; }
        public int Height { get; }
        public int IndexAnchor { get; }
        public int IndexPivot { get; }
    }
}