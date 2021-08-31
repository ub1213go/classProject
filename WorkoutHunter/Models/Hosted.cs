using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WorkoutHunterV2.Models
{
    public class Hosted : IHostedService, IDisposable
    {
        static Timer _timer;
        public bool toggle { get; set; }

        public IConfiguration Configuration { get; }
        public Hosted(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null,
                TimeSpan.Zero,
                TimeSpan.FromDays(7));
            return Task.CompletedTask;
        }

        private int execCount = 0;

        public void DoWork(object state)
        {
            //利用 Interlocked 計數防止重複執行
            Interlocked.Increment(ref execCount);
            if(execCount == 1)
            {
                string connectionstring = Configuration.GetConnectionString("LinkToDb");
                Console.WriteLine("Work");
                using (var cn = new SqlConnection(connectionstring))
                {
                    cn.Open();
                    var a = new SqlCommand("exec RankProcedure", cn);
                    a.ExecuteNonQuery();
                    a.Dispose();
                }
            }
            Interlocked.Decrement(ref execCount);
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            //調整Timer為永不觸發，停用定期排程
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
