using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEG.Core.Wrappers.Results.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(data, isSuccess: true, message)
        {

        }
        public SuccessDataResult(T data) : base(data, isSuccess: true)
        {

        }
        public SuccessDataResult(string message) : base(data: default, isSuccess:true, message)
        {

        }
        public SuccessDataResult() : base(data: default, isSuccess: true)
        {

        }
    }
}
