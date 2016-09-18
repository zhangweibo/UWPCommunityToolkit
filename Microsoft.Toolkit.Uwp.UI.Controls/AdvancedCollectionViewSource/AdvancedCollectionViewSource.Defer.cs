﻿using System;

namespace Microsoft.Toolkit.Uwp.UI.Controls.AdvancedCollectionViewSource
{
    /// <summary>
    /// A collection view source implementation that supports filtering, grouping, sorting and incremental loading
    /// </summary>
    public partial class AdvancedCollectionViewSource
    {
        /// <summary>
        /// Stops refreshing until it is disposed
        /// </summary>
        /// <returns>An disposable object</returns>
        public IDisposable DeferRefresh()
        {
            return new NotificationDeferrer(this);
        }

        /// <summary>
        /// Notification deferrer helper class
        /// </summary>
        public class NotificationDeferrer : IDisposable
        {
            private readonly AdvancedCollectionViewSource _acvs;
            private readonly object _currentItem;

            /// <summary>
            /// Initializes a new instance of the <see cref="NotificationDeferrer"/> class.
            /// </summary>
            /// <param name="acvs">Source ACVS</param>
            public NotificationDeferrer(AdvancedCollectionViewSource acvs)
            {
                _acvs = acvs;
                _currentItem = _acvs.CurrentItem;
                _acvs._deferCounter++;
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            /// <filterpriority>2</filterpriority>
            public void Dispose()
            {
                _acvs.MoveCurrentTo(_currentItem);
                _acvs._deferCounter--;
                _acvs.Refresh();
            }
        }
    }
}
