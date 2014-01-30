using System.Collections.Generic;

namespace CustomTimers.Common
{
    public interface IMementoContainer
    {
        object CreateMemento();
        void SetMemento(object memento);
    }
}
