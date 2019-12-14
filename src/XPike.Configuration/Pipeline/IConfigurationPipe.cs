using System;
using System.Threading.Tasks;

namespace XPike.Configuration.Pipeline
{
    public interface IConfigurationPipe
    {
        string PipelineGet(string key, Func<string, string> next);

        Task<string> PipelineGetAsync(string key, Func<string, Task<string>> next);

        T PipelineGet<T>(string key, Func<string, T> next);

        Task<T> PipelineGetAsync<T>(string key, Func<string, Task<T>> next);
    }
}