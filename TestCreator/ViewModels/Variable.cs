using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreator.ViewModels
{
    public class Variable : ViewModelBase
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}
