// Program.cs (Wydawca)
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using shared;

namespace Wydawca_zadanie1
{
    internal class Program
    {
        private static bool isRunning = false;
        private static int counter = 1;
        private static readonly Random random = new Random();
        private static readonly MessageObserver messageObserver = new MessageObserver();

        static async Task Main(string[] args)
        {
            Console.WriteLine("wydawca");

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("amqps://zyfkaljc:i9rkwiZr1uYcU1abPKWEccn6NpjuDihg@cow.rmq2.cloudamqp.com/zyfkaljc"), h =>
                {
                    h.Username("zyfkaljc");
                    h.Password("i9rkwiZr1uYcU1abPKWEccn6NpjuDihg");
                });

                cfg.ReceiveEndpoint("publisher_queue", ep =>
                {
                    ep.Handler<Ustaw>(context =>
                    {
                        if (context.Message.Dziala == "True")
                        {
                            isRunning = true;
                            Console.WriteLine("Generator uruchomiony");
                        }
                        else if (context.Message.Dziala == "False")
                        {
                            isRunning = false;
                            Console.WriteLine("Generator zatrzymany");
                        }
                        return Task.CompletedTask;
                    });

                    ep.Handler<OdpA>(async context =>
                    {
                        await HandleResponse(context, "OdpA", context.Message.Kto);
                    });

                    ep.Handler<OdpB>(async context =>
                    {
                        await HandleResponse(context, "OdpB", context.Message.Kto);
                    });
                });
                cfg.ConnectConsumeObserver(messageObserver);
            });

            await busControl.StartAsync();

            try
            {
                Task.Run(() => MonitorKeyPress());
                while (true)
                {
                    if (isRunning)
                    {
                        var message = new Publ { Number = counter++ };
                        await busControl.Publish(message);
                        Console.WriteLine($"Opublikowano wiadomość: {message.Number}");
                        messageObserver.IncrementPublishCount<Publ>();
                    }

                    await Task.Delay(1000);
                }
            }
            finally
            {
                await busControl.StopAsync();
            }
        }

        private static async Task HandleResponse<T>(ConsumeContext<T> context, string responseType, string kto) where T : class
        {
            int attempt = 0;
            const int maxRetries = 5;

            while (attempt < maxRetries)
            {
                try
                {
                    attempt++;
                    await messageObserver.PreConsume(context);
                    if (random.Next(3) == 0)
                    {
                        throw new Exception("sztuczny wyjątek");
                    }
                    else
                    {
                        Console.WriteLine($"Odpowiedź typu {responseType} od {kto}");
                        await messageObserver.PostConsume(context);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    await messageObserver.ConsumeFault(context, ex);
                    if (attempt == maxRetries)
                    {
                        Console.WriteLine($"Nie udało się obsłużyć wiadomości od {kto} po {maxRetries} próbach");
                    }
                    else
                    {
                        Console.WriteLine($"Obsługa wyjątku od {kto}: {ex.Message}, próba numer {attempt}");
                        if (responseType == "OdpA")
                        {
                            await context.RespondAsync(new ExceptionA { exceptionmessage = ex.Message, message = kto });
                        }
                        else if (responseType == "OdpB")
                        {
                            await context.RespondAsync(new ExceptionB { exceptionmessage = ex.Message, message = kto });
                        }
                    }
                }
            }
        }

        private static void MonitorKeyPress()
        {
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.S)
                {
                    messageObserver.PrintStatistics();
                }
            }
        }
    }

    public class MessageObserver : IConsumeObserver
    {
        private readonly Dictionary<string, int> _attemptCounts = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _successCounts = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _publishCounts = new Dictionary<string, int>();

        public Task PreConsume<T>(ConsumeContext<T> context) where T : class
        {
            var messageType = typeof(T).Name;
            if (!_attemptCounts.ContainsKey(messageType))
            {
                _attemptCounts[messageType] = 0;
                _successCounts[messageType] = 0;
            }

            _attemptCounts[messageType]++;
            return Task.CompletedTask;
        }

        public Task PostConsume<T>(ConsumeContext<T> context) where T : class
        {
            var messageType = typeof(T).Name;
            _successCounts[messageType]++;
            return Task.CompletedTask;
        }

        public Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
        {
            return Task.CompletedTask;
        }

        public void IncrementPublishCount<T>() where T : class
        {
            var messageType = typeof(T).Name;
            if (!_publishCounts.ContainsKey(messageType))
            {
                _publishCounts[messageType] = 0;
            }

            _publishCounts[messageType]++;
        }

        public void PrintStatistics()
        {
            Console.WriteLine("Statystyki wiadomości:");
            foreach (var messageType in _attemptCounts.Keys)
            {
                Console.WriteLine($"Typ wiadomości: {messageType}");
                Console.WriteLine($"  Liczba prób obsłużenia: {_attemptCounts[messageType]}");
                Console.WriteLine($"  Liczba pomyślnie obsłużonych: {_successCounts[messageType]}");
            }
            foreach (var messageType in _publishCounts.Keys)
            {
                if (_publishCounts.ContainsKey(messageType))
                {
                    Console.WriteLine($"Typ wiadomości: {messageType}");
                    Console.WriteLine($"  Liczba opublikowanych komunikatów: {_publishCounts[messageType]}");
                }
                else
                {
                    Console.WriteLine($"  Liczba opublikowanych komunikatów: 0");
                }
            }
        }
    }
}
