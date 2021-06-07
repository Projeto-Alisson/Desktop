using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Repositorio
{
    public static class API <Model>
    {
        static String host = "http://apiprojetotoledo1.herokuapp.com/view";
        static HttpClient client = new HttpClient();

        public static List<Model> get(string path)
        {
            Func<string, Task<List<Model>>> func = async (_path) =>
            {
                List<Model> models = default;
                HttpResponseMessage response = await client.GetAsync(host + _path, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    HttpContent content = response.Content;
                    models = await content.ReadAsAsync<List<Model>>();
                }
                return models;
            };

            return func(path).Result;
        }

        public static List<Model> get(string path, int cod)
        {
            object body = new
            {
                cod_empresa = cod
            };

            Func<string, object, Task<List<Model>>> func = async (_path, _body) =>
            {
                List<Model> models = default;


                var client = new HttpClient();
                var serializedProduto = JsonConvert.SerializeObject(_body);
                var content = new StringContent(serializedProduto, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.method
                var result = await client.(host + path, content);


                if (response.IsSuccessStatusCode)
                {
                    HttpContent content = response.Content;
                    models = await content.ReadAsAsync<List<Model>>();
                }
                return models;
            };

            return func(path, body).Result;
        }

        public async static void post(string path, Model model)
        {
            using (var client = new HttpClient())
            {
                var serializedProduto = JsonConvert.SerializeObject(model);
                var content = new StringContent(serializedProduto, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(host+ path, content);
            }
        }

    }
}