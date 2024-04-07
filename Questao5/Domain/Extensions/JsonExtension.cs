using System.Text.Json;

namespace Questao5.Domain.Extensions;
public static class JsonExtension
{
    public static string ToJson<T>(this T objeto)
    {
        return JsonSerializer.Serialize<T>(objeto);
    }

    public static T ToObjetoByJson<T>(this string jsonObjeto)
    {
        return JsonSerializer.Deserialize<T>(jsonObjeto);
    }
}
