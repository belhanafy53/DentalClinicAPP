using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicAPP
{
    public interface IChangeToken
    {
        //
        // Summary:
        //     Indicates if this token will pro-actively raise callbacks. If false, the token
        //     consumer must poll Microsoft.Extensions.Primitives.IChangeToken.HasChanged to
        //     detect changes.
        bool ActiveChangeCallbacks { get; }
        //
        // Summary:
        //     Gets a value that indicates if a change has occurred.
        bool HasChanged { get; }

        //
        // Summary:
        //     Registers for a callback that will be invoked when the entry has changed. Microsoft.Extensions.Primitives.IChangeToken.HasChanged
        //     MUST be set before the callback is invoked.
        //
        // Parameters:
        //   callback:
        //     The System.Action`1 to invoke.
        //
        //   state:
        //     State to be passed into the callback.
        //
        // Returns:
        //     An System.IDisposable that is used to unregister the callback.
        IDisposable RegisterChangeCallback(Action<object> callback, object state);
    }
}
