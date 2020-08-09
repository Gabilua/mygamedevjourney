namespace Custom.Dialogue
{
    using UnityEngine;

    public interface IDialogueWindow
    {
        void SetData(DialogueData data);
    }

    public abstract class DialogueBase : MonoBehaviour, IDialogueWindow
    {
        protected DialogueData currentData;



        public virtual void SetData(DialogueData data)
        {
            currentData = data;
        }
    }
}