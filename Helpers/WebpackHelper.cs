using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace webpack_aspnet5.Helpers
{
    public static class WebpackHelper
    {
        public static JObject GetWebpackAssetsJson(string applicationBasePath)
        {
            JObject webpackAssetsJson = null;
            string packageJsonFilePath = $"{applicationBasePath}\\{"package.json"}";

            using (StreamReader packageJsonFile = File.OpenText(packageJsonFilePath))
            {
                using (JsonTextReader packageJsonReader = new JsonTextReader(packageJsonFile))
                {
                    JObject packageJson = (JObject)JToken.ReadFrom(packageJsonReader);
                    JObject webpackConfigJson = (JObject)packageJson["customConfig"]["webpackConfig"];
                    string webpackAssetsFileName = webpackConfigJson["assetsFileName"].Value<string>();
                    string webpackBuildDirectory = webpackConfigJson["buildDirectory"].Value<string>();
                    string webpackAssetsFilePath = $"{applicationBasePath}\\{webpackBuildDirectory}\\{webpackAssetsFileName}";

                    using (StreamReader webpackAssetsFile = File.OpenText(webpackAssetsFilePath))
                    {
						using (JsonTextReader webpackAssetsReader = new JsonTextReader(webpackAssetsFile))
						{
							webpackAssetsJson = (JObject)JToken.ReadFrom(webpackAssetsReader);
						}
                    }
                }
            }
			
			return webpackAssetsJson;
        }
    }
}