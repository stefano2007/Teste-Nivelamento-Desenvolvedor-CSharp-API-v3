using System.Text.Json;

namespace Questao5.Domain.Extensions;
public static class JsonExtension
{
    public static string ToJson<T>(this T objeto)
        => JsonSerializer.Serialize<T>(objeto);

    public static T ToObjetoByJson<T>(this string jsonObjeto)
        => JsonSerializer.Deserialize<T>(jsonObjeto);

}
