using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using UnityEditor;

namespace UnityEngine {

    public enum destinationOption { Neutral, Black, White };

    [Serializable]
    [CreateAssetMenu]
    public class PlatformTile : Tile {

        //Destination layer of the tile
        public destinationOption destinationLayer;

        public destinationOption getDestination() {
            return destinationLayer;
        }


        /*
        #if UNITY_EDITOR       //The block inside the #if statement adds the Platform to the assets create tab
            [MenuItem("Assets/Create/Tile/PlatformTile")]

            public static void CreatePlatformTile()
            {
                string path = EditorUtility.SaveFilePanelInProject("Save Platform Tile", "New Tile", "asset", "Save Tile", "Tile");
                if (path == "")
                    return;
                AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<PlatformTile>(), path);
            }
        #endif
        */
    }
}