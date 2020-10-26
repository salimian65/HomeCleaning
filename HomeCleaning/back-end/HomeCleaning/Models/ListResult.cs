using System.Collections.Generic;
using System.Linq;

namespace HomeCleaning.Api.Models
{
    public class ListResult<T>
    {
        public ListResult(IEnumerable<T> result, int totalCount, int pageNumber, int pageSize)
        {
            Result = result;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public ListResult((IEnumerable<T>, int) result, int pageNumber, int pageSize) : this(result.Item1, result.Item2, pageNumber, pageSize)
        {
        }

        public ListResult(IEnumerable<T> result) : this(result, result.Count(), 1, result.Count())
        {
        }

        public IEnumerable<T> Result { get; }

        public int TotalCount { get; }

        public int PageSize { get; }

        /// <summary>
        /// Start from 1
        /// </summary>
        public int PageNumber { get; }
    }
}