 using UnityEditor;
 using UnityEngine;
 using System.Collections.Generic;

// Code used from https://answers.unity.com/questions/431952/how-to-show-an-icon-in-hierarchy-view.html
// Last viewed: 05.05.22

 //[InitializeOnLoad]
 class IconLoader
 {
     static Texture2D texture;
     static List<int> markedObjects;
     
     static IconLoader ()
     {
         // Init
         texture = AssetDatabase.LoadAssetAtPath ("Assets/Art/link.png", typeof(Texture2D)) as Texture2D;
         EditorApplication.update += UpdateCB;
         EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
     }
     
     static void UpdateCB ()
     {
         // Check here
         GameObject[] go = Object.FindObjectsOfType (typeof(GameObject)) as GameObject[];
         
         markedObjects = new List<int> ();
         foreach (GameObject g in go) 
         {
             // Example: mark all lights
             if (g.GetComponent<TeleportationPoint> () != null)
                 markedObjects.Add (g.GetInstanceID ());
         }
         
     }
 
     static void HierarchyItemCB (int instanceID, Rect selectionRect)
     {
         // place the icoon to the right of the list:
         Rect r = new Rect (selectionRect); 
         r.x = r.width - 60;
         r.width = 18;
         
         if (markedObjects.Contains (instanceID)) 
         {
             // Draw the texture if it's a light (e.g.)
             GUI.Label (r, texture); 
         }
     }
 }
