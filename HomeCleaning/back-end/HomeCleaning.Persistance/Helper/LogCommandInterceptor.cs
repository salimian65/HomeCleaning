#if DEBUG
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructures.Helper
{
    public class LogCommandInterceptor : DbCommandInterceptor
    {
        private static void WriteLine(CommandEventData data)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($@"EF {data}", "LocalDB");
            Console.ResetColor();
        }

        public override void CommandFailed(DbCommand command, CommandErrorEventData data)
        {
            WriteLine(data);
        }

        public override Task CommandFailedAsync(DbCommand command, CommandErrorEventData data, CancellationToken cancellation)
        {
            WriteLine(data);
            return Task.CompletedTask;
        }

        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData data, DbDataReader result)
        {
            WriteLine(data);
            return result;
        }

        public override object ScalarExecuted(DbCommand command, CommandExecutedEventData data, object result)
        {
            WriteLine(data);
            return result;
        }

        public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData data, int result)
        {
            WriteLine(data);
            return result;
        }

        public override Task<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData data, DbDataReader result, CancellationToken cancellation)
        {
            WriteLine(data);
            return Task.FromResult(result);
        }

        public override Task<object> ScalarExecutedAsync(DbCommand command, CommandExecutedEventData data, object result, CancellationToken cancellation)
        {
            WriteLine(data);
            return Task.FromResult(result);
        }

        public override Task<int> NonQueryExecutedAsync(DbCommand command, CommandExecutedEventData data, int result, CancellationToken cancellation)
        {
            WriteLine(data);
            return Task.FromResult(result);
        }
    }
}
#endif
