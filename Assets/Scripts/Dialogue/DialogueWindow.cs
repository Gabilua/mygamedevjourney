namespace Custom.Dialogue
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.Playables;

    /// <summary>
    /// Displays dialogues with animations
    /// </summary>
    public class DialogueWindow : MonoBehaviour
    {
        // referenses
        [Header("DialogueWindows")]
        public DialogueBase yesNoDialogueWindow;
        public DialogueBase inputDialogueWindow;
        public DialogueBase messageDialogueWindow;

        [Header("Animations")]
        public PlayableDirector showDialogueWindowTimeline;
        public PlayableDirector transitionFromTimelne;
        public PlayableDirector transitionToTimelne;
        public PlayableDirector hideDialogueWindowTimeline;

        // cached
        private DialogueData[] dialogueChain;
        private int currentDialogueIndex;



        public void OpenDialogue(DialogueContainer container)
        {
            currentDialogueIndex = 0;
            dialogueChain = container.dialogueChain;
            SetDialogueData();

            transitionFromTimelne.Stop();
            transitionToTimelne.Stop();
            hideDialogueWindowTimeline.Stop();
            showDialogueWindowTimeline.Play();
        }

        public void ContinueDialogue()
        {
            hideDialogueWindowTimeline.Stop();
            showDialogueWindowTimeline.Stop();
            transitionFromTimelne.Play();
            transitionToTimelne.Stop();
        }

        public void CloseDialogue()
        {
            transitionFromTimelne.Stop();
            transitionToTimelne.Stop();
            showDialogueWindowTimeline.Stop();
            hideDialogueWindowTimeline.Play();
        }

        private void OnTransitionFinished(PlayableDirector director)
        {
            HideAllWindows();

            currentDialogueIndex++;

            if(currentDialogueIndex >= dialogueChain.Length)
            {
                CloseDialogue();
                return;
            }

            SetDialogueData();
            transitionToTimelne.Play();
        }

        private void HideAllWindows()
        {
            yesNoDialogueWindow.gameObject.SetActive(false);
            inputDialogueWindow.gameObject.SetActive(false);
            messageDialogueWindow.gameObject.SetActive(false);
        }

        private void SetDialogueData()
        {
            switch (dialogueChain[currentDialogueIndex].dialogueType)
            {
                case DialogueType.YesNo:
                    yesNoDialogueWindow.SetData(dialogueChain[currentDialogueIndex]);
                    yesNoDialogueWindow.gameObject.SetActive(true);
                    break;
                case DialogueType.Input:
                    inputDialogueWindow.SetData(dialogueChain[currentDialogueIndex]);
                    inputDialogueWindow.gameObject.SetActive(true);
                    break;
                case DialogueType.Continue:
                    messageDialogueWindow.SetData(dialogueChain[currentDialogueIndex]);
                    messageDialogueWindow.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        private void OnEnable()
        {
            transitionFromTimelne.stopped += OnTransitionFinished;
        }

        private void OnDisable()
        {
            transitionFromTimelne.stopped -= OnTransitionFinished;
        }
    }
}