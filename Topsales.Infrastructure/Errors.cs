using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topsales.Infrastructure
{
    public class Errors
    {      
        internal static Exception FaildToPerformAction(string action ,string url, string content)
        {
            throw new InfrastructureException(
               $"Action {action} failed url:{url} and content:{content}...");
        }

        internal static Exception FaildToPerformAction(string action, string message, string url, string content)
        {
            throw new InfrastructureException(
               $"Action {action} failed. message={message} url={url} and content={content}...");
        }

        internal static Exception FailedToExtract(string resourceType,string url, string content)
        {
            throw new InfrastructureException(
                $"Failed to extract {resourceType} result. url:{url} and content:{content}...");
        }

        internal static void NotFound(string getProducts)
        {
            throw new NotImplementedException();
        }
    }
}
