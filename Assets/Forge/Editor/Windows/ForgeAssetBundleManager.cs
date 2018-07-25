using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Forge.Editor.Models;
using Forge.Settings;

namespace Forge.Editor.Windows
{
    /// <summary>
    ///     This editor window assists users in selecting and exporting assets
    /// </summary>
    public class ForgeAssetBundleManager : EditorWindow
    {
        /// <summary>
        ///     The list of ForgeAssetBundles
        /// </summary>
        public ForgeAssetBundle[] AssetBundleObjects = new ForgeAssetBundle[1];

        /// <summary>
        ///     The current scroll position
        /// </summary>
        private Vector2 _scrollPosition = Vector2.zero;

        /// <summary>
        ///    Shows this window
        /// </summary>
        [MenuItem("Forge/Manage Asset Bundle")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ForgeAssetBundleManager), true, Language.ForgeAssetBundleManagerWindowName);
        }

        /// <summary>
        ///     All GUI drawing happens here
        /// </summary>
        public void OnGUI()
        {
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, true, true);

            DrawAssetBundlesList();

            GUILayout.EndScrollView();
        }
        
        /// <summary>
        ///     Draws the ForgeAssetBundle list
        /// </summary>
        private void DrawAssetBundlesList()
        {
            ScriptableObject target = this;
            SerializedObject serializedObject = new SerializedObject(target);
            SerializedProperty serializedProperty = serializedObject.FindProperty("AssetBundleObjects");

            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
