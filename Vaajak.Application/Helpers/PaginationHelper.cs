using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaajak.Application.Dto.Primitives;

namespace Vaajak.Application.Helpers
{
    public static class PaginationHelper
    {
        public static async Task<PaginatedResponse<T>> ToPaginatedResponseAsync<T>(this IQueryable<T> query, PaginationRequestDTO paginationRequestDTO)
        {
            var totalCount = await query.CountAsync();
            if (paginationRequestDTO.Paging)
            {
                query = query.Skip((paginationRequestDTO.PageNumber - 1) * paginationRequestDTO.PageSize).Take(paginationRequestDTO.PageSize);
            }

            var items = await query.ToListAsync();

            return new PaginatedResponse<T>
            {
                Items = items,
                PageNumber = paginationRequestDTO.PageNumber,
                PageSize = paginationRequestDTO.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
