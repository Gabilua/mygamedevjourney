namespace Custom.Dialogue
{
    using TMPro;
    using UnityEngine.UI;

    /// <summary>
    /// Dialogue window that can send input data (not yet implemented sending data)
    /// </summary>
    public class InputDialogue : DialogueBase
    {
        // referenses
        public TMP_Text question;
        public TMP_InputField answerInputField;
        public Button submitButton;



        public override void SetData(DialogueData data)
        {
            base.SetData(data);

            question.text = data.textData[0];

            submitButton.onClick.AddListener(InvokeButtonAction);
        }

        // TODO: Add functionality of sending data from inputField
        private void InvokeButtonAction()
        {
            if(currentData.buttonActions.Length > 0)
                currentData.buttonActions[0]?.Invoke();

            submitButton.onClick.RemoveListener(InvokeButtonAction);
        }
    }
}