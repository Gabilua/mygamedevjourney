namespace Custom.Dialogue
{
    using TMPro;
    using UnityEngine.UI;

    /// <summary>
    /// Single message dialogue window with a button to continue/close the dialogue
    /// </summary>
    public class MessageDialogue : DialogueBase
    {
        public TMP_Text message;

        public Button continueButton;



        public override void SetData(DialogueData data)
        {
            base.SetData(data);

            message.text = data.textData[0];

            continueButton.onClick.AddListener(InvokeButtonAction);
        }

        private void InvokeButtonAction()
        {
            if(currentData.buttonActions.Length > 0)
                currentData.buttonActions[0]?.Invoke();

            continueButton.onClick.RemoveListener(InvokeButtonAction);
        }
    }
}