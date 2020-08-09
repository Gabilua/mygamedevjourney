namespace Custom.Dialogue
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public enum DialogueType
    {
        YesNo,
        Input,
        Continue
    }

    [System.Serializable]
    public class DialogueData
    {
        public string name;
        public DialogueType dialogueType;
        [TextArea]
        public string[] textData;
        public UnityEvent[] buttonActions;
    }

    /// <summary>
    /// Contains chains of dialogue data (only linear chains rn)
    /// </summary>
    public class DialogueContainer : MonoBehaviour
    {
#if UNITY_EDITOR
        [HideInInspector]
        public string internalName;
        [HideInInspector]
        public bool isEditing;
#endif

        public string containerName;

        public DialogueData[] dialogueChain;

        [ContextMenu("Test")]
        public void TestDialogue()
        {
            FindObjectOfType<DialogueWindow>().OpenDialogue(this);
        }
    }
}
