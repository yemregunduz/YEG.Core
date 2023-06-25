using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEG.Core.Wrappers.Results.Concrete
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {

        }
        public ErrorDataResult(T data) : base(data, isSuccess: false)
        {

        }
        public ErrorDataResult(string message) : base(data: default, isSuccess: false, message)
        {

        }
        public ErrorDataResult() : base(data: default, isSuccess: false)
        {

        }
    }
}
