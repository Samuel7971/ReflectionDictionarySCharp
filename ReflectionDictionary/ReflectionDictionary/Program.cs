using System;
using System.Collections.Generic;

namespace ReflectionDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            var dicionario = new Dictionary<string, object>() { { "Id", 3 }, { "Nome", "Celular" }, { "Preco", 2100M } };

            var obj = SerializaObjctForJson<Produto>(Reflection<Produto>(dicionario));
            Console.WriteLine(obj);

            Console.ReadKey();
        }

        static T Reflection<T>(Dictionary<string, object> input)
        {
            var tipo = typeof(T);
            var obj = Activator.CreateInstance(tipo);

            foreach (var item in input)
            {
                var propety = tipo.GetProperty(item.Key);
                propety.SetValue(obj, item.Value);
            }
            return (T)obj;
        }

        static string SerializaObjctForJson<T>(T input)
        {
            var valorSerializado = "";

            foreach (var prop in input.GetType().GetProperties())
            {
                var type = prop.GetValue(input);
                if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(double))
                {
                    valorSerializado += "\"" + prop.Name + "\":" + "\"" + String.Format("{0:N}", type) + "\",";
                }
                else
                {
                    valorSerializado += "\"" + prop.Name + "\":" + "\"" + type + "\",";
                }
            }
            valorSerializado = valorSerializado.Substring(1, valorSerializado.Length - 2);
            return "{\"" + valorSerializado + "}";
        }
    }
}

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
}

