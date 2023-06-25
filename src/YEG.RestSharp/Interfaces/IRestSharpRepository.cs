using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEG.RestSharp.Models;
using YEG.RestSharp.Repository;

namespace YEG.RestSharp.Interfaces
{
    public interface IRestSharpRepository<T>
        where T: class
    {
        Task<T> GetAsync(RestRequestParameter requestParameter);
        Task<T> PostAsync(RestRequestParameter requestParameter, T data);
        Task<T> PutAsync(RestRequestParameter requestParameter, T data);
        Task<T> PatchAsync(RestRequestParameter requestParameter, T data);
        Task<T> DeleteAsync(RestRequestParameter requestParameter);
    }
}
