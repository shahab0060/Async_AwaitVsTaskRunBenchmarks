using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Async_AwaitVsTaskRunBenchmarks
{
    [MemoryDiagnoser]
    [SimpleJob(warmupCount: 1)] 
    public class AsyncVsTaskRun
    {
        private const int CalculationCount = 1_000_000_000;
        private int PerformCpuIntensiveWork()
        {
            int total = 0;
            for (int i = 0; i < CalculationCount; i++)
            {
                total += i;
            }
            return total;
        }

        [Benchmark]
        public async Task AsyncAwaitDirectly()
        {
            await PerformCpuIntensiveWorkAsync();
        }

        [Benchmark]
        public async Task OffloadToTaskRun()
        {
            await Task.Run(() => PerformCpuIntensiveWork());
        }

        private async Task<int> PerformCpuIntensiveWorkAsync()
        {
            return await Task.FromResult(PerformCpuIntensiveWork());
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<AsyncVsTaskRun>();
            //How to use:
            //1:Open a new powershell windows
            //2:Navigate to the project destination by typing : cd Async_AwaitVsTaskRunBenchmarks
            //3:Run the project in release mode by typing : dotnet run -c Release
        }
    }
}
