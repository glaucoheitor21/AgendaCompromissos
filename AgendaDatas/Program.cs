using System;
using System.Collections.Generic;
using System.Globalization;

class Compromisso
{
    public string Descricao { get; set; }
    public DateTimeOffset DataHora { get; set; }
    public string TimeZoneAmigavel { get; set; }

    public override string ToString()
    {
        return $"{Descricao} - {DataHora:yyyy-MM-dd HH:mm} ({TimeZoneAmigavel})";
    }
}

class Program
{
    static List<Compromisso> compromissos = new();

    // Mapeamento de timezones amigáveis para IDs reconhecidos pelo sistema
    static readonly Dictionary<string, string> timezoneMap = new()
    {
        { "UTC", "UTC" },
        { "America/Sao_Paulo", "E. South America Standard Time" },
        { "America/New_York", "Eastern Standard Time" },
        { "America/Los_Angeles", "Pacific Standard Time" },
        { "Europe/London", "GMT Standard Time" },
        { "Europe/Paris", "Romance Standard Time" },
        { "Europe/Berlin", "W. Europe Standard Time" },
        { "Asia/Tokyo", "Tokyo Standard Time" },
        { "Asia/Shanghai", "China Standard Time" },
        { "Asia/Kolkata", "India Standard Time" },
        { "Australia/Sydney", "AUS Eastern Standard Time" },
        { "Africa/Johannesburg", "South Africa Standard Time" },
        { "America/Mexico_City", "Central Standard Time (Mexico)" },
        { "America/Argentina/Buenos_Aires", "Argentina Standard Time" },
        { "Pacific/Auckland", "New Zealand Standard Time" }
    };

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n1. Adicionar compromisso");
            Console.WriteLine("2. Exibir compromissos do dia atual");
            Console.WriteLine("3. Exibir compromissos de uma data");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    AdicionarCompromisso();
                    break;
                case "2":
                    ExibirCompromissosDiaAtual();
                    break;
                case "3":
                    ExibirCompromissosPorData();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void ListarTimezones()
    {
        Console.WriteLine("Timezones disponíveis:");
        int index = 1;
        foreach (var tz in timezoneMap.Keys)
        {
            Console.WriteLine($"{index}. {tz}");
            index++;
        }
    }

    static string ObterTimezoneAmigavel()
    {
        ListarTimezones();
        Console.Write("Escolha o número do TimeZone (pressione Enter para usar o padrão do sistema): ");
        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
            return "UTC";

        if (int.TryParse(input, out var index) && index >= 1 && index <= timezoneMap.Count)
        {
            return new List<string>(timezoneMap.Keys)[index - 1];
        }

        Console.WriteLine("Opção inválida. Usando UTC.");
        return "UTC";
    }

    static void AdicionarCompromisso()
    {
        Console.Write("Descrição: ");
        var descricao = Console.ReadLine();

        Console.Write("Data e hora (yyyy-MM-dd HH:mm): ");
        var dataHoraStr = Console.ReadLine();

        var timezoneAmigavel = ObterTimezoneAmigavel();
        var timezoneId = timezoneMap[timezoneAmigavel];

        if (!DateTime.TryParseExact(dataHoraStr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dataHora))
        {
            Console.WriteLine("Data/hora inválida.");
            return;
        }

        try
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            var dataHoraOffset = new DateTimeOffset(dataHora, tz.GetUtcOffset(dataHora));
            compromissos.Add(new Compromisso
            {
                Descricao = descricao,
                DataHora = dataHoraOffset,
                TimeZoneAmigavel = timezoneAmigavel
            });
            Console.WriteLine("Compromisso adicionado.");
        }
        catch
        {
            Console.WriteLine("Timezone inválido.");
        }
    }

    static void ExibirCompromissosDiaAtual()
    {
        var timezoneAmigavel = ObterTimezoneAmigavel();
        var timezoneId = timezoneMap[timezoneAmigavel];

        try
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            var agora = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);
            var data = agora.Date;

            Console.WriteLine($"\nCompromissos para {data:yyyy-MM-dd} ({timezoneAmigavel}):");
            foreach (var c in compromissos)
            {
                var dataConvertida = TimeZoneInfo.ConvertTime(c.DataHora.UtcDateTime, tz);
                if (dataConvertida.Date == data)
                    Console.WriteLine(c);
            }
        }
        catch
        {
            Console.WriteLine("Timezone inválido.");
        }
    }

    static void ExibirCompromissosPorData()
    {
        Console.Write("Informe a data (yyyy-MM-dd): ");
        var dataStr = Console.ReadLine();

        var timezoneAmigavel = ObterTimezoneAmigavel();
        var timezoneId = timezoneMap[timezoneAmigavel];

        if (!DateTime.TryParseExact(dataStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var data))
        {
            Console.WriteLine("Data inválida.");
            return;
        }

        try
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);

            Console.WriteLine($"\nCompromissos para {data:yyyy-MM-dd} ({timezoneAmigavel}):");
            foreach (var c in compromissos)
            {
                var dataConvertida = TimeZoneInfo.ConvertTime(c.DataHora.UtcDateTime, tz);
                if (dataConvertida.Date == data.Date)
                    Console.WriteLine(c);
            }
        }
        catch
        {
            Console.WriteLine("Timezone inválido.");
        }
    }
}