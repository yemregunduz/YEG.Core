using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEG.Core.Wrappers.Results.Abstract;

namespace YEG.Core.Wrappers.Results.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public T Data { get; }

        public DataResult(T data, bool isSuccess, string message) : base(isSuccess, message)
        {
            Data = data;
        }
        public DataResult(T data, bool isSuccess) : base(isSuccess)
        {
            Data = data;
        }
    }
}
