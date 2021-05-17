// =================================================================================================
//    Author: Tomas "NewTwoToss" Szilagyi
//    Date: 17.05.2021
// =================================================================================================

using System;
using System.Collections.Generic;
using Plugins.GameUIBuilder.Editor.Scripts.Nodes;

namespace Plugins.GameUIBuilder.Editor.Scripts
{
    [Serializable]
    public class UndoRedoController
    {
        private readonly DTossCreator _data;
        private readonly List<SourceNode> _sourceNodes;
        private SourceNode _backup;
        private SourceNode _backupRedo;

        public UndoRedoController(DTossCreator data)
        {
            _data = data;
            _sourceNodes = new List<SourceNode>();
        }

        public void Register()
        {
            var clone = _data.SourceNode.MyClone();
            //_sourceNodes.Add(clone);
            _backup = clone;
        }

        public void Undo()
        {
            var clone = _data.SourceNode.MyClone();
            _backupRedo = clone;
            _data.SourceNode = _backup;
        }

        public void Redo()
        {
            _data.SourceNode = _backupRedo;
        }
    }
}