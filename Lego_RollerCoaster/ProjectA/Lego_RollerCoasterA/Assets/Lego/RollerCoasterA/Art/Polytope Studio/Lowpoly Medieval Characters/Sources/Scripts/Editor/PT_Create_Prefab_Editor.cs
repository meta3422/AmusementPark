using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Polytope
{
    [CustomEditor(typeof(PT_Create_Prefab))]
    public class PT_Create_Prefab_Editor : Editor
    {
        private PT_Create_Prefab script = null;

        // Meshes
        private SerializedProperty meshes = null;
        private SerializedProperty loaded = null;
        private SerializedProperty material = null;
        private SerializedProperty preview = null;
        private SerializedProperty time = null;
        private SerializedProperty duplicateMaterial = null;

        // Preview
        private float frequency = 1f;

        private void OnEnable()
        {
            // Meshes
            script = (PT_Create_Prefab)target;
            loaded = serializedObject.FindProperty("loaded");
            meshes = serializedObject.FindProperty("meshes");
            material = serializedObject.FindProperty("material");
            preview = serializedObject.FindProperty("preview");
            time = serializedObject.FindProperty("time");
            duplicateMaterial = serializedObject.FindProperty("duplicateMaterial");

            EditorApplication.update += Preview;
            time.floatValue = 0f;
            serializedObject.ApplyModifiedProperties();
            //Debug.Log("Enable");
        }

        private void OnDisable()
        {
            //Debug.Log("Disable");
            EditorApplication.update -= Preview;
        }

        private void Preview()
        {
            if(preview.boolValue)
            {
                if (EditorApplication.timeSinceStartup > time.floatValue)
                {
                    time.floatValue = (float)EditorApplication.timeSinceStartup + frequency;
                    serializedObject.ApplyModifiedProperties();
                    MeshesRandom();
                }
            }
        }    

        private void SetMaterialProperty_C(string name, string label)
        {
            string shaderColor = "_" + name.ToUpper() + "COLOR";
            SerializedProperty materialInstance = material.FindPropertyRelative("instance");

            // Check if the shader has collor for this property
            if (IsMaterialProperty(shaderColor))
            {
                SerializedProperty color = serializedObject.FindProperty(name).FindPropertyRelative("color");
                EditorGUILayout.PropertyField(color, new GUIContent(label + " Color"));
                if (color.colorValue != (materialInstance.objectReferenceValue as Material).GetColor(shaderColor))
                {
                    (materialInstance.objectReferenceValue as Material).SetColor(shaderColor, color.colorValue);
                }
            }
        }

        private void SetMaterialProperty_CI(string name, string label)
        {
            string shaderColor = "_" + name.ToUpper() + "COLOR";
            string shaderTex = "_" + name.ToUpper() + "MASK";
            SerializedProperty materialInstance = material.FindPropertyRelative("instance");

            // Check if the shader has collor for this property
            if (IsMaterialProperty(shaderColor))
            {
                SerializedProperty color = serializedObject.FindProperty(name).FindPropertyRelative("color");
                EditorGUILayout.PropertyField(color, new GUIContent(label + " Color"));
                if (color.colorValue != (materialInstance.objectReferenceValue as Material).GetColor(shaderColor))
                {
                    (materialInstance.objectReferenceValue as Material).SetColor(shaderColor, color.colorValue);
                }
            }

            // Check if the shader has texture for this property
            if (IsMaterialProperty(shaderTex))
            {
                SerializedProperty sprite = serializedObject.FindProperty(name).FindPropertyRelative("image");
                sprite.objectReferenceValue = (Sprite)EditorGUILayout.ObjectField(label + " Sprite", sprite.objectReferenceValue, typeof(Sprite), allowSceneObjects: true);
                string pathToShaderTexture = AssetDatabase.GetAssetPath((materialInstance.objectReferenceValue as Material).GetTexture("_COATOFARMSMASK"));
                string pathToScriptTexture = AssetDatabase.GetAssetPath(sprite.objectReferenceValue);
                if (pathToShaderTexture != pathToScriptTexture)
                {
                    (materialInstance.objectReferenceValue as Material).SetTexture(shaderTex, AssetDatabase.LoadAssetAtPath<Texture>(pathToScriptTexture));
                }
            }
        }

        private void SetMaterialProperty_CS(string name, string label)
        {
            string shaderColor = "_" + name.ToUpper() + "COLOR";
            string shaderSmoothness = "_" + name.ToUpper() + "SMOOTHNESS";
            SerializedProperty materialInstance = material.FindPropertyRelative("instance");

            // Check if the shader has collor for this property
            if (IsMaterialProperty(shaderColor))
            {
                SerializedProperty color = serializedObject.FindProperty(name).FindPropertyRelative("color");
                EditorGUILayout.PropertyField(color, new GUIContent(label + " Color"));
                if (color.colorValue != (materialInstance.objectReferenceValue as Material).GetColor(shaderColor))
                {
                    (materialInstance.objectReferenceValue as Material).SetColor(shaderColor, color.colorValue);
                }
            }

            // Check if the shader has smoothness for this property
            if (IsMaterialProperty(shaderSmoothness))
            {
                SerializedProperty smoothness = serializedObject.FindProperty(name).FindPropertyRelative("smoothness");
                EditorGUILayout.PropertyField(smoothness, new GUIContent(label + " Smoothness"));
                if (smoothness.floatValue != (materialInstance.objectReferenceValue as Material).GetFloat(shaderSmoothness))
                {
                    (materialInstance.objectReferenceValue as Material).SetFloat(shaderSmoothness, smoothness.floatValue);
                }
            }
        }

        private void SetMaterialProperty_CSM(string name, string label)
        {
            string shaderColor = "_" + name.ToUpper() + "COLOR";
            string shaderSmoothness = "_" + name.ToUpper() + "SMOOTHNESS";
            string shaderMetallic = "_" + name.ToUpper() + "METALLIC";
            SerializedProperty materialInstance = material.FindPropertyRelative("instance");

            // Check if the shader has collor for this property
            if (IsMaterialProperty(shaderColor))
            {
                SerializedProperty color = serializedObject.FindProperty(name).FindPropertyRelative("color");
                EditorGUILayout.PropertyField(color, new GUIContent(label + " Color"));
                if (color.colorValue != (materialInstance.objectReferenceValue as Material).GetColor(shaderColor))
                {
                    (materialInstance.objectReferenceValue as Material).SetColor(shaderColor, color.colorValue);
                }
            }

            // Check if the shader has smoothness for this property
            if (IsMaterialProperty(shaderSmoothness))
            {
                SerializedProperty smoothness = serializedObject.FindProperty(name).FindPropertyRelative("smoothness");
                EditorGUILayout.PropertyField(smoothness, new GUIContent(label + " Smoothness"));
                if (smoothness.floatValue != (materialInstance.objectReferenceValue as Material).GetFloat(shaderSmoothness))
                {
                    (materialInstance.objectReferenceValue as Material).SetFloat(shaderSmoothness, smoothness.floatValue);
                }
            }

            // Check if the shader has metallic for this property
            if (IsMaterialProperty(shaderMetallic))
            {
                SerializedProperty metallic = serializedObject.FindProperty(name).FindPropertyRelative("metallic");
                EditorGUILayout.PropertyField(metallic, new GUIContent(label + " Metallic"));
                if (metallic.floatValue != (materialInstance.objectReferenceValue as Material).GetFloat(shaderMetallic))
                {
                    (materialInstance.objectReferenceValue as Material).SetFloat(shaderMetallic, metallic.floatValue);
                }
            }
        }

        private void SetMaterialProperty_CSP(string name, string label)
        {
            string shaderColor = "_" + name.ToUpper() + "COLOR";
            string shaderSize = "_" + name.ToUpper() + "SIZE";
            string shaderPower = "_" + name.ToUpper() + "POWER";
            SerializedProperty materialInstance = material.FindPropertyRelative("instance");

            // Check if the shader has collor for this property
            //Debug.Log("CSP: " + shaderColor + " value: " + IsMaterialProperty(shaderColor));
            if (IsMaterialProperty(shaderColor))
            {
                SerializedProperty color = serializedObject.FindProperty(name).FindPropertyRelative("color");
                EditorGUILayout.PropertyField(color, new GUIContent(label + " Color"));
                if (color.colorValue != (materialInstance.objectReferenceValue as Material).GetColor(shaderColor))
                {
                    (materialInstance.objectReferenceValue as Material).SetColor(shaderColor, color.colorValue);
                }
            }

            // Check if the shader has smoothness for this property
            if (IsMaterialProperty(shaderSize))
            {
                SerializedProperty size = serializedObject.FindProperty(name).FindPropertyRelative("size");
                EditorGUILayout.PropertyField(size, new GUIContent(label + " Size"));
                if (size.floatValue != (materialInstance.objectReferenceValue as Material).GetFloat(shaderSize))
                {
                    (materialInstance.objectReferenceValue as Material).SetFloat(shaderSize, size.floatValue);
                }
            }

            // Check if the shader has metallic for this property
            if (IsMaterialProperty(shaderPower))
            {
                SerializedProperty power = serializedObject.FindProperty(name).FindPropertyRelative("power");
                EditorGUILayout.PropertyField(power, new GUIContent(label + " Power"));
                if (power.floatValue != (materialInstance.objectReferenceValue as Material).GetFloat(shaderPower))
                {
                    (materialInstance.objectReferenceValue as Material).SetFloat(shaderPower, power.floatValue);
                }
            }
        }

        private void SetMaterialProperty_UTL()
        {
            SerializedProperty materialInstance = material.FindPropertyRelative("instance");

            if (IsMaterialProperty("_MetalicOn"))
            {
                SerializedProperty metallicOn = serializedObject.FindProperty("metallicOn");
                EditorGUILayout.PropertyField(metallicOn, new GUIContent("Metallic ON"));
                float metallic = metallicOn.boolValue ? 1f : 0f;
                if (metallic != (materialInstance.objectReferenceValue as Material).GetFloat("_MetalicOn"))
                {
                    (materialInstance.objectReferenceValue as Material).SetFloat("_MetalicOn", metallic);
                    serializedObject.ApplyModifiedProperties();
                }
            }

            if (IsMaterialProperty("_SmoothnessOn"))
            {
                SerializedProperty smoothnessOn = serializedObject.FindProperty("smoothnessOn");
                EditorGUILayout.PropertyField(smoothnessOn, new GUIContent("Smoothness ON"));
                float smoothness = smoothnessOn.boolValue ? 1f : 0f;
                if (smoothness != (materialInstance.objectReferenceValue as Material).GetFloat("_SmoothnessOn"))
                {
                    (materialInstance.objectReferenceValue as Material).SetFloat("_SmoothnessOn", smoothness);
                    serializedObject.ApplyModifiedProperties();
                }
            }

            if (IsMaterialProperty("_OCCLUSION"))
            {
                SerializedProperty occlusion = serializedObject.FindProperty("occlusion");
                EditorGUILayout.PropertyField(occlusion, new GUIContent("Occlusion"));
                if (occlusion.floatValue != (materialInstance.objectReferenceValue as Material).GetFloat("_OCCLUSION"))
                {
                    (materialInstance.objectReferenceValue as Material).SetFloat("_OCCLUSION", occlusion.floatValue);
                }
            }

            SerializedProperty gpuinstancing = serializedObject.FindProperty("gpuinstancing");
            SerializedProperty doubleSidedGI = serializedObject.FindProperty("doubleSidedGI");
            SerializedProperty renderQueue = serializedObject.FindProperty("renderQueue");
            EditorGUILayout.PropertyField(gpuinstancing, new GUIContent("Gpu Instancing"));
            EditorGUILayout.PropertyField(doubleSidedGI, new GUIContent("Double Sided GI"));
            EditorGUILayout.PropertyField(renderQueue, new GUIContent("Render Queue"));

            (materialInstance.objectReferenceValue as Material).enableInstancing = gpuinstancing.boolValue;
            (materialInstance.objectReferenceValue as Material).doubleSidedGI = doubleSidedGI.boolValue;
            (materialInstance.objectReferenceValue as Material).renderQueue = renderQueue.intValue;
        }

        private void MaterialDropDownSelection()
        {
            List<string> materialNames = new List<string>();
            SerializedProperty list = material.FindPropertyRelative("assets");
            SerializedProperty idx = material.FindPropertyRelative("index");

            for (int i = 0; i < list.arraySize; i++)
            {
                materialNames.Add((list.GetArrayElementAtIndex(i).objectReferenceValue as Material).name);
            }

            int index = EditorGUILayout.Popup("Material", idx.intValue, materialNames.ToArray());
            if (index < 0) { index = 0; }
            if(idx.intValue != index)
            {
                idx.intValue = index;
                material.FindPropertyRelative("instance").objectReferenceValue = new Material(list.GetArrayElementAtIndex(index).objectReferenceValue as Material);
                ReapplyMaterials();
            }
        }

        private bool IsMaterialProperty(string name)
        {
            try
            {
                return (material.FindPropertyRelative("instance").objectReferenceValue as Material).HasProperty(name);
            }
            catch (Exception exception)
            {
                Debug.LogError("Property: " + name + " message: " + exception.Message);
                return false;
            }

        }

        private void GetDefaultProperty_C(string name)
        {
            string shaderColor = "_" + name.ToUpper() + "COLOR";
            Material dftmat = material.FindPropertyRelative("assets").GetArrayElementAtIndex(material.FindPropertyRelative("index").intValue).objectReferenceValue as Material;

            if (IsMaterialProperty(shaderColor))
            {
                serializedObject.FindProperty(name).FindPropertyRelative("color").colorValue = dftmat.GetColor(shaderColor);
            }
        }

        private void GetDefaultProperty_CI(string name)
        {
            string shaderColor = "_" + name.ToUpper() + "COLOR";
            string shaderTex = "_" + name.ToUpper() + "MASK";
            Material dftmat = material.FindPropertyRelative("assets").GetArrayElementAtIndex(material.FindPropertyRelative("index").intValue).objectReferenceValue as Material;

            if (IsMaterialProperty(shaderColor))
            {
                serializedObject.FindProperty(name).FindPropertyRelative("color").colorValue = dftmat.GetColor(shaderColor);
            }

            if (IsMaterialProperty(shaderTex))
            {
                serializedObject.FindProperty(name).FindPropertyRelative("image").objectReferenceValue = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GetAssetPath(dftmat.GetTexture(shaderTex)));
            }
        }

        private void GetDefaultProperty_CS(string name)
        {
            string shaderColor = "_" + name.ToUpper() + "COLOR";
            string shaderSmoothness = "_" + name.ToUpper() + "SMOOTHNESS";
            Material dftmat = material.FindPropertyRelative("assets").GetArrayElementAtIndex(material.FindPropertyRelative("index").intValue).objectReferenceValue as Material;

            if (IsMaterialProperty(shaderColor))
            {
                serializedObject.FindProperty(name).FindPropertyRelative("color").colorValue = dftmat.GetColor(shaderColor);

            }

            if (IsMaterialProperty(shaderSmoothness))
            {
                serializedObject.FindProperty(name).FindPropertyRelative("smoothness").floatValue = dftmat.GetFloat(shaderSmoothness);
            }
        }

        private void GetDefaultPorperty_CSM(string name)
        {
            string shaderColor = "_" + name.ToUpper() + "COLOR";
            string shaderSmoothness = "_" + name.ToUpper() + "SMOOTHNESS";
            string shaderMetallic = "_" + name.ToUpper() + "METALLIC";
            Material dftmat = material.FindPropertyRelative("assets").GetArrayElementAtIndex(material.FindPropertyRelative("index").intValue).objectReferenceValue as Material;

            if (IsMaterialProperty(shaderColor))
            {
                serializedObject.FindProperty(name).FindPropertyRelative("color").colorValue = dftmat.GetColor(shaderColor);
            }

            if (IsMaterialProperty(shaderSmoothness))
            {
                serializedObject.FindProperty(name).FindPropertyRelative("smoothness").floatValue = dftmat.GetFloat(shaderSmoothness);
            }

            if (IsMaterialProperty(shaderMetallic))
            {
                serializedObject.FindProperty(name).FindPropertyRelative("metallic").floatValue = dftmat.GetFloat(shaderMetallic);
            }
        }

        private void GetDefaultProperty_CSP(string name)
        {
            string shaderColor = "_" + name.ToUpper() + "COLOR";
            string shaderSize = "_" + name.ToUpper() + "SIZE";
            string shaderPower = "_" + name.ToUpper() + "POWER";
            Material dftmat = material.FindPropertyRelative("assets").GetArrayElementAtIndex(material.FindPropertyRelative("index").intValue).objectReferenceValue as Material;

            if (IsMaterialProperty(shaderColor))
            {
                serializedObject.FindProperty(name).FindPropertyRelative("color").colorValue = dftmat.GetColor(shaderColor);
            }

            if (IsMaterialProperty(shaderSize))
            {
                serializedObject.FindProperty(name).FindPropertyRelative("size").floatValue = dftmat.GetFloat(shaderSize);
            }

            if (IsMaterialProperty(shaderPower))
            {
                serializedObject.FindProperty(name).FindPropertyRelative("power").floatValue = dftmat.GetFloat(shaderPower);
            }
        }

        private void GetDefaultProperty_UTL()
        {
            Material dftmat = material.FindPropertyRelative("assets").GetArrayElementAtIndex(material.FindPropertyRelative("index").intValue).objectReferenceValue as Material;

            if (IsMaterialProperty("_OCCLUSION"))
            {
                serializedObject.FindProperty("occlusion").floatValue = dftmat.GetFloat("_OCCLUSION");
            }

            if (IsMaterialProperty("_MetalicOn"))
            {
                serializedObject.FindProperty("metallicOn").boolValue = dftmat.GetFloat("_MetalicOn") > 0f ? true : false;
            }

            if (IsMaterialProperty("_SmoothnessOn"))
            {
                serializedObject.FindProperty("smoothnessOn").boolValue = dftmat.GetFloat("_SmoothnessOn") > 0f ? true : false;
            }

            SerializedProperty gpuinstancing = serializedObject.FindProperty("gpuinstancing");
            SerializedProperty doubleSidedGI = serializedObject.FindProperty("doubleSidedGI");
            SerializedProperty renderQueue = serializedObject.FindProperty("renderQueue");
            gpuinstancing.boolValue = dftmat.enableInstancing;
            doubleSidedGI.boolValue = dftmat.doubleSidedGI;
            renderQueue.intValue = dftmat.renderQueue;
        }

        private void DefaultMaterialProperties()
        {
            GetDefaultProperty_CS("skin");
            GetDefaultProperty_CS("eyes");
            GetDefaultProperty_CS("hair");
            GetDefaultProperty_CS("sclera");
            GetDefaultProperty_CS("lips");
            GetDefaultProperty_CS("scars");
            GetDefaultProperty_CS("other");
            GetDefaultPorperty_CSM("metal1");
            GetDefaultPorperty_CSM("metal2");
            GetDefaultPorperty_CSM("metal3");
            GetDefaultPorperty_CSM("metal4");
            GetDefaultProperty_CS("leather1");
            GetDefaultProperty_CS("leather2");
            GetDefaultProperty_CS("leather3");
            GetDefaultProperty_CS("leather4");
            GetDefaultProperty_C("cloth1");
            GetDefaultProperty_C("cloth2");
            GetDefaultProperty_C("cloth3");
            GetDefaultProperty_C("cloth4");
            GetDefaultProperty_CS("gems1");
            GetDefaultProperty_CS("gems2");
            GetDefaultProperty_CS("gems3");
            GetDefaultProperty_C("feathers1");
            GetDefaultProperty_C("feathers2");
            GetDefaultProperty_C("feathers3");
            GetDefaultProperty_CI("coatofarms");
            GetDefaultProperty_CSP("light1");
            GetDefaultProperty_CSP("light2");
            GetDefaultProperty_CSP("light3");
            GetDefaultProperty_UTL();
        }

        public void ExpansionDragAndDrop()
        {
            Event evt = Event.current;
            Rect area = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));

            // Create the box
            int width = 2;
            int height = 2;
            GUIStyle style = new GUIStyle(GUI.skin.box);
            Texture2D texture = new Texture2D(width, height);
            Color background = Color.white;
            Color[] pix = new Color[width * height];

            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = background;
            }

            texture.SetPixels(pix);
            texture.Apply();
            style.normal.background = texture;
            GUI.Box(area, "Drag & Drop Polytope expansion prefab here", style);

            // Handle the event
            switch (evt.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!area.Contains(evt.mousePosition))
                        return;

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();
                        GameObject expansion = (GameObject)DragAndDrop.objectReferences[0];
                        bool validExpansion = true;
                        try
                        {
                            Avatar expansionAvatar = expansion.GetComponent<Animator>().avatar;
                            Avatar baseAvatar = script.gameObject.GetComponent<Animator>().avatar;

                            if (expansionAvatar != baseAvatar)
                            {
                                validExpansion = false;
                            }
                        }
                        catch
                        {
                            validExpansion = false;
                        }


                        if (!validExpansion)
                        {
                            EditorUtility.DisplayDialog("Expansion",
                                "Prefab cannot be imported! Not a Polytope expansion prefab.",
                                "Ok");
                            return;
                        }

                        SkinnedMeshRenderer[] importMeshes = expansion.GetComponentsInChildren<SkinnedMeshRenderer>();
                        foreach (SkinnedMeshRenderer mesh in importMeshes)
                        {
                            for (int i = 0; i < meshes.arraySize; i++)
                            {
                                SerializedProperty list = meshes.GetArrayElementAtIndex(i).FindPropertyRelative("list");
                                SerializedProperty type = meshes.GetArrayElementAtIndex(i).FindPropertyRelative("type");

                                if (mesh.name.Contains(type.stringValue))
                                {
                                    SkinnedMeshRenderer target = list.GetArrayElementAtIndex(0).objectReferenceValue as SkinnedMeshRenderer;
                                    SkinnedMeshRenderer instance = Instantiate<SkinnedMeshRenderer>(mesh);
                                    instance.transform.parent = target.transform.parent;
                                    instance.transform.localPosition = target.transform.localPosition;
                                    instance.transform.localRotation = target.transform.localRotation;
                                    instance.transform.localScale = target.transform.localScale;
                                    instance.bones = target.bones;
                                    instance.rootBone = target.rootBone;
                                    instance.enabled = false;
                                    list.arraySize += 1;
                                    list.GetArrayElementAtIndex(list.arraySize - 1).objectReferenceValue = instance;

                                    mesh.enabled = false;
                                    break;
                                }
                            }
                        }

                        serializedObject.ApplyModifiedProperties();


                        EditorUtility.DisplayDialog("Expansion",
                                "Meshes imported successfully!",
                                "Ok");
                    }
                    break;
                default:
                    break;
            }
        }

        private void MeshesUpdateSpecials(string typeOfMeshes, bool randomize = false)
        {
            if (typeOfMeshes.Equals(PT_Create_Prefab.TypeOfMesh.helmet))
            {
                SerializedProperty hair = MeshesGetType(PT_Create_Prefab.TypeOfMesh.hair);
                SerializedProperty helmet = MeshesGetType(PT_Create_Prefab.TypeOfMesh.helmet);
                (hair.FindPropertyRelative("list").GetArrayElementAtIndex
                    (hair.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = false;

                if (MeshesCheckForBeard())
                {
                    SerializedProperty beard = MeshesGetType(PT_Create_Prefab.TypeOfMesh.beard);
                    (beard.FindPropertyRelative("list").GetArrayElementAtIndex
                        (beard.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled =
                        (helmet.FindPropertyRelative("list").GetArrayElementAtIndex
                        (helmet.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).name.Contains("_nv");
                }
            }

            if (typeOfMeshes.Equals(PT_Create_Prefab.TypeOfMesh.hair))
            {
                SerializedProperty helmet = MeshesGetType(PT_Create_Prefab.TypeOfMesh.helmet);
                (helmet.FindPropertyRelative("list").GetArrayElementAtIndex(helmet.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = false;

                if (MeshesCheckForBeard())
                {
                    SerializedProperty beard = MeshesGetType(PT_Create_Prefab.TypeOfMesh.beard);
                    (beard.FindPropertyRelative("list").GetArrayElementAtIndex(beard.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = true;
                }
            }

            if (typeOfMeshes.Equals(PT_Create_Prefab.TypeOfMesh.beard))
            {
                SerializedProperty helmet = MeshesGetType(PT_Create_Prefab.TypeOfMesh.helmet);
                SerializedProperty hair = MeshesGetType(PT_Create_Prefab.TypeOfMesh.hair);

                if ((helmet.FindPropertyRelative("list").GetArrayElementAtIndex(helmet.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).name.Contains("_nv"))
                {
                    (helmet.FindPropertyRelative("list").GetArrayElementAtIndex(helmet.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = true;
                    (hair.FindPropertyRelative("list").GetArrayElementAtIndex(hair.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = false;
                }
                else
                {
                    (helmet.FindPropertyRelative("list").GetArrayElementAtIndex(helmet.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = false;
                    (hair.FindPropertyRelative("list").GetArrayElementAtIndex(hair.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = true;
                }
            }

            if(typeOfMeshes.Equals(PT_Create_Prefab.TypeOfMesh.upper))
            {
                SerializedProperty upper = MeshesGetType(PT_Create_Prefab.TypeOfMesh.upper);
                SerializedProperty upperList = upper.FindPropertyRelative("list");
                SerializedProperty upperIdx = upper.FindPropertyRelative("idx");
                SkinnedMeshRenderer upperSM = (upperList.GetArrayElementAtIndex(upperIdx.intValue).objectReferenceValue as SkinnedMeshRenderer);

                SerializedProperty lower = MeshesGetType(PT_Create_Prefab.TypeOfMesh.lower);
                SerializedProperty lowerList = lower.FindPropertyRelative("list");
                SerializedProperty lowerIdx = lower.FindPropertyRelative("idx");
                SkinnedMeshRenderer lowerSM = (lowerList.GetArrayElementAtIndex(lowerIdx.intValue).objectReferenceValue as SkinnedMeshRenderer);

                //Debug.Log($"Update upper mesh: {upperSM.name} lower mesh: {lowerSM.name}");
                if (upperSM.name.Contains("long") && lowerSM.name.Contains("skirt"))
                {
                    // If the upper body is a long cloth, while the lower part is a skirt, force pants on lower part
                    List<SkinnedMeshRenderer> smrs = new List<SkinnedMeshRenderer>();
                    for (int i = 0; i < lowerList.arraySize; i++)
                    {
                        SkinnedMeshRenderer smr = lowerList.GetArrayElementAtIndex(i).objectReferenceValue as SkinnedMeshRenderer;
                        if (smr.name.Contains("pants"))
                        {
                            smrs.Add(smr);
                        }
                    }

                    int index = randomize ? UnityEngine.Random.Range(0, smrs.Count) : 0;
                    lowerSM.enabled = false;
                    smrs[index].enabled = true;
                    lowerIdx.intValue = index;
                    serializedObject.ApplyModifiedProperties();
                }
            }

            if (typeOfMeshes.Equals(PT_Create_Prefab.TypeOfMesh.lower))
            {
                SerializedProperty upper = MeshesGetType(PT_Create_Prefab.TypeOfMesh.upper);
                SerializedProperty upperList = upper.FindPropertyRelative("list");
                SerializedProperty upperIdx = upper.FindPropertyRelative("idx");
                SkinnedMeshRenderer upperSM = (upperList.GetArrayElementAtIndex(upperIdx.intValue).objectReferenceValue as SkinnedMeshRenderer);

                SerializedProperty lower = MeshesGetType(PT_Create_Prefab.TypeOfMesh.lower);
                SerializedProperty lowerList = lower.FindPropertyRelative("list");
                SerializedProperty lowerIdx = lower.FindPropertyRelative("idx");
                SkinnedMeshRenderer lowerSM = (lowerList.GetArrayElementAtIndex(lowerIdx.intValue).objectReferenceValue as SkinnedMeshRenderer);

                //Debug.Log($"Update lower mesh: {lowerSM.name} upper mesh: {upperSM.name}");
                if (lowerSM.name.Contains("skirt") && upperSM.name.Contains("long"))
                {
                    // If the lowe body is a skirt, while the upper part is a long cloth, force short cloth on upper part
                    List<SkinnedMeshRenderer> smrs = new List<SkinnedMeshRenderer>();
                    for (int i = 0; i < upperList.arraySize; i++)
                    {
                        SkinnedMeshRenderer smr = upperList.GetArrayElementAtIndex(i).objectReferenceValue as SkinnedMeshRenderer;
                        if(smr.name.Contains("short"))
                        {
                            smrs.Add(smr);
                        }
                    }

                    int index = randomize ? UnityEngine.Random.Range(0, smrs.Count) : 0;
                    upperSM.enabled = false;
                    smrs[index].enabled = true;
                    upperIdx.intValue = index;
                    serializedObject.ApplyModifiedProperties();
                }
            }
        }

        public void MeshesRandom()
        {
            // Clear all meshes
            for (int i = 0; i < meshes.arraySize; i++)
            {
                SerializedProperty listOfMeshes = meshes.GetArrayElementAtIndex(i).FindPropertyRelative("list");

                if (listOfMeshes.arraySize == 0)
                {
                    // No list of meshes available for this cathegory.
                    continue;
                }

                // Disable all active meshes
                for (int k = 0; k < listOfMeshes.arraySize; k++)
                {
                    (listOfMeshes.GetArrayElementAtIndex(k).objectReferenceValue as SkinnedMeshRenderer).enabled = false;
                }
            }

            // Enable random meshes
            for (int i = 0; i < meshes.arraySize; i++)
            {
                string typeOfMesh = meshes.GetArrayElementAtIndex(i).FindPropertyRelative("type").stringValue;
                SerializedProperty listOfMeshes = meshes.GetArrayElementAtIndex(i).FindPropertyRelative("list");
                //Debug.Log("Random: " + typeOfMesh);
                if (listOfMeshes.arraySize == 0)
                {
                    // No list of meshes available for this cathegory.
                    continue;
                }

                if (typeOfMesh.Equals(PT_Create_Prefab.TypeOfMesh.helmet))
                {
                    // Note: in the meshes type list, the hair and beard are ahead of helmet
                    if (UnityEngine.Random.Range(0, 100) > 50)
                    {
                        MeshesEnableRandom(typeOfMesh);
                        SerializedProperty hair = MeshesGetType(PT_Create_Prefab.TypeOfMesh.hair);
                        SerializedProperty hairList = hair.FindPropertyRelative("list");
                        SerializedProperty hairIndex = hair.FindPropertyRelative("idx");

                        //Debug.Log("Disable hair: " + (hairList.GetArrayElementAtIndex(hairIndex.intValue).objectReferenceValue as SkinnedMeshRenderer).name);
                        (hairList.GetArrayElementAtIndex(hairIndex.intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = false;

                        if (MeshesCheckForBeard())
                        {
                            SerializedProperty beard = MeshesGetType(PT_Create_Prefab.TypeOfMesh.beard);
                            SerializedProperty beardList = beard.FindPropertyRelative("list");
                            SerializedProperty beardIndex = beard.FindPropertyRelative("idx");

                            SerializedProperty helmet = MeshesGetType(PT_Create_Prefab.TypeOfMesh.helmet);
                            SerializedProperty helmetList = helmet.FindPropertyRelative("list");
                            SerializedProperty helmetIndex = helmet.FindPropertyRelative("idx");

                            (beardList.GetArrayElementAtIndex(beardIndex.intValue).objectReferenceValue as SkinnedMeshRenderer).enabled =
                                (helmetList.GetArrayElementAtIndex(helmetIndex.intValue).objectReferenceValue as SkinnedMeshRenderer).name.Contains("_nv");
                        }
                    }
                }
                else if (typeOfMesh.Equals(PT_Create_Prefab.TypeOfMesh.cape))
                {
                    if (UnityEngine.Random.Range(0, 100) > 50)
                    {
                        MeshesEnableRandom(typeOfMesh);
                    }
                }
                else if (typeOfMesh.Equals(PT_Create_Prefab.TypeOfMesh.lower))
                {
                    MeshesEnableRandom(PT_Create_Prefab.TypeOfMesh.lower);
                    MeshesUpdateSpecials(PT_Create_Prefab.TypeOfMesh.lower, true);
                }
                else if (typeOfMesh.Equals(PT_Create_Prefab.TypeOfMesh.upper))
                {
                    MeshesEnableRandom(PT_Create_Prefab.TypeOfMesh.upper);
                    MeshesUpdateSpecials(PT_Create_Prefab.TypeOfMesh.upper, true);
                }
                else
                {
                    MeshesEnableRandom(typeOfMesh);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void MeshesEnableRandom(string type)
        {
            SerializedProperty typeOfMeshes = MeshesGetType(type);
            SerializedProperty list = typeOfMeshes.FindPropertyRelative("list");
            SerializedProperty index = typeOfMeshes.FindPropertyRelative("idx");
            (list.GetArrayElementAtIndex(index.intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = false;
            int attempts = 20;
            int random;
            do
            {
                random = UnityEngine.Random.Range(0, list.arraySize);
                attempts--;
            }
            while (random == index.intValue && attempts > 20);

            index.intValue = random;
            (list.GetArrayElementAtIndex(index.intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = true;
        }

        private bool MeshesCheckForBeard()
        {
            SerializedProperty beard = MeshesGetType(PT_Create_Prefab.TypeOfMesh.beard);
            return beard.FindPropertyRelative("list").arraySize > 0;
        }

        private SerializedProperty MeshesGetType(string typeOfMeshes)
        {
            for (int i = 0; i < meshes.arraySize; i++)
            {
                if (meshes.GetArrayElementAtIndex(i).FindPropertyRelative("type").stringValue == typeOfMeshes)
                {
                    return meshes.GetArrayElementAtIndex(i);
                }
            }

            return null;
        }

        private void MeshesDrawControls()
        {
            for (int i = 0; i < meshes.arraySize; i++)
            {
                SerializedProperty list = meshes.GetArrayElementAtIndex(i).FindPropertyRelative("list");
                SerializedProperty typeOfMeshes = meshes.GetArrayElementAtIndex(i).FindPropertyRelative("type");
                SerializedProperty index = meshes.GetArrayElementAtIndex(i).FindPropertyRelative("idx");

                // Don't display controlls for these type of meshes if the list is empty
                if (list.arraySize == 0)
                {
                    continue;
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(typeOfMeshes.stringValue.First().ToString().ToUpper() + typeOfMeshes.stringValue.Substring(1));

                if (GUILayout.Button("<"))
                {
                    (list.GetArrayElementAtIndex(index.intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = false;

                    if (index.intValue > 0)
                    {
                        (list.GetArrayElementAtIndex(--index.intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = true;
                    }
                    else
                    {
                        index.intValue = list.arraySize - 1;
                        (list.GetArrayElementAtIndex(index.intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = true;
                    }

                    MeshesUpdateSpecials(typeOfMeshes.stringValue);

                    break;
                }
                if (GUILayout.Button(">"))
                {
                    (list.GetArrayElementAtIndex(index.intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = false;
                    if (index.intValue < list.arraySize - 1)
                    {
                        (list.GetArrayElementAtIndex(++index.intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = true;
                    }
                    else
                    {
                        index.intValue = 0;
                        (list.GetArrayElementAtIndex(index.intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = true;
                    }

                    MeshesUpdateSpecials(typeOfMeshes.stringValue);

                    break;
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private void PrefabDrawControls()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(loaded != null && loaded.boolValue ? "Reload All" : "Load"))
            {
                ReloadAll();
            }

            if (loaded != null && loaded.boolValue && GUILayout.Button("Reload Materials"))
            {
                ReloadMaterials();
                ReapplyMaterials();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (loaded != null && loaded.boolValue && MeshesGetType(PT_Create_Prefab.TypeOfMesh.helmet).FindPropertyRelative("list").arraySize > 0 && GUILayout.Button("Toogle Helmet"))
            {
                SerializedProperty helmet = MeshesGetType(PT_Create_Prefab.TypeOfMesh.helmet);
                SerializedProperty hair = MeshesGetType(PT_Create_Prefab.TypeOfMesh.hair);
                SerializedProperty beard = MeshesGetType(PT_Create_Prefab.TypeOfMesh.beard);

                (helmet.FindPropertyRelative("list").GetArrayElementAtIndex(helmet.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled =
                    !(helmet.FindPropertyRelative("list").GetArrayElementAtIndex(helmet.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled;
                if ((helmet.FindPropertyRelative("list").GetArrayElementAtIndex(helmet.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled)
                {
                    if (MeshesCheckForBeard())
                    {
                        (beard.FindPropertyRelative("list").GetArrayElementAtIndex(beard.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled =
                            (helmet.FindPropertyRelative("list").GetArrayElementAtIndex(helmet.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).name.Contains("_nv") ? true : false;
                    }
                    (hair.FindPropertyRelative("list").GetArrayElementAtIndex(hair.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = false;
                }
                else
                {
                    if (MeshesCheckForBeard())
                    {
                        (beard.FindPropertyRelative("list").GetArrayElementAtIndex(beard.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = true;
                    }
                    (hair.FindPropertyRelative("list").GetArrayElementAtIndex(hair.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled = true;
                }
            }

            if (loaded != null && loaded.boolValue && MeshesGetType(PT_Create_Prefab.TypeOfMesh.cape).FindPropertyRelative("list").arraySize > 0 && GUILayout.Button("Toogle Cape"))
            {
                SerializedProperty cape = MeshesGetType(PT_Create_Prefab.TypeOfMesh.cape);
                (cape.FindPropertyRelative("list").GetArrayElementAtIndex(cape.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled =
                    !(cape.FindPropertyRelative("list").GetArrayElementAtIndex(cape.FindPropertyRelative("idx").intValue).objectReferenceValue as SkinnedMeshRenderer).enabled;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            if (loaded != null && loaded.boolValue && GUILayout.Button("Create Prefab"))
            {
                PrefabCreate();
            }

            if (loaded != null && loaded.boolValue && GUILayout.Button("Randomize"))
            {
                MeshesRandom();
            }

            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.textColor = Color.green;
            style.hover.textColor = Color.green;

            if (loaded != null && loaded.boolValue && !preview.boolValue &&GUILayout.Button("Preview"))
            {
                preview.boolValue = true;
            }

            if (loaded != null && loaded.boolValue && preview.boolValue && GUILayout.Button("Preview", style))
            {
                preview.boolValue = false;
            }

            if (loaded != null && loaded.boolValue && !duplicateMaterial.boolValue && GUILayout.Button("Duplicate Material"))
            {
                duplicateMaterial.boolValue = true;
            }

            if (loaded != null && loaded.boolValue && duplicateMaterial.boolValue && GUILayout.Button("Duplicate Material", style))
            {
                duplicateMaterial.boolValue = false;
            }

            EditorGUILayout.EndHorizontal();
        }

        private void PrefabCreate()
        {
            GameObject instance = Instantiate(script.gameObject);
            SkinnedMeshRenderer[] smrs = instance.GetComponentsInChildren<SkinnedMeshRenderer>();
            Material mat;

            if (null == material.FindPropertyRelative("assets").GetArrayElementAtIndex(material.FindPropertyRelative("index").intValue).objectReferenceValue)
            {
                EditorUtility.DisplayDialog("Internal Error",
                                "Link to the materials lost. Please Reload All or Reload Materials!",
                                "Ok");
                return;
            }

            if (duplicateMaterial.boolValue)
            {
                mat = new Material(smrs[0].sharedMaterial);

                string path = AssetDatabase.GetAssetPath(material.FindPropertyRelative("assets").GetArrayElementAtIndex(material.FindPropertyRelative("index").intValue).objectReferenceValue);
                string name = Path.GetFileNameWithoutExtension(path);
                path = Path.GetDirectoryName(path);
                path = path.Replace('\\', '/');
                int count = GetAssetsAtPath<Material>(path).Length;
                string final = path + "/" + name + "_" + count.ToString("D2") + ".mat";
                AssetDatabase.CreateAsset(mat, final);
            }
            else
            {
                SerializedProperty assets = material.FindPropertyRelative("assets");
                SerializedProperty index = material.FindPropertyRelative("index");
                mat = assets.GetArrayElementAtIndex(index.intValue).objectReferenceValue as Material;
            }

            
            DestroyImmediate(instance.GetComponent<PT_Create_Prefab>());
            foreach (SkinnedMeshRenderer smr in smrs)
            {
                if (!smr.enabled)
                {
                    DestroyImmediate(smr.gameObject);
                }
                else
                {
                    smr.sharedMaterial = mat;
                }
            }
        }

        private void ReloadMaterials()
        {
            SerializedProperty assets = material.FindPropertyRelative("assets");
            SerializedProperty index = material.FindPropertyRelative("index");
            assets.arraySize = 0;
            index.intValue = 0;

            // Get the material straight from the prefab's material.
            Material dm = PrefabUtility.GetCorrespondingObjectFromSource(script.gameObject).GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial;
            string path = AssetDatabase.GetAssetPath(dm);
            //Debug.Log("Default material is: " + dm.name);
            string name = Path.GetFileNameWithoutExtension(path);
            path = Path.GetDirectoryName(path);
            path = path.Replace('\\', '/');
            Material[] materials = GetAssetsAtPath<Material>(path);

            foreach (Material m in materials)
            {
                assets.arraySize += 1;
                assets.GetArrayElementAtIndex(assets.arraySize - 1).objectReferenceValue = m;
                serializedObject.ApplyModifiedProperties();
            }

            material.FindPropertyRelative("instance").objectReferenceValue = new Material(dm);
        }

        private void ReapplyMaterials()
        {
            for (int i = 0; i < meshes.arraySize; i++)
            {
                SerializedProperty listOfMeshes = meshes.GetArrayElementAtIndex(i).FindPropertyRelative("list");
                //Debug.Log($"Reaply on: {meshes.GetArrayElementAtIndex(i).displayName} -> {listOfMeshes.arraySize}");
                // Update the material for every mesh
                for (int k = 0; k < listOfMeshes.arraySize; k++)
                {
                    (listOfMeshes.GetArrayElementAtIndex(k).objectReferenceValue as SkinnedMeshRenderer).material =
                        material.FindPropertyRelative("instance").objectReferenceValue as Material;
                }
            }
            serializedObject.ApplyModifiedProperties();
            DefaultMaterialProperties();
        }

        private void ReloadMeshes()
        {
            SkinnedMeshRenderer[] allmeshes = script.GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < meshes.arraySize; i++)
            {
                string typeOfMesh = meshes.GetArrayElementAtIndex(i).FindPropertyRelative("type").stringValue;
                SerializedProperty listOfMeshes = meshes.GetArrayElementAtIndex(i).FindPropertyRelative("list");

                // Clear the list
                for (int k = 0; k < listOfMeshes.arraySize; k++)
                {
                    (listOfMeshes.GetArrayElementAtIndex(k).objectReferenceValue as SkinnedMeshRenderer).enabled = false;
                }

                listOfMeshes.arraySize = 0;
                serializedObject.ApplyModifiedProperties();
                // Reset the display index for this type of mesh
                meshes.GetArrayElementAtIndex(i).FindPropertyRelative("idx").intValue = 0;

                foreach (SkinnedMeshRenderer mesh in allmeshes)
                {
                    if (mesh.name.Contains(typeOfMesh))
                    {
                        listOfMeshes.arraySize += 1;
                        listOfMeshes.GetArrayElementAtIndex(listOfMeshes.arraySize - 1).objectReferenceValue = mesh;
                        mesh.enabled = false;
                        serializedObject.ApplyModifiedProperties();
                    }
                }

                //Debug.Log("Type of mesh: " + typeOfMesh);
                if (listOfMeshes.arraySize > 0 &&
                    typeOfMesh != PT_Create_Prefab.TypeOfMesh.helmet) // Do not display at startup the helmet
                {
                    (listOfMeshes.GetArrayElementAtIndex(0).objectReferenceValue as SkinnedMeshRenderer).enabled = true;
                    //Debug.Log("enable 0 for mesh: " + typeOfMesh);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void ReloadAll()
        {
            ReloadMaterials();
            ReloadMeshes();
            ReapplyMaterials();
            loaded.boolValue = true;
            serializedObject.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            if (!loaded.boolValue)
            {
                ReloadAll();
            }

            EditorGUILayout.LabelField("Meshes", EditorStyles.boldLabel);
            MeshesDrawControls();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Prefab", EditorStyles.boldLabel);
            PrefabDrawControls();


            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Expansion", EditorStyles.boldLabel);

            ExpansionDragAndDrop();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Shader", EditorStyles.boldLabel);
            
            // User may delete a material
            try
            {
                MaterialDropDownSelection();
            }
            catch
            {
                ReloadMaterials();
            }

            EditorGUILayout.Space();
            SetMaterialProperty_CS("skin", "Skin");
            SetMaterialProperty_CS("eyes", "Eyes");
            SetMaterialProperty_CS("hair", "Hair");
            SetMaterialProperty_CS("sclera", "Sclera");
            SetMaterialProperty_CS("lips", "Lips");
            SetMaterialProperty_CS("scars", "Scars");
            SetMaterialProperty_CS("other", "Other");
            EditorGUILayout.Space();
            SetMaterialProperty_CSM("metal1", "Metal1");
            SetMaterialProperty_CSM("metal2", "Metal2");
            SetMaterialProperty_CSM("metal3", "Metal3");
            SetMaterialProperty_CSM("metal4", "Metal4");
            EditorGUILayout.Space();
            SetMaterialProperty_CS("leather1", "Leather1");
            SetMaterialProperty_CS("leather2", "Leather2");
            SetMaterialProperty_CS("leather3", "Leather3");
            SetMaterialProperty_CS("leather4", "Leather4");
            EditorGUILayout.Space();
            SetMaterialProperty_C("cloth1", "Cloth1");
            SetMaterialProperty_C("cloth2", "Cloth2");
            SetMaterialProperty_C("cloth3", "Cloth3");
            SetMaterialProperty_C("cloth4", "Cloth4");
            EditorGUILayout.Space();
            SetMaterialProperty_CS("gems1", "Gems1");
            SetMaterialProperty_CS("gems2", "Gems2");
            SetMaterialProperty_CS("gems3", "Gems3");
            EditorGUILayout.Space();
            SetMaterialProperty_C("feathers1", "Feathers1");
            SetMaterialProperty_C("feathers2", "Feathers2");
            SetMaterialProperty_C("feathers3", "Feathers3");
            EditorGUILayout.Space();
            SetMaterialProperty_CI("coatofarms", "Coat of arms");
            EditorGUILayout.Space();
            SetMaterialProperty_CSP("light1", "Light1");
            SetMaterialProperty_CSP("light2", "Light3");
            SetMaterialProperty_CSP("light3", "Light3");
            SetMaterialProperty_UTL();


            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

        }

        private T[] GetAssetsAtPath<T>(string path)
        {
            ArrayList list = new ArrayList();
            string[] files = Directory.GetFiles(Directory.GetParent(Application.dataPath) + "/" + path);

            foreach (string file in files)
            {
                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(path + "/" + Path.GetFileName(file), typeof(T));
                if (obj != null)
                {
                    list.Add(obj);
                }
            }

            T[] result = new T[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                result[i] = (T)list[i];
            }

            return result;
        }
    }
}
