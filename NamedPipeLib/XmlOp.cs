using NamedPipeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PipeExSer.Operations
{
    class XmlOp
    {
        public async System.Threading.Tasks.Task XmlPutAsync(List<Event> L1, string UserKey, string xmlhead, string xmlend)
        {
            string xmlerBody = "";
            using (var client = new HttpClient())
            {
                foreach (Event E in L1)
                {
                    xmlerBody = xmlerBody +
            "<tem:Event>" +
            "<!--Optional:-->" +
            "<!--Optional:-->" +
            "<tem:UserKey>" + UserKey + "</tem:UserKey>" +
            "<tem:IndexEvent>" + E.IndexEvent.ToString() + "</tem:IndexEvent>" +
            "<tem:DateEvent>" + "2021-12-12" + "</tem:DateEvent>" +
            "<!--Optional:-->" +
            "<tem:HeureEvent>" + E.HeureEvent + "</tem:HeureEvent>" +
            "<tem:DoorNumber>" + E.DoorNumber.ToString() + "</tem:DoorNumber>" +
            "<tem:UserNumber>" + E.UserNumber.ToString() + "</tem:UserNumber>" +
            "<tem:CodeEvent>" + E.CodeEvent.ToString() + "</tem:CodeEvent>" +
            "<tem:CodeControler>" + E.CodeControler.ToString() + "</tem:CodeControler>" +
            "<tem:IndiceControler>" + E.IndiceControler.ToString() + "</tem:IndiceControler>" +
            "<tem:Selected>" + E.Selected.ToString().ToLower() + "</tem:Selected>" +
            "<!--Optional:-->" +
            "<tem:NumAccessCard>" + E.NumAccessCard + "</tem:NumAccessCard>" +
            "<tem:Data12>" + E.Data12.ToString() + "</tem:Data12>" +
            "<tem:Flux>" + E.Flux.ToString() + "</tem:Flux>" +
            "</tem:Event>";

                }
                try
                {
                    // Console.WriteLine(xmlhead + xmlerBody + xmlend);
                    var content = new StringContent(xmlhead + xmlerBody + xmlend, Encoding.UTF8, "application/xml"); ;
                    // Console.WriteLine(xmlhead + xmlerBody + xmlend);
                    var response = await client.PutAsync("https://localhost:44395/Service.asmx", content);
                    string O = await response.Content.ReadAsStringAsync();
                    string APIstat = response.StatusCode.ToString();
                    int hint = O.IndexOf("<InsertManyResult>");
                    int hint2 = O.IndexOf("INSERTION DONE");
                    string info = O.Substring(hint + 18, hint2 - hint - 18);
                    // Console.WriteLine(O);
                    Console.WriteLine(APIstat);
                    if (O.Contains("INSERTION DONE"))
                    {


                        Console.WriteLine("Sent to API DONE");
                    }
                    else
                    {
                        Console.WriteLine("NO IT FAILED");
                    }
                    Console.WriteLine(info);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        public async Task<bool> XmlStatusAsync(string xmlhead, string xmlend)
        {
            using (var client = new HttpClient())
            {
                string xmlerBody = "<tem:ServerH/>";
                try
                {
                    // Console.WriteLine(xmlhead + xmlerBody + xmlend);
                    var content = new StringContent(xmlhead + xmlerBody + xmlend, Encoding.UTF8, "application/xml"); ;

                    var response = await client.PostAsync("https://localhost:44395/Service.asmx", content);
                    string O = await response.Content.ReadAsStringAsync();
                    string n = response.StatusCode.ToString();
                    //   Console.WriteLine(O);
                    //   Console.WriteLine(n);

                    Boolean r = O.Contains("ONLINE");
                    if (r) Console.WriteLine("TEST SUCCESS");
                    return true;
                }
                catch (Exception e)
                {

                    //  Console.WriteLine("SERVER IS DOWN OR REQUEST IS TRASH");
                    return false;
                }
            }
        }
    }
}
