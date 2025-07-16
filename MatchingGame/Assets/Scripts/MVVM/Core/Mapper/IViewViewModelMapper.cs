using System;

namespace MVVM.Core.Mapper
{
    public interface IViewViewModelMapper
    {
        Type GetViewModelType(Type viewType);
    }
}