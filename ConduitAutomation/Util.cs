using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using EnumsNET;
using OpenQA.Selenium;

namespace ConduitAutomation
{
    public static class Util
    {
        public static Environment GetEnvironmentEnumFromDescription(string description)
        {
            return Enums.GetMembers<Environment>().Single(x => x.Attributes.Get<DescriptionAttribute>().Description == description).Value;
        }

        public static Browser GetBrowserEnumFromDescription(string description)
        {
            return Enums.GetMembers<Browser>().Single(x => x.Attributes.Get<DescriptionAttribute>().Description == description).Value;
        }

        public static string BuildUriString(string baseUri, string relativeUri)
        {
            var a = new Uri(baseUri);
            var combined = new Uri(a, relativeUri);
            return combined.ToString();
        }

        public static IList<string> GetListOfItems(IList<IWebElement> webElements)
        {
            var items = new List<string>();
            foreach (var i in webElements)
            {
                if (!string.IsNullOrEmpty(i.Text))
                {
                    items.Add(i.Text);
                }
            }

            return items;
        }
    }
}