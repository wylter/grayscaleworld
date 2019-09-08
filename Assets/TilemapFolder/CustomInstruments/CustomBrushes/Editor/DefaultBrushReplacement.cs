using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityEditor {

    [CustomGridBrush(true, true, true, "Default Brush")]
    public class DefaultBrushReplacement : GridBrush {
        // Just placeholder	to show info text
    }

    [CustomEditor(typeof(DefaultBrushReplacement))]
    public class DefaultBrushReplacementEditor : GridBrushEditor {

        private string pathBasic = "Prefabs/InitializeLevelPrefabs/GameController.prefab";
        private string pathDynamic = "Prefabs/InitializeLevelPrefabs/DynamicLevel.prefab";
        //private string pathSound = "Prefabs/InitializeLevelPrefabs/SoundManager.prefab";

        public override void OnPaintInspectorGUI() {
            GUILayout.Space(5f);
            GUILayout.Label("It is a generic brush for painting tiles and game objects. To create the level use the Platform Brush.");
            GUILayout.Space(5f);
            if (!Camera.main) {
                GUILayout.Space(5f);
                GUILayout.Label("This scene is not yet ready for level editing.");
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Initialize Scene")) {
                    PrepareScene();
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }

        //instantiate all prefabs inside the basic level prefab
        private void PrepareScene() {

            /* Not necessary anymore since the Soundmanager has been added to the GameController
            //Instantiate the Soundmanager
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/" + pathSound);
            PrefabUtility.InstantiatePrefab(prefab);
            */

            //Instantiate the basic level
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/" + pathBasic);
            /*GameObject prefabClone =*/ PrefabUtility.InstantiatePrefab(prefab) /*as GameObject*/;

            //Instantiate the basic dynamic level
            prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/" + pathDynamic);
            UnityEngine.Object dynamicLevel = PrefabUtility.InstantiatePrefab(prefab);
            PrefabUtility.DisconnectPrefabInstance(dynamicLevel);


            /*prefabClone.transform.DetachChildren();

            DestroyImmediate(prefabClone);*/
        }
    }
}
