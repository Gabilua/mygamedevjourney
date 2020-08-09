namespace Custom.Dialogue
{
    using TMPro;
    using UnityEngine.UI;

    /// <summary>
    /// Dialogue window that has 2 options
    /// Yes - continues the dialogue
    /// No  - closes the dialogue
    /// </summary>
    public class YesNoDialogue : DialogueBase
    {
        public TMP_Text message;
        public TMP_Text question;

        public Button yesButton;
        public Button noButton;



        public override void SetData(DialogueData data)
        {
            base.SetData(data);

            message.text = data.textData[0];
            question.text = data.textData[1];

            yesButton.onClick.AddListener(OnYesButtonClick);
            noButton.onClick.AddListener(OnNoButtonClick);
        }

        private void OnYesButtonClick()
        {
            if(currentData.buttonActions.Length > 0)
                currentData.buttonActions[0]?.Invoke();

            yesButton.onClick.RemoveListener(OnYesButtonClick);

        }

        private void OnNoButtonClick()
        {
            if(currentData.buttonActions.Length > 1)
            currentData.buttonActions[1]?.Invoke();

            noButton.onClick.RemoveListener(OnNoButtonClick);
        }
    }
}