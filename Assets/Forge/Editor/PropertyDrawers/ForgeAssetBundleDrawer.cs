using UnityEditor;
using UnityEngine;
using Forge.Editor.Models;
using Forge.Settings;
using System.IO;

namespace Forge.Editor.PropertyDrawers
{
    /// <summary>
    ///     Property drawer for the ForgeAssetBundle class
    /// </summary>
    [CustomPropertyDrawer(typeof(ForgeAssetBundle))]
    public class ForgeAssetBundleDrawer : PropertyDrawer
    {
        /// <summary>
        ///     Draws the ForgeAssetBundle property
        /// </summary>
        /// <param name="position">The start position</param>
        /// <param name="property">The ForgeAssetBundle property</param>
        /// <param name="label">The current label</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(property.FindPropertyRelative("Name"), true);
            EditorGUILayout.PropertyField(property.FindPropertyRelative("GameObjects"), true);

            if (GUI.Button(
                new Rect(position.x + 150F, position.y + EditorGUIUtility.singleLineHeight, position.width - 150F, 20F), 
                Language.ForgeAssetBundleManagerBuildBundle + " " + property.FindPropertyRelative("Name").stringValue)
            ) {
                BuildAssetBundle(property);
            }
        }

        /// <summary>
        ///     Override of default property height.
        ///     This fixes a weird bug(I think) with the fields going down one line.
        /// </summary>
        /// <param name="property">The current property</param>
        /// <param name="label">The label</param>
        /// <returns>The height</returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) - EditorGUIUtility.singleLineHeight;
        }

        /// <summary>
        ///     Generates the current assetBundle
        /// </summary>
        /// <param name="property">The ForgeAssetBundle</param>
        protected void BuildAssetBundle(SerializedProperty property)
        {
            SerializedProperty gameObjectsProperty = property.FindPropertyRelative("GameObjects");

            Object[] assetBundleObjects = new Object[gameObjectsProperty.arraySize];

            Debug.Log(assetBundleObjects.Length);

            if(!Directory.Exists(Path.Combine(InternalData.GamePath, InternalData.ASSET_BUNDLES_DIR)))
            {
                Directory.CreateDirectory(Path.Combine(InternalData.GamePath, InternalData.ASSET_BUNDLES_DIR));
            }

            BuildPipeline.BuildAssetBundles(
                Path.Combine(InternalData.GamePath, InternalData.ASSET_BUNDLES_DIR),
                BuildAssetBundleOptions.None,
                BuildTarget.StandaloneWindows64
            );

            BuildPipeline.BuildAssetBundle(
                assetBundleObjects[0],
                assetBundleObjects,
                Path.Combine(InternalData.GamePath, InternalData.ASSET_BUNDLES_DIR),
                BuildAssetBundleOptions.None,
                BuildTarget.StandaloneWindows64
            );
        }
    }
}
