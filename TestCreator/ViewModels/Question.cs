using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestCreator.ViewModels
{
    public class Question : ViewModelBase
    {
        [JsonProperty("v")]
        public string Text { get; set; } = "";
    }
}
