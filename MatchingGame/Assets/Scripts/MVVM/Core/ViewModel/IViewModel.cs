using System;

namespace MVVM.Core.ViewModel
{
    public interface IViewModel : IDisposable
    {
        void ViewShowed();
        void ViewHided();
        void ViewDestroyed();
    }
}