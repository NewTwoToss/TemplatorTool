// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 09.05.2021
// =================================================================================================

using UnityEngine;

namespace Plugins.GameUIBuilder.Editor.Scripts.Creators.Contracts
{
    public interface IParentable
    {
        public RectTransform Parent { get; }
    }
}