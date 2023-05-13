using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReiyxDev
{
    public class WebDomainInterpreter
    {
        public string CheckDomain(string _url)
        {
            Uri uri = new Uri(_url);

            return uri.Host;
        }
    }
}