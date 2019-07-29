﻿using System.Collections.Generic;
using System.Threading;

namespace EFSecondLevelCache.Core
{
    /// <summary>
    /// Asynchronous version of the IEnumerable interface, allowing elements of the enumerable sequence to be retrieved asynchronously.
    /// </summary>
    public class EFAsyncEnumerable<T> : IAsyncEnumerable<T>
    {
        private readonly IEnumerator<T> _inner;

        /// <summary>
        /// Asynchronous version of the IEnumerable interface
        /// </summary>
        public EFAsyncEnumerable(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        /// <summary>
        /// Gets an asynchronous enumerator over the sequence.
        /// </summary>
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return new EFAsyncEnumerator<T>(_inner);
        }
    }
}