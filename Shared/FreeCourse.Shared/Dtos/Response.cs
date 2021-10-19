using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FreeCourse.Shared.Dtos
{
    public class Response<T>                                                         // where ile kısıtlamıyoruz, int de olabilir, struct'da olabilir, class'da olabilir
    {   
        public T Data { get; private set; }                                             // Başarılı olunduğunda gidecek data

        [JsonIgnore]                                                                    // Uygulama içinde kullanılacak, zaten apilerin dönüşlerinde her daim bir status kodu oluyor                                                                                 o yüzden bunu göndermeye gerek yok
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccessful { get; private set; }
        public List<string> Errors { get; set; }

        // Static Factory Methods :  statik olarak nesne dönülüyorsa

        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };    // propları burada setlediğimizden setlerini private verdik, dışardan set edilmelerinin önüne geçtik
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Fail(List<string> errors, int statusCode)
        {
            return new Response<T> { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
        }

        public static Response<T> Fail(string error, int statusCode)
        {
            return new Response<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
