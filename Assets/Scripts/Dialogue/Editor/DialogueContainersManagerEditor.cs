namespace Custom.Dialogue
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    [CustomEditor(typeof(DialogueContainersManager))]
    public class DialogueContainersManagerEditor : Editor
    {
        private DialogueContainersManager manager;
        private List<DialogueContainer> dialogueContainers;
        private SerializedProperty prop_dialogueContainers;
        private SerializedObject[] so_dialogueContainers;


        private bool[] foldoutsStates;



        private void OnEnable()
        {
            manager = (DialogueContainersManager)target;
            FindAllDialogueContainers();
            manager.dialogueContainers = dialogueContainers;
            prop_dialogueContainers = serializedObject.FindProperty("dialogueContainers");
            so_dialogueContainers = new SerializedObject[dialogueContainers.Count];

            foldoutsStates = new bool[dialogueContainers.Count];
        }

        public override void OnInspectorGUI()
        {
            if(dialogueContainers != null && dialogueContainers.Count > 0)
            {
                EditorGUILayout.BeginVertical();
                EditorGUI.BeginChangeCheck();

                for (int i = 0; i < dialogueContainers.Count; i++)
                {
                    SerializedProperty dContainer = prop_dialogueContainers.GetArrayElementAtIndex(i);
                    so_dialogueContainers[i] = new SerializedObject(dContainer.objectReferenceValue);

                    SerializedProperty foldout = so_dialogueContainers[i].FindProperty("isEditing");

                    foldout.boolValue = EditorGUILayout.Foldout(foldout.boolValue, dialogueContainers[i].internalName, true);

                    if (!foldoutsStates[i] && foldout.boolValue)
                    {
                        for (int x = 0; x < dialogueContainers.Count; x++)
                        {
                            if(x != i)
                            {
                                foldoutsStates[x] = false;
                                dialogueContainers[x].isEditing = false;
                            }
                        }

                        foldoutsStates[i] = true;
                    }


                    if (foldout.boolValue)
                    {
                        if (GUILayout.Button("Test Dialogue"))
                        {
                            if(EditorApplication.isPlaying)
                            {
                                dialogueContainers[i].TestDialogue();
                            }
                            else
                            {
                                EditorApplication.EnterPlaymode();
                                dialogueContainers[i].TestDialogue();
                            }
                        }

                        dialogueContainers[i].internalName = EditorGUILayout.TextField("Internal name", dialogueContainers[i].internalName);
                        dialogueContainers[i].containerName = EditorGUILayout.TextField("Container name", dialogueContainers[i].containerName);
                        EditorGUILayout.Space();

                        SerializedProperty dialogueChain = so_dialogueContainers[i].FindProperty("dialogueChain");
                            EditorGUILayout.PropertyField(dialogueChain, dialogueChain.isExpanded);
                    }
                }

                EditorGUILayout.EndVertical();

                if(EditorGUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();

                    for (int i = 0; i < so_dialogueContainers.Length; i++)
                    {
                        so_dialogueContainers[i].ApplyModifiedProperties();
                    }
                }
            }
        }

        private void FindAllDialogueContainers()
        {
            Scene scene = SceneManager.GetActiveScene();
            GameObject[] roots = scene.GetRootGameObjects();
            dialogueContainers = new List<DialogueContainer>();

            for (int i = 0; i < roots.Length; i++)
            {
                FindDialogueContainerRecursive(roots[i]);
            }
        }

        private void FindDialogueContainerRecursive(GameObject target)
        {
            DialogueContainer container = target.GetComponent<DialogueContainer>();

            if (container != null)
                dialogueContainers.Add(container);

            int childCount = target.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                FindDialogueContainerRecursive(target.transform.GetChild(i).gameObject);
            }
        }
    }
}