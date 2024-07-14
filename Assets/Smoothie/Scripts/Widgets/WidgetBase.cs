namespace Smoothie.Scripts.Widgets
{
    public abstract class WidgetBase<TModel, TView> : IWidget where TModel : IViewModel where TView : BaseView
    {
        protected TModel ViewModel { get; private set; }
        protected TView View { get; private set; }
        protected ISmoothieInstance SmoothieInstance { get; private set; }

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
    }
}