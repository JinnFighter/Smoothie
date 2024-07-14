namespace Smoothie.Scripts.Widgets
{
    public abstract class WidgetBase<TModel, TView> : IWidget where TModel : IViewModel where TView : BaseView
    {
        protected TModel ViewModel { get; private set; }
        protected TView View { get; private set; }
        private ISmoothieInstance SmoothieInstance { get; set; }

        public void Init()
        {
            InitInner();
        }

        public void Terminate()
        {
            TerminateInner();
        }

        public void Setup(IViewModel model, BaseView view, ISmoothieInstance smoothieInstance)
        {
            ViewModel = (TModel)model;
            View = (TView)view;
            SmoothieInstance = smoothieInstance;
        }

        protected virtual void InitInner()
        {
        }

        protected virtual void TerminateInner()
        {
        }

        protected void Open<TWidget>(IViewModel viewModel) where TWidget : IWidget  => SmoothieInstance.Open<TWidget>(viewModel);
        protected void Close<TWidget>(IViewModel viewModel) where TWidget : IWidget  => SmoothieInstance.Close<TWidget>(viewModel);
    }
}