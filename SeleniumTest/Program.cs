using Newtonsoft.Json.Linq;
using OpenQA.Selenium.DevTools.V115.IndexedDB;
using OpenQA.Selenium.Edge;


namespace SeleniumTest {
    internal class Program {
        static async Task Main(string[] args) {
            using var Service = EdgeDriverService.CreateDefaultService();
            Service.HideCommandPromptWindow = true;

            var Options = new EdgeOptions() {

            };

            using var Driver = new EdgeDriver(Service, Options);

            Driver.Navigate().GoToUrl("https://app.smokeball.com/#/login");

            await Task.Delay(10 * 1000);

            //Make the following work.

            using var Session = Driver.GetDevToolsSession();

            var Adapter = new IndexedDBAdapter(Session);


            var Query = new RequestDataCommandSettings() {
                SecurityOrigin = $@"https://app.smokeball.com/",
                DatabaseName = "smokeballStateDB",
                ObjectStoreName = "persisted-state",
                IndexName = "persist:itops/auth2",
            };

            var Response = await Adapter.RequestData(Query);

        }
    }
}