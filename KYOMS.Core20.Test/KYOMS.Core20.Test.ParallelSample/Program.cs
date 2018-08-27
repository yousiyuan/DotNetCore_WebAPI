using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KYOMS.Core20.Test.ParallelSample
{
    class Program
    {
        static void Main(string[] args)
        {

            var actions = new List<Action>();
            for (var i = 0; i < 100; i++)
            {
                var j = i;
                actions.Add(() => ShowDate(DateTime.Now.AddDays(j).ToString("yyyy-MM-dd HH:mm:ss.fff")));
            }
            Console.WriteLine(actions.Count);
            Console.WriteLine("开启多核执行");


            Parallel.Invoke(new ParallelOptions { MaxDegreeOfParallelism = 8 }, actions.ToArray());

            Console.ReadKey();
        }

        static void ShowDate(string value)
        {
            Console.WriteLine($"当前时间戳：{value}");
        }


    }
}
