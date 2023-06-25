using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEG.Core.Wrappers.Results.Abstract;

namespace YEG.Core.BusinessRules
{
    public class BusinessRules
    {
        /// <summary>
        /// Executes the given array of IResult objects and returns the first logic result that has failed.
        /// Returns null if no failed result is found.
        /// </summary>
        /// <param name="logics">Array of logic results to execute</param>
        /// <returns>The first failed logic result or null</returns>
        public static IResult? ExecuteFirstFailedLogic(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.IsSuccess)
                {
                    return logic;
                }
            }
            return null;
        }

        /// <summary>
        /// Executes the given array of IResult objects and returns a list of all logic results that have failed.
        /// Returns null if there are no failed results or all results are successful.
        /// </summary>
        /// <param name="logics">Array of logic results to execute</param>
        /// <returns>List of failed logic results or null</returns>
        public static IList<IResult>? ExecuteAllFailedLogics(params IResult[] logics)
        {
            IList<IResult>? results = null;

            foreach (var logic in logics)
            {
                if (!logic.IsSuccess)
                {
                    if(results is null)
                        results = new List<IResult>();

                    results.Add(logic);
                }
            }

            return results is not null ? results.Where(result => result.IsSuccess == false).ToList() : results;
        }
    }
}
