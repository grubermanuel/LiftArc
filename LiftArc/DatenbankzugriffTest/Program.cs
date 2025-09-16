using System;
using Npgsql;

class Program
{
    static async Task Main()
    {
        var connString = "Host=10.0.33.72;Port=5432;Database=liftarc;Username=liftarcuser;Password=liftarcpass";

        await using var conn = new NpgsqlConnection(connString);
        await conn.OpenAsync();

        var sql = "SELECT id, name, calories, protein, carbs, fat FROM foods";

        await using var cmd = new NpgsqlCommand(sql, conn);
        await using var reader = await cmd.ExecuteReaderAsync();

        Console.WriteLine("ID | Name           | Kalorien | Protein | Carbs | Fett");
        Console.WriteLine("------------------------------------------------------");

        while (await reader.ReadAsync())
        {
            Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetString(1),-14} | {reader.GetInt32(2),-8} | {reader.GetDecimal(3),-7} | {reader.GetDecimal(4),-5} | {reader.GetDecimal(5),-5}");
        }
    }
}