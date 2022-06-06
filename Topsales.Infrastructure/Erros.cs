using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topsales.Infrastructure
{
    public class Erros
    {
        public static Exception FaildToGetOrdersList(string url, string content)
        { 
            throw new InfrastructureException(
                $"Faild to get orders list for url:{url} and content:{content}...");
        }

        public static Exception FaildToGetOrdersList(string message, string url, string content)
        {
            throw new InfrastructureException(
                $"Message: {message} url:{url} and content:{content}...");
        }

        internal static Exception FailedToExtractOrdersResult(string url, string content)
        {
            throw new InfrastructureException(
               $"Failed to extract orders result url:{url} and content:{content}...");
        }

        internal static Exception FaildToGetProducts(string url, string content)
        {
            throw new InfrastructureException(
               $"Faild to get product list. url:{url} and content:{content}...");
        }

        internal static Exception FailedToExtractProducts(string url, string content)
        {
            throw new InfrastructureException(
                $"Failed to extract products result. url:{url} and content:{content}...");
        }

        internal static Exception FaildToGetProducts(string message, string url, string content)
        {
            throw new InfrastructureException(
               $"Message: {message} url:{url} and content:{content}...");
        }

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
    }
}
