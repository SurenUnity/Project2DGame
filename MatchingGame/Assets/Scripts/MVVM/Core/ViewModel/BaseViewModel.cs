namespace MVVM.Core.ViewModel
{
    public abstract class BaseViewModel : IBaseViewModel 
    {
        protected IViewManager ViewManager;

        public void SetViewManager(IViewManager viewManager)
        {
            ViewManager = viewManager;
        }

        public virtual void ViewShowed()
        {
        }

        public virtual void ViewHided()
        {
        }

        public virtual void ViewDestroyed()
        {
        }

        public abstract void Dispose();
    }
}