using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace White_Tiger
{
    public class JsonContentTypeMapper : WebContentTypeMapper
    {
        public override WebContentFormat
               GetMessageFormatForContentType(string contentType)
        {
            if (contentType == "text/javascript")
            {
                return WebContentFormat.Json;
            }
            else if ( contentType==WebContentFormat.Raw.ToString())
            {
                return WebContentFormat.Json;

            }
            else
            {
                return WebContentFormat.Default;
            }
        }
    }
}
