﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anubis.Web.Models
{
    public class ApplicationModel
    {
        public long ApplicationId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public object Regions { get; set; }
    }
}