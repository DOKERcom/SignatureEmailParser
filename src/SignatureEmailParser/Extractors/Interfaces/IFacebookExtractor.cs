﻿using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Interfaces
{
    public interface IFacebookExtractor
    {

        public Task<ExtractWordModels> ExtractFacebooks(string data);

    }
}
