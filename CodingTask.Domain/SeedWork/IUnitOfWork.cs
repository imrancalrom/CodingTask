﻿using System.Threading;
using System.Threading.Tasks;

namespace CodingTask.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}