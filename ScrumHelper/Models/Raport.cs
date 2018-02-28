using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumHelper.Models
{
    public class Raport
    {
        public List<TaskView> TaskViews { get; set; }

        public ViewModel ViewModel { get; set; }
    }
}