using Smoothie.Scripts.Widgets;

namespace Examples.Playground.Scripts.Screens
{
    public class PlaygroundUiScreen : UiScreen<PlaygroundScreenViewModel, PlaygroundScreenView>
    {
        protected override void InitInner()
        {
            View.ButtonCreateDialog.onClick.AddListener(HandleButtonCreateDialogClicked);
        }

        protected override void TerminateInner()
        {
            View.ButtonCreateDialog.onClick.RemoveListener(HandleButtonCreateDialogClicked);
        }

        private void HandleButtonCreateDialogClicked()
        {
        }
    }
}