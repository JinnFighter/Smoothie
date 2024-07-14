using Smoothie.Scripts.Widgets;

namespace Examples.Playground.Scripts.Dialogs
{
    public class PlaygroundDialog : UiDialog<PlaygroundDialogViewModel, PlaygroundDialogView>
    {
        protected override void InitInner()
        {
            View.ButtonAccept.onClick.AddListener(HandleButtonAcceptClicked);
        }

        protected override void TerminateInner()
        {
            View.ButtonAccept.onClick.RemoveListener(HandleButtonAcceptClicked);
        }

        private void HandleButtonAcceptClicked()
        {
            ViewModel.Accept();
            Close<PlaygroundDialog>(ViewModel);
        }
    }
}