using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestCreator.ViewModels
{
    public class TaskBaseViewModel : ViewModelBase
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        public string BaseWrongMessage = "";

        public virtual TaskBaseViewModel Clone()
        {
            throw new NotImplementedException();
        }
    }
}
