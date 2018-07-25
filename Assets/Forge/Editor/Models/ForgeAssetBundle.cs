using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Forge.Editor.Models
{
    [Serializable]
    public class ForgeAssetBundle
    {
        public string Name;
        public List<UnityEngine.Object> GameObjects = new List<UnityEngine.Object>();
        
    }
}
