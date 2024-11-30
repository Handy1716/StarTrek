using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Application.Services
{
    public abstract class BaseService
    {
        protected List<T> GetPagedResults<T>(IQueryable<T> query, int pageNumber, int pageSize)
        {
            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
